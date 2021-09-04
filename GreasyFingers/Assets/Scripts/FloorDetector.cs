using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
        Debug.Log("collider entered");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
        Debug.Log("collider exited");
    }

    public bool GetGrounded() {
        return isGrounded;
    }
}
