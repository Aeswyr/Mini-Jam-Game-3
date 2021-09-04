using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 6f;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private FloorDetector floorDetector;

    // Start is called before the first frame update
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    
    bool jumping;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 momentum = rbody.velocity;
        if (Input.GetAxis("Horizontal") != 0) {
                momentum.x = speed * Input.GetAxis("Horizontal");
                
        }
        if (jumping && floorDetector.GetGrounded()) {
            momentum.y = speed;
            jumping = false;
        }
        rbody.velocity = momentum;
        //if (Input.GetAxis("Vertical") != 0) {
        //        momentum.y = speed * Input.GetAxis("Vertical");
        //        rbody.velocity = momentum;
        //}
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            jumping = true;
    }
}
