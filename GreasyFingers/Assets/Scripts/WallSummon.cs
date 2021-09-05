using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSummon : MonoBehaviour
{
    public float lifespan = 10f;
    private float summonTime;

    private float riseSpeed = 12.5f;
    private float riseTime = 0.17f;

    public bool expired = false;

    [SerializeField] private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        summonTime = Time.time;
        rbody = gameObject.GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, riseSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.velocity.y != 0 && Time.time > summonTime + riseTime) {
            rbody.velocity = new Vector2(0, 0);
        }

        if (Time.time > summonTime + lifespan) {
            expired = true;
        }
    }
}
