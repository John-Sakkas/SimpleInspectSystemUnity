using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.Build.Content;

public class ChangeSceneReaction : MonoBehaviour
{
    [HideInInspector] public bool NeedItems = false;
    [HideInInspector] public bool NeedItemsFoldOut = false;
    [HideInInspector] public bool InteractionObjectFoldOut = false;

    [HideInInspector] public List<GameObject> ConditionItem;
    [HideInInspector] public List<GameObject> ReactionDoneItems;

    [SerializeField] private PlayerInventory PlayerInventory;



    private int item_counter = 0;
    private bool notItemFind = false;
    public int SceneInt;

    [TextArea(5, 5)] public string Done_Text;
    [TextArea(5, 5)] public string Missing_text;

    [SerializeField] private CursorIcon Icon;
    [SerializeField] public static bool MouseOn = false;

    public void Start()
    {
        PlayerInventory = FindObjectOfType<PlayerInventory>();
        Icon = FindObjectOfType<CursorIcon>();
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



    public void StartChangeScene() 
    {
        //na allazi i ikona tou cursor se mia porta me ena belaki

        item_counter = 0;
        StopAllCoroutines();
        if (NeedItems)
        {
            if (PlayerInventory.PlayerItems.Count != 0) // elexnei an iparxoun items sto inventory kai den exei gine complete to interaction
            {
                for (int i = 0; i < PlayerInventory.PlayerItems.Count; i++) // gia ola ta antikeimena sto inventory
                {
                    for (int y = 0; y < ConditionItem.Count; y++) // gia ola ta antikeimena sto condition
                    {
                        if (PlayerInventory.PlayerItems[i].name == ConditionItem[y].name) //an yparxoun kai den exei gine complete
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
                    SceneManager.LoadScene(1);
                    Player_UIText.instance.DisplayUI(Done_Text);
                }

                if (notItemFind) //an lipi kati 
                {
                    Player_UIText.instance.DisplayUI(Missing_text);
                    notItemFind = false;
                }
            }
            else // an den exei kanena antikeimeno sto inventory
            {
                Player_UIText.instance.DisplayUI("I dont have any item to try something!!!!!");
            }
        }
        else 
        {
            for (int i = 0; i < ReactionDoneItems.Count; i++)
            {
                if(ReactionDoneItems[i].activeInHierarchy)
                    item_counter++;
            }


            if (item_counter == ReactionDoneItems.Count)
            {
                SceneManager.LoadScene(1);
                Player_UIText.instance.DisplayUI(Done_Text);
            }
            else // an den exei kanena antikeimeno sto inventory
            {
                Player_UIText.instance.DisplayUI("I dont have any item to try something!!!!!");
            }
        }
    }
}

[CustomEditor(typeof(ChangeSceneReaction))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ChangeSceneReaction ItemsIsNeeded = (ChangeSceneReaction) target;

        ItemsIsNeeded.NeedItems = EditorGUILayout.Toggle("Inventory Items ", ItemsIsNeeded.NeedItems);

        InventoryItemsShow();
        ReactionConditionShow();

        #region InventoryItemsShow
        void InventoryItemsShow()
        {
            if (ItemsIsNeeded.NeedItems)
                GUI.enabled = true;
            else
                GUI.enabled = false;


            ItemsIsNeeded.NeedItemsFoldOut = EditorGUILayout.Foldout(ItemsIsNeeded.NeedItemsFoldOut, "Inventory Items ", true);
            if (ItemsIsNeeded.NeedItemsFoldOut)
            {
                EditorGUI.indentLevel++;

                List<GameObject> list = ItemsIsNeeded.ConditionItem;
                int size = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count));

                while (size > list.Count)
                {
                    list.Add(null);
                }

                while (size < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }

                for (int i = 0; i < size; i++)
                {
                    list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(GameObject), true) as GameObject;
                }
                EditorGUI.indentLevel--;
            }
        }
        #endregion

        #region ReactionConditionShow
        void ReactionConditionShow()
        {
            if (!ItemsIsNeeded.NeedItems)
                GUI.enabled = true;
            else
                GUI.enabled = false;
            ItemsIsNeeded.InteractionObjectFoldOut = EditorGUILayout.Foldout(ItemsIsNeeded.InteractionObjectFoldOut, "Interaction Object State", true);

            if (ItemsIsNeeded.InteractionObjectFoldOut) 
            {
                EditorGUI.indentLevel++;
                List<GameObject> list = ItemsIsNeeded.ReactionDoneItems;
                int size = Mathf.Max(0, EditorGUILayout.IntField("Size", list.Count));

                while (size > list.Count)
                {
                    list.Add(null);
                }

                while (size < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }

                for (int i = 0; i < size; i++)
                {
                    list[i] = EditorGUILayout.ObjectField("Element " + i, list[i], typeof(GameObject), true) as GameObject;
                }
                EditorGUI.indentLevel--;
            }
        }
        #endregion
    }
}
