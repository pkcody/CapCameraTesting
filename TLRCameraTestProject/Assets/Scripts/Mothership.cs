using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mothership : MonoBehaviour
{
    public bool item1;
    public bool item2;
    public bool item3;


    public ItemObject item1obj;
    public ItemObject item2obj;
    public ItemObject item3obj;

    public void CheckHasParts()
    {
        foreach (var ind in FindObjectsOfType<DisplayingInventory>())
        {
            for (int i = 0; i < 3; i++)
            {
                if (!item1)
                {
                    if (item1obj.UIimage == ind.ItemsRow1UI[i].GetComponent<Image>().sprite || item1obj.UIimage == ind.ItemsRow2UI[i].GetComponent<Image>().sprite)
                    {
                        item1 = true;
                    }
                }
                if (!item2)
                {
                    if (item2obj.UIimage == ind.ItemsRow1UI[i].GetComponent<Image>().sprite || item2obj.UIimage == ind.ItemsRow2UI[i].GetComponent<Image>().sprite)
                    {
                        item2 = true;
                    }
                }
                if (!item3)
                {
                    if (item3obj.UIimage == ind.ItemsRow1UI[i].GetComponent<Image>().sprite || item3obj.UIimage == ind.ItemsRow2UI[i].GetComponent<Image>().sprite)
                    {
                        item3 = true;
                    }
                }

                if(item1 && item2 && item3)
                {
                    ScenesManager.instance.ChangeToScene("Quit");
                }

            }
        }



    }
}
