using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{

    public bool drawDebugRaycasts = false;

    public float speed = 2f;
    public float maxAccel = 0.4f;
    private int direction = 1;
    private float originalXScale;
    
    private bool alive = true;

    private float footOffset = 0.5f;
    private float wallOffset = 0.5f;
    private float midHeight = 0.3f;
    private float groundCheck = 0.1f;
    private float wallCheck = 0.2f;

    public LayerMask attackLayer;

    [SerializeField] private Rigidbody2D rbody;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        originalXScale = transform.localScale.x;
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        int cLayer = collision.gameObject.layer;

        if (alive && attackLayer == (attackLayer | (1 << cLayer))) {
            die();
        }
    }

    void die() {
        // TODO: Run death animation
        Debug.Log("die");
        alive = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move() {
        RaycastHit2D frontfoot = Raycast(new Vector2(footOffset * direction, 0f), Vector2.down, groundCheck);
        RaycastHit2D backfoot = Raycast(new Vector2(footOffset * -direction, 0f), Vector2.down, groundCheck);

        RaycastHit2D wall  = Raycast(new Vector2(wallOffset * direction, midHeight), Vector2.right * direction, wallCheck);

        if ((backfoot && !frontfoot) || wall) {
            turnAround();
        }

        float xVelGoal = speed * direction;
        float accel = Mathf.Clamp(xVelGoal - rbody.velocity.x, -maxAccel, maxAccel); 
        rbody.velocity = new Vector2(rbody.velocity.x + accel, rbody.velocity.y);
    }

    void turnAround() {
        //Turn the character by flipping the direction
		direction *= -1;

		//Record the current scale
		Vector3 scale = transform.localScale;

		//Set the X scale to be the original times the direction
		scale.x = originalXScale * direction;

		//Apply the new scale
		transform.localScale = scale;
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
