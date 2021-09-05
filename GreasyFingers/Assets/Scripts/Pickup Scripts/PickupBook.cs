using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBook : MonoBehaviour
{
    [SerializeField] private Item[] rewards;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.TryGetComponent(typeof(PlayerInventory), out Component comp)) {
            foreach (var item in rewards)
                ((PlayerInventory)comp).Add(item);
            Destroy(gameObject);
        }
    }
}
