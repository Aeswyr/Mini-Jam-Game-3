using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int[] inventory = new int[(int)Item.ITEM_MAX];
    private int[] levelInventory = new int[(int)Item.ITEM_MAX];
    private int[] staticLevelInventory = new int[(int)Item.ITEM_MAX];

    public void Add(Item item, int count = 1) {
        inventory[(int)item] += count;
    }

    public void AddLevelInventory(int[] inv) {
        staticLevelInventory = inv;
    }

    public void FillLevelInventory() {
        for (int i = 0; i < staticLevelInventory.Length; i++)
            levelInventory[i] = staticLevelInventory[i];
    }

    /**
    * returns false if you try to remove more items than exist in the inventory,
    * and fails to remove any items.
    * returns true if removal is succesful
    */
    public bool Remove(Item item, int count = 1) {
        if (inventory[(int)item] + levelInventory[(int)item] < count)
            return false;
        levelInventory[(int)item] -= count;
        if (levelInventory[(int)item] < 0) {
            inventory[(int)item] += levelInventory[(int)item];
            levelInventory[(int)item] = 0;
        }
        return true;
    }

    public int Get(Item item) {
        return inventory[(int)item] + levelInventory[(int)item];
    }

    public int GetLevelItem(Item item) {
        return levelInventory[(int)item];
    }

    public int GetStaticLevelItem(Item item) {
        return staticLevelInventory[(int)item];
    }

    public void GetLevelRewards() {
        for (int i = 0; i < (int)Item.ITEM_MAX; i++)
        {
            inventory[i] += levelInventory[i];
            levelInventory[i] = 0;
        }
    }

}

public enum Item {
    Default, Key, Life, Dash, Superdash, Doublejump, Bookbomb, Platform, Reaper, Hookshot, ITEM_MAX
}
