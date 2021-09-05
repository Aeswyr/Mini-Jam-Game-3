using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.TryGetComponent(typeof(PlayerInventory), out Component comp)) {
            ((PlayerInventory)comp).Add(Item.Life);
            Destroy(gameObject);
        }
    }
}
