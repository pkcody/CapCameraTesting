using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayingInventory : MonoBehaviour
{
    //Inventory
    public InventoryObject inventoryObj;
    public TextMeshProUGUI invHolderText;
    public List<GameObject> itemList = new List<GameObject>();

    public GameObject perRobotUI;

    public List<Transform> BodyPartsRowUI;
    public List<Transform> ItemsRow1UI;
    public List<Transform> ItemsRow2UI;
    public List<Transform> AbilitiesCol;

    public List<Transform> MasterList;
    public int masterIndex = 0;

    //public GameObject item_obj;

    private void Start()
    {
        inventoryObj.Container.Clear();
        //get robot UI
        perRobotUI = gameObject.GetComponent<CharacterMovement>().robotInfo;

        //Fill inv
        //body parts
        for (int i = 0; i < 3; i++)
        {
            BodyPartsRowUI.Add(perRobotUI.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(i));
        }
        for (int i = 0; i < 3; i++)
        {
            ItemsRow1UI.Add(perRobotUI.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(i));
        }
        for (int i = 0; i < 3; i++)
        {
            ItemsRow2UI.Add(perRobotUI.transform.GetChild(0).GetChild(1).GetChild(2).GetChild(i));
        }
        for (int i = 0; i < 2; i++)
        {
            AbilitiesCol.Add(perRobotUI.transform.GetChild(3).GetChild(i));
        }

        //master list
        MasterList.AddRange(BodyPartsRowUI);
        MasterList.AddRange(ItemsRow1UI);
        MasterList.AddRange(ItemsRow2UI);
        MasterList.AddRange(AbilitiesCol);
        //disable highlights
        foreach (var item in MasterList)
        {
            item.GetChild(0).gameObject.SetActive(false);
        }
        MasterList[0].GetChild(0).gameObject.SetActive(true);
    }

    public void InventoryRight()
    {
        foreach (var item in MasterList[masterIndex].gameObject.GetComponentsInChildren<Image>(true))
        {
            if (item.name == "Glow")
            {
                item.gameObject.SetActive(false);
            }
        }
        masterIndex = (masterIndex + 1) % MasterList.Count;
        foreach (var item in MasterList[masterIndex].gameObject.GetComponentsInChildren<Image>(true))
        {
            if (item.name == "Glow")
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    public void InventoryLeft()
    {
        foreach (var item in MasterList[masterIndex].gameObject.GetComponentsInChildren<Image>(true))
        {
            if(item.name == "Glow")
            {
                item.gameObject.SetActive(false);
            }
        }
        
        if (masterIndex == 0)
        {
            masterIndex = MasterList.Count - 1;
        }
        else
        {
            masterIndex -= 1;
        }
        foreach (var item in MasterList[masterIndex].gameObject.GetComponentsInChildren<Image>(true))
        {
            if (item.name == "Glow")
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    public void DropCurrentItem()
    {
        int containerIndex = -1;
        foreach (var item in inventoryObj.Container)
        {
            if (item.item.UIimage.name == MasterList[masterIndex].GetComponent<Image>().sprite.name)
            {
                containerIndex = inventoryObj.Container.IndexOf(item);
            }
        }

        if(containerIndex != -1)
        {
            ItemObject itemToRemove = inventoryObj.Container.ElementAt(containerIndex).item;
            if (itemToRemove.type == ItemType.Resources)
            {
                MasterList[masterIndex].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Red");
            }
            else if (itemToRemove.type == ItemType.BodyParts)
            {
                MasterList[masterIndex].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Red");
            }
            else if (itemToRemove.type == ItemType.AbilityScripts)
            {
                gameObject.GetComponent<CharacterMovement>().playerSpeed = 5;
                MasterList[masterIndex].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Orange");
            }
            else if (itemToRemove.type == ItemType.BuiltItem)
            {
                
                MasterList[masterIndex].gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Red");
            }

            StartCoroutine(RespawnItemWithDelayedPickup(itemToRemove));


            // Towers give player info sound and progress
            if (itemToRemove.prefab.tag == "TowerCraftingEncounter")
            {
                print("ahhhhh help");
                if (itemToRemove.UIimage.name.Contains("TowerBuild2_UI"))
                {
                    //send function 1
                    FindObjectOfType<TowerProgress>().TowerPlacedCheck(2, itemToRemove.prefab);
                    print("T2");
                    //find and destroy old
                    //show UI
                    //play sound
                }
                if (itemToRemove.UIimage.name.Contains("TowerBuild3_UI"))
                {
                    //send function 1
                    // if T2 doesnt exist then message cant place
                    FindObjectOfType<TowerProgress>().TowerPlacedCheck(2, itemToRemove.prefab);
                    //else
                    print("T3");
                        //find and destroy old
                        //show UI
                        //play sound
                }
            }

            inventoryObj.RemoveItem(itemToRemove);
        }
    }

    IEnumerator RespawnItemWithDelayedPickup(ItemObject itemToRemove)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject droppedItem = Instantiate(itemToRemove.prefab, spawnPos, Quaternion.identity, GameObject.FindGameObjectWithTag("PickupParent").transform);
        droppedItem.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(3);

        droppedItem.GetComponent<BoxCollider>().enabled = true;
    }

    public void AddToInventory(ItemObject io)
    {
        if(io.type == ItemType.Resources)
        {
            for (int i = 0; i < 3; i++)
            {
                if (ItemsRow1UI[i].GetComponent<Image>().sprite.name.Contains("Red"))
                {
                    ItemsRow1UI[i].GetComponent<Image>().sprite = io.UIimage;
                    GetComponent<RobotMessaging>().RobotSpeakResource(io);

                    return;
                }
                
            }
            for (int i = 0; i < 3; i++)
            {
                if (ItemsRow2UI[i].GetComponent<Image>().sprite.name.Contains("Red"))
                {
                    ItemsRow2UI[i].GetComponent<Image>().sprite = io.UIimage;
                    GetComponent<RobotMessaging>().RobotSpeakResource(io);
                    return;
                }

            }
        }
        else if (io.type == ItemType.BodyParts)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BodyPartsRowUI[i].GetComponent<Image>().sprite.name.Contains("Red"))
                {
                    BodyPartsRowUI[i].GetComponent<Image>().sprite = io.UIimage;
                    return;
                }

            }
        }
        else if (io.type == ItemType.AbilityScripts)
        {
            for (int i = 0; i < 3; i++)
            {
                if (AbilitiesCol[i].GetComponent<Image>().sprite.name.Contains("Orange"))
                {
                    AbilitiesCol[i].GetComponent<Image>().sprite = io.UIimage;
                    if (io.UIimage.name.Contains("Blue"))
                    {
                        gameObject.GetComponent<CharacterMovement>().playerSpeed = 10;
                    }
                    return;
                }

            }
        }


    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Item>(out _))
        {
            itemList.Clear();
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Item>(out _))
        {
            itemList.Add(collision.gameObject);

            foreach (GameObject obj in itemList)
            {
                Item item = obj.GetComponent<Item>();
                if(item.item.type == ItemType.Resources)
                {
                    inventoryObj.AddItem(item.item, 1); // change 1 to amount
                    AddToInventory(item.item);
                    Destroy(collision.gameObject);
                }
                else if (item.item.type == ItemType.BodyParts)
                {
                    inventoryObj.AddItem(item.item, 1); // only makes 1 at time
                    AddToInventory(item.item);
                    Destroy(collision.gameObject);
                }
                else if (item.item.type == ItemType.AbilityScripts)
                {
                    inventoryObj.AddItem(item.item, 1); // only 1 of a type
                    AddToInventory(item.item);
                    Destroy(collision.gameObject);
                }
            }
            itemList.Clear();
            
        }
    }
}
