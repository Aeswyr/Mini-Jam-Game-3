using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateControls : MonoBehaviour
{
    
    public GameObject bookstack;
    public GameObject bookstackPrefab;

    [SerializeField] private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bookstack != null && !bookstack.Equals(null) && bookstack.GetComponent<WallSummon>().expired) {
            Destroy(bookstack);
            bookstack = null;
        }
    }

    public void SummonBookstack() {
        if (bookstack == null) {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            bookstack = Instantiate(bookstackPrefab, pos, Quaternion.identity);
        }
    }
}
