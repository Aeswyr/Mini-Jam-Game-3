using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHudManager : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private ItemDisplayManager[] displays;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var display in displays)
            display.SetInventory(inventory);
    }
}
