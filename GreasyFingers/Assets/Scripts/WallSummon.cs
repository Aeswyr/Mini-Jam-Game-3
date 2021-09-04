using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSummon : MonoBehaviour
{
    public float lifespan = 10f;
    private float summonTime;

    public bool expired = false;


    // Start is called before the first frame update
    void Start()
    {
        summonTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > summonTime + lifespan) {
            expired = true;
        }
    }
}
