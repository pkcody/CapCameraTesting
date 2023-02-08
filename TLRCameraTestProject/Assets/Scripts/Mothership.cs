using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mothership : MonoBehaviour
{
    public Slider h20Slider;

    public bool fullH20 = false;
    public bool window = false;
    public bool battery = false;
    public bool inhaler = false;

    private void Start()
    {
        h20Slider.gameObject.SetActive(false);
    }
    public void TryMotherShipEnd()
    {
        if(fullH20 && window && battery && inhaler)
        {
            ScenesManager.instance.ChangeToScene("Quit");
        }
    }
    public void CheckH20()
    {
        if(h20Slider.value == h20Slider.maxValue)
        {
            fullH20 = true;
            h20Slider.gameObject.SetActive(false);
            TryMotherShipEnd();
        }
    }

    //public void CheckHasParts()
    //{
    //    foreach (var ind in FindObjectsOfType<DisplayingInventory>())
    //    {
    //        for (int i = 0; i < 3; i++)
    //        {
    //            if (!item1)
    //            {
    //                if (item1obj.UIimage == ind.ItemsRow1UI[i].GetComponent<Image>().sprite || item1obj.UIimage == ind.ItemsRow2UI[i].GetComponent<Image>().sprite)
    //                {
    //                    item1 = true;
    //                }
    //            }
    //            if (!item2)
    //            {
    //                if (item2obj.UIimage == ind.ItemsRow1UI[i].GetComponent<Image>().sprite || item2obj.UIimage == ind.ItemsRow2UI[i].GetComponent<Image>().sprite)
    //                {
    //                    item2 = true;
    //                }
    //            }
    //            if (!item3)
    //            {
    //                if (item3obj.UIimage == ind.ItemsRow1UI[i].GetComponent<Image>().sprite || item3obj.UIimage == ind.ItemsRow2UI[i].GetComponent<Image>().sprite)
    //                {
    //                    item3 = true;
    //                }
    //            }

    //            if(item1 && item2 && item3)
    //            {
    //                ScenesManager.instance.ChangeToScene("Quit");
    //            }

    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Window_obj")
        {
            window = true;
            Destroy(other.gameObject);
            TryMotherShipEnd();
        }
        else if (other.name == "QuadBattery")
        {
            battery = true;
            Destroy(other.gameObject);
            TryMotherShipEnd();
        }
        else if (other.name == "InhalerReceiver")
        {
            inhaler = true;
            Destroy(other.gameObject);
            TryMotherShipEnd();
        }
    }
}
