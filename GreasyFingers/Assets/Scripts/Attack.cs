using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float summonTime;
    public float damageStartTime;
    public float damageEndTime;
    public float totalLength;

    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Collider2D col;
    //private bool attackDamage = false;

    // Start is called before the first frame update
    void Start()
    {

        //rbody = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
        summonTime = Time.time;
        
        //rbody = gameObject.GetComponent<Rigidbody2D>();
        //rbody.detectCollisions = false;
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= summonTime + damageStartTime && Time.time < summonTime + damageEndTime) {
            //attackDamage = true;
            //rbody.detectCollisions = true;
            col.enabled = true;
        }

        if (Time.time >= summonTime + damageEndTime) {
            //attackDamage = false;
            col.enabled = false;
        }
        
        if (Time.time >= summonTime + totalLength) {
            Object.Destroy(this.gameObject);
        }
    }
}
