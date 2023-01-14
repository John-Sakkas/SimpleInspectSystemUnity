using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnimationReaction : MonoBehaviour
{
    [SerializeField] private List<GameObject> ConditionItem;
    [SerializeField] private PlayerInventory PlayerInventory;
    [SerializeField] private Animator Interaction_Animation;

    [TextArea(5, 5)] public string Done_Text;
    [TextArea(5, 5)] public string Missing_text;

    public int item_counter = 0;

    [SerializeField] private CursorIcon Icon;
    public static bool MouseOn = false;

    public bool InteractionComplete = false;
    public GameObject InteractioGameobject_Done;

    private bool notItemFind = false;

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

    public void StartAnimationReaction() 
    {
        StopAllCoroutines();
        item_counter = 0;
        if (PlayerInventory.PlayerItems.Count != 0 && !InteractionComplete) // checks for items in the inventory and if the interaction is false
        {
            for (int i = 0; i < PlayerInventory.PlayerItems.Count; i++) // checks the items in the inventory
            {
                for (int y = 0; y < ConditionItem.Count; y++) // items for the condition
                {
                    if (PlayerInventory.PlayerItems[i].name == ConditionItem[y].name && !InteractionComplete) //if the items exist and the condition is false
                    {
                        item_counter++; //increase the counter 
                    }
                    else
                    {
                        notItemFind = true; // else no item
                    }
                }
            }

            if (item_counter == ConditionItem.Count) // if the condition is true you have all the items
            {
                notItemFind = false; 
                InteractionComplete = true; // interaction complete 
                InteractioGameobject_Done.SetActive(true);
                Interaction_Animation.SetBool(Interaction_Animation.parameters[0].name, true); //tun the animation

                Player_UIText.instance.DisplayUI(Done_Text);

                for (int i = 0; i < ConditionItem.Count; i++)
                {
                    for (int y = 0; y < PlayerInventory.PlayerItems.Count; y++)
                    {
                        if (PlayerInventory.PlayerItems[y].name == ConditionItem[i].name)
                        {
                            PlayerInventory.DeleteItem(PlayerInventory.PlayerItems[y]); //delete the items in the inventory
                        }
                    }
                }
            }

            if (notItemFind) //is you miss an item 
            {
                Player_UIText.instance.DisplayUI(Missing_text);
                notItemFind = false;
            }

        }
        else if (!InteractionComplete) //if you dont have any item
        {
            Player_UIText.instance.DisplayUI("I dont have any item to try something!!!!!");
        }

    }
}
