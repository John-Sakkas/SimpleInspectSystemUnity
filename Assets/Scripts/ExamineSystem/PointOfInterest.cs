using System.Collections;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    private PlayerInventory PlayerInventory;
    private CursorIcon Icon;
    private bool notItemFind;

    public string content;
    public bool needItem;
    public bool ItemIsFound;
    public static bool MouseOn;

    public PointOfInterest instance;
    public GameObject neededItem;
    public Animator animator;


    public void Awake()
    {
        notItemFind = false;
        ItemIsFound = false;
        instance = this;
        MouseOn = false;
        Icon = FindObjectOfType<CursorIcon>();
        PlayerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void Update()
    {
        Ray RayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(RayOrigin, out hit, 1000f))
        {
            if (hit.collider.CompareTag("pointOfInterest"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CheckForNeededItem();
                }
            }
        }
        StartCoroutine(FadeIn_Out());
    }

    IEnumerator FadeIn_Out() 
    {
        if (gameObject.GetComponent<MeshRenderer>().material.color.a == 0f)
        {
            LeanTween.alpha(gameObject, 0.5f, 1.5f);
        }
        else if (gameObject.GetComponent<MeshRenderer>().material.color.a == 0.5f)
        {
            LeanTween.alpha(gameObject, 0f, 1.5f);
        }

        yield return new WaitForSeconds(0.01f);
    }

    #region CheckForItems
    public void CheckForNeededItem() 
    {
        if (!ItemIsFound)
        {
            if (PlayerInventory.PlayerItems.Count != 0)
            {
                for (int i = 0; i < PlayerInventory.PlayerItems.Count; i++) // Check for every item in the inventory
                {
                    if (PlayerInventory.PlayerItems[i].name == neededItem.name) //if the task is not complete go in 
                    {
                        notItemFind = false;
                        Player_UIText.instance.DisplayUI("OPEN THE BOX"); // Message for the user
                        ItemIsFound = true;

                        animator.enabled = true;


                        for (int y = 0; y < PlayerInventory.PlayerItems.Count; y++)
                        {
                            if (PlayerInventory.PlayerItems[y].name == neededItem.name)
                            {
                                PlayerInventory.DeleteItem(PlayerInventory.PlayerItems[y]); //Delete the items 
                            }
                        }

                        break;
                    }
                    else
                    {
                        notItemFind = true;
                    }
                }
            }
            else
            {
                Player_UIText.instance.DisplayUI("No items to inventory");
            }            

            if (notItemFind) //If some items are missing
            {
                Player_UIText.instance.DisplayUI(content);
                notItemFind = false;
            }
        }
        else 
        {
            Player_UIText.instance.DisplayUI("i already done that!");
        }

    }
    #endregion

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

