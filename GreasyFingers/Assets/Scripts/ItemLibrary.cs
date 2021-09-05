using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemLibrary", menuName = "ScriptableObjects/ItemLibrary", order = 1)]
public class ItemLibrary : ScriptableObject
{
    [SerializeField] private DisplayPair[] images;

    public Sprite GetFull(Item item) {
        return images[(int)item].full;
    }

    public Sprite GetEmpty(Item item) {
        return images[(int)item].empty;
    }
}

[Serializable] public struct DisplayPair {
    public Sprite full, empty;
}
