using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public  int maxSummons = 5;
    private int numSummons = 0;

    private List<GameObject> summons;
    public GameObject wallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        summons = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = summons.Count - 1; i >= 0; i--) {
            GameObject summon = summons[i];
        //foreach (GameObject summon in summons) {
            if (summon.GetComponent<WallSummon>().expired) {
                summons.Remove(summon);
                Destroy(summon);
                numSummons--;
            }
        } 
    }

    public bool addSummon(Vector3 pos) {
        if (numSummons < maxSummons) {
            summons.Add(Instantiate(wallPrefab, pos, Quaternion.identity));
            numSummons++;
            return true;
        }
        return false;
    }
}
