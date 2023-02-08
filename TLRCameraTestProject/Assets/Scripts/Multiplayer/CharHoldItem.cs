using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharHoldItem : MonoBehaviour
{
    public Transform holdSpot;
    public GameObject currentHold;

    //Encounter - window
    [Header("Window")]
    public GameObject Window_obj;
    public bool windowInRange;

    public void HoldPlease(Transform got)
    {
        if (currentHold == null)
        {
            GetComponent<CharacterMovement>().animator.SetBool("isHold", true);

            Vector3 oldRot = got.localEulerAngles;
            got.position = holdSpot.position;
            got.SetParent(holdSpot);
            got.localEulerAngles = Vector3.zero;
            print(got);
            currentHold = got.gameObject;

            if (got.childCount == 1)
            { 
                if(got.GetChild(0).TryGetComponent<LineController>(out LineController lineController))
                {
                    lineController.StartLine();
                }
            }

            if (got.name.Contains("broken"))
            {
                got.transform.localEulerAngles = new Vector3(-90, 0, 0);
            }
            if (got.name.Contains("Window"))
            {
                got.transform.localEulerAngles = new Vector3(-90, 0, 0);
            }
            if(got.name.Contains("Inhale"))
            {
                FindObjectOfType<Mothership>().h20Slider.gameObject.SetActive(true);
            }
        }
    }

    public void WindowEncounter()
    {
        foreach (var item in Window_obj.GetComponentsInChildren<Transform>(true))
        {
            if (currentHold != null)
            {
                if (item.name.Contains(currentHold.name))
                {
                    item.gameObject.SetActive(true);
                    Destroy(currentHold);
                    GetComponent<CharacterMovement>().inRangeHold = false;
                    Window_obj.GetComponent<WindowFull>().AskFull();
                    currentHold = null;

                    GetComponent<CharacterMovement>().animator.SetBool("isHold", false);


                    break;
                }
            }

        }
    }

    public void OnDropHeldItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (currentHold != null)
            {
                DropCurrentHeldItem(currentHold);
                GetComponent<CharacterMovement>().animator.SetBool("isHold", false);
                currentHold = null;
            }
        }

    }

    private void DropCurrentHeldItem(GameObject goToDrop)
    {
        if (goToDrop.transform.childCount == 1)
        {
            if (goToDrop.transform.GetChild(0).TryGetComponent<LineController>(out LineController lineController))
            {
                lineController.DisableLine();
            }
        }

        goToDrop.transform.SetParent(null);
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        goToDrop.transform.position = spawnPos;
        currentHold = null;

    }

    public void OnTransmitToInhaler(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (currentHold.name.Contains("InhalerTransmitter"))
            {
                foreach (GameObject player in PlayerSpawning.instance.players)
                {
                    if (player != null)
                    {
                        if (player.GetComponent<CharHoldItem>().currentHold != null)
                        {
                            print(player.GetComponent<CharHoldItem>().currentHold.name);
                            if (player.GetComponent<CharHoldItem>().currentHold.name.Contains("InhalerReceiver"))
                            {
                                player.GetComponent<CharHoldItem>().currentHold.GetComponent<CloudInhaler>().InhaleClouds();
                                print("Trying to inhale");
                            }
                        }
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.GetComponent<Collider>().tag == "FixItEncounter")
        {
            windowInRange = false;

        }



    }

    public void OnTriggerEnter(Collider collision)
    {
        //print(collision.name);
        if (collision.GetComponent<Collider>().tag == "FixItEncounter")
        {
            Window_obj = collision.gameObject;
            windowInRange = true;
            WindowEncounter();
        }



    }
}
