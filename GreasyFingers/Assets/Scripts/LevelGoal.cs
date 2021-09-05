using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private GameObject nextLevel;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<PlayerController>() != null) {
            GameObject lvl = Instantiate(nextLevel);
            lvl.GetComponent<LevelData>().SetPlayer(collider.gameObject);
            lvl.GetComponent<LevelData>().FirstTimeSetup();
            Destroy(gameObject.transform.parent.parent.gameObject);
        }
    }
}
