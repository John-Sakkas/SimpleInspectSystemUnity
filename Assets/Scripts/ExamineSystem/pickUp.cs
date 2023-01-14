using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    public string pickUp_Text;
    private PlayerInventory PlayerController;
    private CursorIcon Icon;
    public static bool MouseOn = false;

    public void Start()
    {
        PlayerController = FindObjectOfType<PlayerInventory>();
        Icon = FindObjectOfType<CursorIcon>();
    }

    public void pickUpSystem(GameObject item)
    {
        PlayerController.AddToInventory(item);
        item.GetComponent<InspectSystem>().has_pickup = true;
        Player_UIText.displayDone = false;
        Player_UIText.instance.DisplayUI(pickUp_Text);
        item.SetActive(false);
        this.enabled = false;
    }
    public void OnMouseOver()
    {
        Transform camera = Camera.main.transform;
        float dist = Vector3.Distance(camera.position, transform.position); //This is your distance
        if (dist <= 2)
        {
            MouseOn = true;
            Icon.ChangeIcon(this.gameObject);
        }
        else
        {
            MouseOn = false;
            Icon.ChangeToDefault();
        }
    }

    public void OnMouseExit()
    {
        MouseOn = false;
        Icon.ChangeToDefault();
    }
}
