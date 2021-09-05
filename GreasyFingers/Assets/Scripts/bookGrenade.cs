using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookGrenade : MonoBehaviour
{
    
    public LayerMask groundLayer;

    public float hThrowSpeed;
    public float vThrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, - initialFallspeed);
    }

    public void setDir(int direction) {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(hThrowSpeed * direction, vThrowSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        int cLayer = collision.gameObject.layer;

        if (groundLayer == (groundLayer | (1 << cLayer))) {
            Object.Destroy(this.gameObject);
        }
    }
}
