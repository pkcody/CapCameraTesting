using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProgress : MonoBehaviour
{
    public GameObject[] towerList = new GameObject[3];
    public GameObject tower1; // this is changing for random spawn bc of clones (clone wars)

    private void Start()
    {
        towerList[0] = tower1;
    }

    public void TowerPlacedCheck(int num, GameObject tower)
    {
        int index = num--;
        // maybe check which clone
        if(towerList[index] == null)
        {
            PlayTowerInfo(num, tower);
        }
        else
        {
            //send to destroy old
            Destroy(towerList[index]);
            PlayTowerInfo(num, tower);
        }
        
    }

    public void PlayTowerInfo(int num, GameObject tower)
    {
        int index = num--;
        towerList[index] = tower;
        if (index == 2)
        {
            // Play tower 2 info
            print("tower scream 2");
        }
        else if (index == 3)
        {
            // Play tower 3 info
            print("tower scream 3");
        }
    }
}
