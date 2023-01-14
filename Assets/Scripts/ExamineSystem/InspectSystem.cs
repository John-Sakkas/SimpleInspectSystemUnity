using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InspectSystem : MonoBehaviour
{
    private ExamineSystem ExamineSystem;
    private Camera ExamineCamera;
    private CursorIcon Icon;
    public TMP_Text Item_title_Text;
    public TMP_Text Item_Description_Text;

    [SerializeField] private List<GameObject> PointOfInterestList;

    public bool loadPoints = false;
    public static bool MouseOn = false;

    public Texture Item_icon;
    public bool has_pickup;
    public bool Pickup;

    public float DefaultZoom, MinZoom, MaxZoom;
    public float rotSpeed;
    
    public string Item_Title;
    [TextArea(5,5)] public string Item_Discription;

    public void Start()
    {
        DefaultZoom = 50f;
        MinZoom = 40f;
        MaxZoom = 70f;
        rotSpeed = 200;
        ExamineSystem = FindObjectOfType<ExamineSystem>();
        ExamineCamera = GameObject.Find("ExamineCamera").GetComponent<Camera>();
        Icon = FindObjectOfType<CursorIcon>();
        Item_title_Text = FindObjectOfType<Item_Title_Container>(true).GetComponentInChildren<TMP_Text>();
        Item_Description_Text = FindObjectOfType<Item_Description_Container>(true).GetComponentInChildren<TMP_Text>();
    }

    public void Update()
    {
        if (ExamineSystem.ExamineMode && ExamineSystem.Examine_Object.name == this.transform.name)
        {
            if (Input.GetMouseButton(0))
            {
                float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
                float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

                transform.Rotate(Vector3.up, -rotX, Space.World);
                transform.Rotate(Vector3.right, rotY, Space.World);
            }

            ExamineItemInfo();
            ExamineZoom();
        }

        if (!ExamineSystem.ExamineMode) 
        {
            ExamineCamera.fieldOfView = DefaultZoom;
        }

        if (ExamineSystem.ExamineMode && !loadPoints && ExamineSystem.Examine_Object.name == this.transform.name)
        {
            for (int i = 0; i < PointOfInterestList.Count; i++)
            {
                PointOfInterestList[i].SetActive(true);
                PointOfInterestList[i].layer = 7;
            }
            loadPoints = true;
        }
        else if (!ExamineSystem.ExamineMode && loadPoints && ExamineSystem.Examine_Object.name == this.transform.name)
        {
            for (int i = 0; i < PointOfInterestList.Count; i++)
            {
                PointOfInterestList[i].SetActive(false);
                PointOfInterestList[i].layer = 0;
            }
            loadPoints = false;
        }
    }


    public void ExamineZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(ExamineCamera.fieldOfView >= MinZoom)
                ExamineCamera.fieldOfView--;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (ExamineCamera.fieldOfView <= MaxZoom)
                ExamineCamera.fieldOfView++;
        }
    }

    public void ExamineItemInfo() 
    {
        Item_title_Text.text = Item_Title;
        Item_Description_Text.text = Item_Discription;
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
