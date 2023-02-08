using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProgress : MonoBehaviour
{
    public GameObject[] towerList = new GameObject[3];
    public GameObject tower1; // this is changing for random spawn bc of clones (clone wars)
    public List<GameObject> towerWaves;
    public List<GameObject> towerProgress;
    
    public GameObject YayTowerUI;

    private void Start()
    {
        towerList[0] = tower1;
    }

    public void TowerPlacedCheck(int num, GameObject tower)
    {
        int index = num - 1;
        // maybe check which clone
        print("index" + index);
        if(towerList[index] == null)
        {
            print("playing info");
            PlayTowerInfo(num, tower);
        }
        else if(num == 2 && towerList[index] != null && towerList[index + 1] == null)
        {
            PlayTowerInfo(2, tower);
        }
        else
        {
            //send to destroy old
            //Destroy(towerList[index]);
            //PlayTowerInfo(num, tower);
        }
        
    }

    public void PlayTowerInfo(int num, GameObject tower)
    {
        int index = num - 1;
        
        if (num == 2)
        {
            // Play tower 2 info
            print("tower scream 2");
            towerList[index] = tower;
            YayTowerUI.SetActive(true);
            towerWaves[index].SetActive(true);
            towerProgress[index].SetActive(true);
            FindObjectOfType<TowerSoundEffect>().PlayTower2Sound();
            FindObjectOfType<Mothership>().tower2 = true;
            FindObjectOfType<Mothership>().TryMotherShipEnd();

            Invoke("HideParents", 5f);

        }
        if (num == 2 && towerList[index] != null && towerList[index + 1] == null)
        {
            // Play tower 3 info
            print("tower scream 3");
            towerList[index + 1] = tower;
            YayTowerUI.SetActive(true);
            towerWaves[index + 1].SetActive(true);
            towerProgress[index + 1].SetActive(true);
            FindObjectOfType<TowerSoundEffect>().PlayTower3Sound();
            FindObjectOfType<Mothership>().tower3 = true;
            FindObjectOfType<Mothership>().TryMotherShipEnd();

            Invoke("HideParents", 5f);

        }
        else if (num == 3 && towerList[index-1] == null)
        {
            // Play tower 2 info
            print("tower scream 2");
            towerList[index-1] = tower;
            YayTowerUI.SetActive(true);
            towerWaves[index-1].SetActive(true);
            towerProgress[index-1].SetActive(true);
            FindObjectOfType<TowerSoundEffect>().PlayTower2Sound();
            FindObjectOfType<Mothership>().tower2 = true;
            FindObjectOfType<Mothership>().TryMotherShipEnd();

            Invoke("HideParents", 5f);

        }
        else if (num == 3)
        {
            // Play tower 3 info
            print("tower scream 3");
            towerList[index] = tower;
            YayTowerUI.SetActive(true);
            towerWaves[index].SetActive(true);
            towerProgress[index].SetActive(true);
            towerWaves[index - 1].SetActive(false);
            towerProgress[index - 1].SetActive(false);

            FindObjectOfType<TowerSoundEffect>().PlayTower3Sound();
            FindObjectOfType<Mothership>().tower3 = true;
            FindObjectOfType<Mothership>().TryMotherShipEnd();

            Invoke("HideParents", 5f);

        }
    }

    public void HideParents()
    {
        YayTowerUI.SetActive(false);
    }
}
