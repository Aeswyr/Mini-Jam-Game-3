using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBook : MonoBehaviour
{
    
    public LayerMask groundLayer;

    public float initialFallspeed;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, - initialFallspeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        int cLayer = collision.gameObject.layer;

        if (groundLayer == (groundLayer | (1 << cLayer))) {
            Object.Destroy(this.gameObject);
        }
    }
}
