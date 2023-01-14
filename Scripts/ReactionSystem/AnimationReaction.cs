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
        if (PlayerInventory.PlayerItems.Count != 0 && !InteractionComplete) // elexnei an iparxoun items sto inventory kai den exei gine complete to interaction
        {
            for (int i = 0; i < PlayerInventory.PlayerItems.Count; i++) // gia ola ta antikeimena sto inventory
            {
                for (int y = 0; y < ConditionItem.Count; y++) // gia ola ta antikeimena sto condition
                {
                    if (PlayerInventory.PlayerItems[i].name == ConditionItem[y].name && !InteractionComplete) //an yparxoun kai den exei gine complete
                    {
                        item_counter++; // afksise to counter 
                    }
                    else
                    {
                        notItemFind = true; // allios kati lipi
                    }
                }
            }

            if (item_counter == ConditionItem.Count) // an iparxoun ola ta antikeimena
            {
                notItemFind = false; 


                InteractionComplete = true; // to interaction egine 
                InteractioGameobject_Done.SetActive(true);
                Interaction_Animation.SetBool(Interaction_Animation.parameters[0].name, true); // trekse to animation

                Player_UIText.instance.DisplayUI(Done_Text);

                for (int i = 0; i < ConditionItem.Count; i++)
                {
                    for (int y = 0; y < PlayerInventory.PlayerItems.Count; y++)
                    {
                        if (PlayerInventory.PlayerItems[y].name == ConditionItem[i].name)
                        {
                            PlayerInventory.DeleteItem(PlayerInventory.PlayerItems[y]); // bres ta antikeimena sto inventory kai svista
                        }
                    }
                }
            }

            if (notItemFind) //an lipi kati 
            {
                Player_UIText.instance.DisplayUI(Missing_text);
                notItemFind = false;
            }

        }
        else if (!InteractionComplete) // an den exei kanena antikeimeno sto inventory
        {
            Player_UIText.instance.DisplayUI("I dont have any item to try something!!!!!");
        }

    }
}
