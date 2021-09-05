using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.TryGetComponent(typeof(PlayerInventory), out Component comp)) {
            if (((PlayerInventory)comp).Remove(Item.Key))
                Destroy(transform.parent.gameObject);
        }
    }
}
