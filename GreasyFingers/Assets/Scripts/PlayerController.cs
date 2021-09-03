using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 6f;
    [SerializeField] private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 momentum = rbody.velocity;
        if (Input.GetAxis("Horizontal") != 0) {
                momentum.x = speed * Input.GetAxis("Horizontal");
                
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            momentum.y = speed;
        }
        rbody.velocity = momentum;
        //if (Input.GetAxis("Vertical") != 0) {
        //        momentum.y = speed * Input.GetAxis("Vertical");
        //        rbody.velocity = momentum;
        //}
    }
}
