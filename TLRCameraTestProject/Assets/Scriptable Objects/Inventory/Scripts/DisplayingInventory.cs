using System.Collections;
using System.Collections.Generic;
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
                    return;
                }
                
            }
            for (int i = 0; i < 3; i++)
            {
                if (ItemsRow2UI[i].GetComponent<Image>().sprite.name.Contains("Red"))
                {
                    ItemsRow2UI[i].GetComponent<Image>().sprite = io.UIimage;
                    return;
                }

            }
        }
        else if (io.type == ItemType.BodyParts)
        {
            for (int i = 0; i < 3; i++)
            {
                if (AbilitiesCol[i].GetComponent<Image>().sprite.name.Contains("Red"))
                {
                    AbilitiesCol[i].GetComponent<Image>().sprite = io.UIimage;
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
