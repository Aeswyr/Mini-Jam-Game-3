using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private float speed = 2f;
    private float maxAccel = 0.4f;

    public bool horizontal;
    public int direction;

    private bool turning = false;

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
        float velGoal = speed * direction;

        if (horizontal) {
            float accel = Mathf.Clamp(velGoal - rbody.velocity.x, -maxAccel, maxAccel); 
            rbody.velocity = new Vector2(rbody.velocity.x + accel, rbody.velocity.y);
        } else {
            float accel = Mathf.Clamp(velGoal - rbody.velocity.y, -maxAccel, maxAccel); 
            rbody.velocity = new Vector2(rbody.velocity.x , rbody.velocity.y + accel);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        //Turn the character by flipping the direction
        int cLayer = collision.gameObject.layer;

        if (groundLayer == (groundLayer | (1 << cLayer)) && !turning) {
		    direction *= -1;
            turning = true;
        }
        /* flipping code, can uncomment if needed.
		//Record the current scale
		Vector3 scale = transform.localScale;

		//Set the X scale to be the original times the direction
		scale.x = originalXScale * direction;

		//Apply the new scale
		transform.localScale = scale;
        */
    }

    void OnTriggerExit2D() {
        turning = false;
    }
}
