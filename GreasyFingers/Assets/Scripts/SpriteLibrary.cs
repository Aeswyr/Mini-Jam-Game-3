using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteLibrary", menuName = "ScriptableObjects/SpriteLibrary", order = 1)]
public class SpriteLibrary : ScriptableObject
{
    
    [SerializeField] private Sprite[] sprites;
    public Sprite Get(int index){
        return sprites[index];
    }
}
