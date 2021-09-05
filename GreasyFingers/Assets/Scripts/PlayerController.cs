using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool drawDebugRaycasts = false;

    //Constants
    private float speed = 6f;
    private float maxAccel = .8f;

    private float footOffset = .25f;
    [SerializeField] private float vFootOffset = -0.9f;
    private float groundCheck = 0.1f;

    public float summonDist;

    public float jumpHoldDuration;	//How long the jump key can be held
    public float jumpForce;           //Initial jump force
	public float jumpHoldForce;		//Incremental force when jump is held

    public float doubleJumpForce;
    private bool usedDoubleJump = false;
    
    private float originalXScale;

    //Variables

    private bool onGround = true;
    private int direction = 1;
    public bool alive = true;
    public bool dying = false;
    
    private bool isJumping;
    private float jumpTime;							//Variable to hold jump duration

    private float coyoteTime;
    private float coyoteDuration;

    private float dashSpeed = 30f;
    private Vector2 dashVelocity;
    private float lastDash = 0f;
    private float dashCooldown = 3f;
    private readonly float RAD45 = Mathf.Sqrt(2) / 2;

    private PlayerInput input;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public LayerMask proximityLayer;

    public GameObject activeCrate;
    public GameObject fallingBookPrefab;
    public GameObject attackPrefab;
    public GameObject bookBombPrefab;

    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
        originalXScale = transform.localScale.x;

        inventory.Add(Item.Platform, 5);

        int[] inv = {0, 0, 0, 5, 4, 5, 5, 1, 1, 1};
        inventory.AddLevelInventory(inv);
        inventory.FillLevelInventory();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (alive) {
            PhysicsCheck();

            GroundMovement();
            midAirMovement();

            handleAbilities();
        } else if (dying) {
            playerDeath();
        }
    }

    void playerDeath() {
        //Run death animation
        OnDeath();

        rbody.bodyType = RigidbodyType2D.Static;
        dying = false;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        int cLayer = collision.gameObject.layer;

        if (alive && enemyLayer == (enemyLayer | (1 << cLayer))) {
            alive = false;
            dying = true;
        }

        if (proximityLayer == (proximityLayer | (1 << cLayer))) {
            activeCrate = collision.gameObject;
            activeCrate.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Activated");
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        int cLayer = collision.gameObject.layer;

        if (proximityLayer == (proximityLayer | (1 << cLayer)) && collision.gameObject == activeCrate) {
            activeCrate.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Deactivated");
            activeCrate = null;
        }
    }

    void PhysicsCheck() {
        RaycastHit2D leftCheck  = Raycast(new Vector2(- footOffset, vFootOffset), Vector2.down, groundCheck);
        RaycastHit2D rightCheck = Raycast(new Vector2(  footOffset, vFootOffset), Vector2.down, groundCheck);

        if (leftCheck || rightCheck) {
            if (!onGround) {
                usedDoubleJump = false;
                OnLand();
            }
            SetGrounded(true);
        } else {
            SetGrounded(false);
        }
    }

    private void SetGrounded(bool val) {
        onGround = val;
        animator.SetBool("Grounded", val);
    }

    void GroundMovement() {
        animator.SetBool("Moved", input.horizontal != 0);

        float xVelGoal = speed * input.horizontal;

        float accel = Mathf.Clamp(xVelGoal - rbody.velocity.x, -maxAccel, maxAccel); 

        rbody.velocity = new Vector2(rbody.velocity.x + accel, rbody.velocity.y);

        if ( (xVelGoal * direction) < 0f)
			FlipCharacterDirection();

		//If the player is on the ground, extend the coyote time window
		if (onGround) {
			coyoteTime = Time.time + coyoteDuration;
        }
    }
    
    void midAirMovement() {
        if (input.jumpPressed && !isJumping && (onGround || coyoteTime > Time.time)) {
            //momentum.y = speed * Input.GetAxis("Vertical");
            rbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            jumpTime = Time.time + jumpHoldDuration;

            OnJump();
            
        } else if (isJumping) {
			//...and the jump button is held, apply an incremental force to the rigidbody...
			if (input.jumpHeld)
				rbody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);

			//...and if jump time is past, set isJumping to false
			if (jumpTime <= Time.time)
				isJumping = false;
		} else if (input.jumpPressed && !isJumping && !usedDoubleJump && inventory.Remove(Item.Doublejump)) {
            rbody.velocity = new Vector2(rbody.velocity.x, 0);
            rbody.AddForce(new Vector2(0f, doubleJumpForce), ForceMode2D.Impulse);
            usedDoubleJump = true;

            Vector3 pos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
            Instantiate(fallingBookPrefab, pos, Quaternion.identity);

            OnJump();
        }
    }

    private void OnJump() {
        isJumping = true;
        animator.SetTrigger("Jumped");
    }

    private void OnLand() {
        animator.SetTrigger("Landed");
    }

    private void OnDeath() {
        animator.SetTrigger("Died");
        alive = false;
    }

    private void OnRevive() {
        alive = true;
        animator.Play("Base Layer.player_idle");
    }
    	
    void FlipCharacterDirection() {
		//Turn the character by flipping the direction
		direction *= -1;

		//Record the current scale
		Vector3 scale = transform.localScale;

		//Set the X scale to be the original times the direction
		scale.x = originalXScale * direction;

		//Apply the new scale
		transform.localScale = scale;
	}

    void handleAbilities() {
        if (input.summonPressed && activeCrate != null && !activeCrate.Equals(null) && inventory.Remove(Item.Platform)) {
            //gm.addSummon(new Vector3(transform.position.x + direction * summonDist, transform.position.y, transform.position.z));
            activeCrate.transform.parent.gameObject.GetComponent<CrateControls>().SummonBookstack();
        }

        if (input.dashPressed && checkDash()) {
            ApplyDash();
        }

        if (input.attackPressed && inventory.Remove(Item.Reaper)) {
            Vector3 pos = new Vector3(transform.position.x + (direction * 1f), transform.position.y, transform.position.z);
            GameObject atk = Instantiate(attackPrefab, pos, Quaternion.identity);
            //atk.transform.parent = rbody.transform;
        }
        if (input.bookBombPressed && inventory.Remove(Item.Bookbomb)) {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject bomb = Instantiate(bookBombPrefab, pos, Quaternion.identity);
            bomb.GetComponent<bookGrenade>().setDir(direction);
            //atk.transform.parent = rbody.transform;
        }
    }

    private bool checkDash() {
        return Time.time > lastDash + dashCooldown && (input.horizontal != 0 || input.vertical != 0) && inventory.Remove(Item.Dash);
    }

    private void ApplyDash() {
        dashVelocity = dashSpeed * new Vector2(input.horizontal, input.vertical);

        if (input.horizontal != 0 && input.vertical != 0)
            dashVelocity *= RAD45;

        //AnimateDash();

        lastDash = Time.time;
        rbody.velocity = dashVelocity;
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length) {
		//Call the overloaded Raycast() method using the ground layermask and return 
		//the results
		return Raycast(offset, rayDirection, length, groundLayer);
	}

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask) {
		//Record the player's position
		Vector2 pos = transform.position;

		//Send out the desired raycasr and record the result
		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

		//If we want to show debug raycasts in the scene...
		if (drawDebugRaycasts)
		{
			//...determine the color based on if the raycast hit...
			Color color = hit ? Color.red : Color.green;
			//...and draw the ray in the scene view
			Debug.DrawRay(pos + offset, rayDirection * length, color);
		}

		//Return the results of the raycast
		return hit;
	}
}
