using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public bool drawDebugRaycasts = false;

    private float speed = 2f;
    private float maxAccel = 0.4f;

    private float wallCheck = 0.3f;

    public bool horizontal;
    public int direction;

    [SerializeField] private Rigidbody2D rbody;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start() {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        Move();
    }

    void Move() {
        RaycastHit2D wall = Raycast(new Vector2(0, 0), rbody.velocity, wallCheck);

        if (wall) {
            turnAround();
        }

        float velGoal = speed * direction;

        if (horizontal) {
            float accel = Mathf.Clamp(velGoal - rbody.velocity.x, -maxAccel, maxAccel); 
            rbody.velocity = new Vector2(rbody.velocity.x + accel, rbody.velocity.y);
        } else {
            float accel = Mathf.Clamp(velGoal - rbody.velocity.y, -maxAccel, maxAccel); 
            rbody.velocity = new Vector2(rbody.velocity.x , rbody.velocity.y + accel);
        }
    }

    void turnAround() {
        //Turn the character by flipping the direction
		direction *= -1;
        
        /*
		//Record the current scale
		Vector3 scale = transform.localScale;

		//Set the X scale to be the original times the direction
		scale.x = originalXScale * direction;

		//Apply the new scale
		transform.localScale = scale;
        */
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
