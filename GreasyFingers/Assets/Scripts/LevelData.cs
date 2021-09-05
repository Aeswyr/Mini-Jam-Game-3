using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Item[] LevelItems;
    [SerializeField] private Vector2 spawnpoint;

    public void FirstTimeSetup() {
        inventory.GetLevelRewards();
        Setup();
    }

    public void Setup() {
        player.gameObject.transform.position = spawnpoint;
        int[] inv = new int[(int)Item.ITEM_MAX];
        foreach (var item in LevelItems)
            inv[(int)item]++;
        inventory.AddLevelInventory(inv);
        inventory.FillLevelInventory();
    }

    public void SetPlayer(GameObject p) {
        player = p.GetComponent<PlayerController>();
        inventory = p.GetComponent<PlayerInventory>();
    } 
}
