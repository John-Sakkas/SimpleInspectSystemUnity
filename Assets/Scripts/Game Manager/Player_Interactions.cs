using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Interactions : MonoBehaviour
{
    private ExamineSystem examineSystem;
    private PlayerInventory playerInventory;
    private pickUp _pickUp;
    private CursorIcon _cursorIcon;
    private float time;
    private float timer;

    public float ExamineDistance;
    public float InteractionDistance;
    public GameObject Examine_Object;

    public void Start()
    {
        time = 0.5f;
        _pickUp = FindObjectOfType<pickUp>();
        examineSystem = FindObjectOfType<ExamineSystem>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        _cursorIcon = FindObjectOfType<CursorIcon>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray RayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        #region Examine System
        if (Input.GetKeyDown(KeyCode.E) && !ExamineSystem.ExamineMode && !PlayerInventory.InventoryIsOn)
        {
            if (Physics.Raycast(RayOrigin, out hit, ExamineDistance))
            {
                if (hit.collider.CompareTag("Examine"))
                {
                    Examine_Object = hit.collider.gameObject;
                    examineSystem.ExamineAction(Examine_Object);
                    _cursorIcon.ChangeMouseIcon(CursorLockMode.None, true, Color.white, 5);

                }
                else if (hit.collider.CompareTag("pickUp")) 
                {
                    Examine_Object = hit.collider.gameObject;
                    _pickUp.pickUpSystem(hit.collider.gameObject);
                    _cursorIcon.ChangeMouseIcon(CursorLockMode.Locked, false, Color.white, 5);

                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && Examine_Object.GetComponent<InspectSystem>().Pickup && ExamineSystem.ExamineMode)
        {
            if (!Examine_Object.GetComponent<InspectSystem>().has_pickup)
            {
                examineSystem.ExamineActionPickUp();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && ExamineSystem.ExamineMode) 
        {
            examineSystem.ResetTransform();
        }
        #endregion

        timer += Time.deltaTime;
        if (timer >= time)
        {
            if (Input.GetKey(KeyCode.I) && !ExamineSystem.ExamineMode && !TooltipTrigger.EximaneFromInventory)
            {
                playerInventory.InvetoryAction();
                timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !PlayerInventory.InventoryIsOn && !ExamineSystem.ExamineMode)
        {
            if (Physics.Raycast(RayOrigin, out hit, InteractionDistance))
            {
                if (hit.collider.CompareTag("Interaction"))
                {
                    if (hit.collider.GetComponent<AnimationReaction>() != null)
                    {
                        hit.collider.GetComponent<AnimationReaction>().StartAnimationReaction();
                    }
                    if (hit.collider.GetComponent<ChangeSceneReaction>() != null)
                    {
                        hit.collider.GetComponent<ChangeSceneReaction>().StartChangeScene();
                    }
                }
            }
        }
    }
}
