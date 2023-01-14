using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject PlayerController;


    public List<GameObject> PlayerItems;
    private CursorIcon _cursorIcon;

    [SerializeField] GameObject Inventory_UI;

    [SerializeField] GameObject UI_Body;

    [SerializeField] GameObject Item_Slot;
    [SerializeField] GameObject Item_Image;

    public static bool InventoryIsOn = false;

    public void Start()
    {
        _cursorIcon = FindObjectOfType<CursorIcon>();

        PlayerController = GameObject.FindGameObjectWithTag("Player");
    }

    public void InvetoryAction() 
    {
        if (!InventoryIsOn)
        {
            _cursorIcon.ChangeMouseIcon(CursorLockMode.None, true, Color.black, 5);

            PlayerController.GetComponent<FirstPersonController>().enabled = false;
            Inventory_UI.SetActive(true);
            InventoryIsOn = true;
        }
        else
        {
            _cursorIcon.ChangeMouseIcon(CursorLockMode.Locked, false, Color.white, 5);

            PlayerController.GetComponent<FirstPersonController>().enabled = true;
            TooltipSystem.Hide();
            Inventory_UI.SetActive(false);
            InventoryIsOn = false;
        }
    }



    public void AddToInventory(GameObject itemToAdd)
    {
        PlayerItems.Add(itemToAdd);
        UpdateCanvas(itemToAdd.GetComponent<InspectSystem>().Item_icon,itemToAdd.name , itemToAdd);
    }

    public void UpdateCanvas(Texture Image,string itemName , GameObject item) 
    {
        GameObject item_Slot = Instantiate(Item_Slot, UI_Body.transform);
        item_Slot.name = itemName;
        item_Slot.GetComponent<TooltipTrigger>().content = item.GetComponent<InspectSystem>().Item_Discription;
        item_Slot.GetComponent<TooltipTrigger>().header = item.GetComponent<InspectSystem>().Item_Title;
        item_Slot.GetComponent<TooltipTrigger>().itemToInspect = item;

        GameObject item_image = Instantiate(Item_Image, item_Slot.transform);
        item_image.GetComponent<RawImage>().texture = Image;
    }

    public void DeleteItem(GameObject itemtoDelete) 
    {
        PlayerItems.Remove(itemtoDelete);
        for (int i = 0; i < UI_Body.transform.childCount; i++)
        {
            if (UI_Body.GetComponent<Transform>().GetChild(i).gameObject.name == itemtoDelete.name) 
            {
                Destroy(UI_Body.GetComponent<Transform>().GetChild(i).gameObject);
                Debug.LogWarning("Delete" + itemtoDelete.name);
            }
        }
    }
}
