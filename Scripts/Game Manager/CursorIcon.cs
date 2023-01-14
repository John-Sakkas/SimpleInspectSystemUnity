using UnityEngine;
using UnityEngine.UI;

public class CursorIcon : MonoBehaviour
{
    public Sprite[] Cursor_Icons;
    public Image CursorRend;
    public Vector3 DefaultPos;

    public bool IsComplete;
    public bool _startMove;
  
    private void Start()
    {
        _startMove = false;
        IsComplete = false;
        DefaultPos = CursorRend.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InspectSystem.MouseOn = false;
        ChangeSceneReaction.MouseOn = false;
        AnimationReaction.MouseOn = false;
        pickUp.MouseOn = false;
        PointOfInterest.MouseOn = false;
    }


    // Update is called once per frame
    //void Update()
    //{
    //    Cursor.visible = false;

    //    Ray RayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Physics.Raycast(RayOrigin, out hit, 50f))
    //    {
    //        if (hit.collider.CompareTag("pickUp") && ExamineSystem.ExamineMode)
    //        {
    //            pickUp.MouseOn = true;
    //        }
    //        else if (hit.collider.CompareTag("pointOfInterest") && ExamineSystem.ExamineMode)
    //        {
    //            PointOfInterest.MouseOn = true;
    //        }
    //        else if (hit.collider.CompareTag("Interaction"))
    //        {
    //            if (hit.collider.GetComponent<AnimationReaction>() != null)
    //            {
    //                if (hit.collider.GetComponent<AnimationReaction>().InteractionComplete)
    //                    IsComplete = true;
    //            }
    //            else
    //                IsComplete = false;

    //        }
    //        else
    //        {
    //            pickUp.MouseOn = false;
    //            PointOfInterest.MouseOn = false;
    //        }
    //    }
    //    else 
    //    {
    //        pickUp.MouseOn = false;
    //        PointOfInterest.MouseOn = false;
    //    }

    //    if (ExamineSystem.ExamineMode)
    //    {
    //        if (pickUp.MouseOn)
    //            ChangeMouseIcon(CursorLockMode.None, Input.mousePosition, Color.white, 3);            
    //        else if (PointOfInterest.MouseOn)
    //            ChangeMouseIcon(CursorLockMode.None, Input.mousePosition, Color.white, 4);
    //        else
    //            ChangeMouseIcon(CursorLockMode.None, Input.mousePosition, Color.white, 0);
    //    }
    //    else if (PlayerInventory.InventoryIsOn)
    //        ChangeMouseIcon(CursorLockMode.None, Input.mousePosition, Color.black, 0);
    //    else if (!InspectSystem.MouseOn && !ChangeSceneReaction.MouseOn && !AnimationReaction.MouseOn && !pickUp.MouseOn && !PointOfInterest.MouseOn)
    //        ChangeMouseIcon(CursorLockMode.Locked, DefaultPos, Color.white, 5);
    //}

    public void Update()
    {
        if(_startMove)
            CursorRend.transform.position = Input.mousePosition;
        else
            CursorRend.transform.position = DefaultPos;

    }

    public void ChangeMouseIcon(CursorLockMode lockMode, bool startMoving, Color cursorColor, int cursorIcon) 
    {
        Cursor.lockState = lockMode;
        _startMove = startMoving;
        CursorRend.color = cursorColor;
        CursorRend.sprite = Cursor_Icons[cursorIcon];
    }

    public void ChangeIcon(GameObject Gobject)
    {
        if (Gobject.CompareTag("Examine") && !ExamineSystem.ExamineMode && !PlayerInventory.InventoryIsOn)
            CursorRend.sprite = Cursor_Icons[1];
        else if (Gobject.CompareTag("Interaction") && !ExamineSystem.ExamineMode && !PlayerInventory.InventoryIsOn)
        {
            if(IsComplete)
                CursorRend.sprite = Cursor_Icons[5];
            else
                CursorRend.sprite = Cursor_Icons[2];
        }
        else if (Gobject.CompareTag("pickUp"))
            CursorRend.sprite = Cursor_Icons[3];
        else if (Gobject.CompareTag("pointOfInterest"))
            CursorRend.sprite = Cursor_Icons[4];
        else if (ExamineSystem.ExamineMode)
            CursorRend.sprite = Cursor_Icons[0];
        else if(Gobject == null)
            CursorRend.sprite = Cursor_Icons[5];

    }

    public void ChangeToDefault() 
    {
        CursorRend.sprite = Cursor_Icons[5];
    }
}
