using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ExamineSystem : MonoBehaviour
{
    [SerializeField] private GameObject Examine_Point;
    [SerializeField] private GameObject VFX_Effect;
    [SerializeField] private GameObject info_UI;

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private CursorIcon _cursorIcon;

    [SerializeField] private Camera ExamineCamera;
    [SerializeField] private Transform MainCamera;
    [SerializeField] private GameObject Inventory_UI;

    public GameObject Examine_Object;
    public GameObject OriginalParentObject;

    public Vector3 parentSize;
    private Vector3 OriginalScale;
    private Vector3 OriginalPosition;
    private Quaternion OriginalRotation;

    public int OriginalLayer;
    public static bool ExamineMode;

    public string content;
    public string header;

    public void Start()
    {
        Examine_Point = GameObject.Find("Examine_Point");
        VFX_Effect = GameObject.Find("DoF-Effect");
        Inventory_UI = GameObject.Find("Panel");
        info_UI = GameObject.Find("Info_UI");
        _cursorIcon = FindObjectOfType<CursorIcon>();

        playerInventory = FindObjectOfType<PlayerInventory>();
        ExamineCamera = GameObject.Find("ExamineCamera").GetComponent<Camera>();
        MainCamera = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>().transform;

        parentSize = Examine_Point.transform.GetComponent<BoxCollider>().bounds.size / 2;
        info_UI.GetComponentInChildren<Button>(true).onClick.AddListener(ExitExamineMode);
        info_UI.SetActive(false);
        VFX_Effect.SetActive(false);
        Inventory_UI.SetActive(false);
        ExamineMode = false;
    }

    public void ExamineAction(GameObject item) 
    {
        ExamineMode = true;
        info_UI.SetActive(true);
        VFX_Effect.SetActive(true);
        Examine_Object = item;
        ItemInspect(Examine_Object);
    }

    public void ExamineActionPickUp()
    {
        Examine_Object.GetComponent<InspectSystem>().has_pickup = true;
        playerInventory.AddToInventory(Examine_Object);
        ExitExamineMode();
    }

    #region Item Inspection
    public void ItemInspect(GameObject Inspect_object)
    {
        if (TooltipTrigger.EximaneFromInventory) 
        {
            TooltipSystem.Hide();
            ExamineMode = true;
            info_UI.SetActive(true);
            VFX_Effect.SetActive(true);
            Examine_Object = Inspect_object;
            Examine_Object.SetActive(true);

            Inventory_UI.SetActive(false);
            PlayerInventory.InventoryIsOn = false;
        }

        if (Examine_Object.transform.parent != null)
            OriginalParentObject = Examine_Object.transform.parent.gameObject;
        else
            OriginalParentObject = null;

        this.gameObject.GetComponent<FirstPersonController>().enabled = false;
        SetNewTransform(Inspect_object);
        ResizeObject(Inspect_object);
    }
    #endregion

    #region Reset Tranform
    public void ResetTransform()
    {
        ExamineCamera.fieldOfView = 50f;
        Examine_Object.transform.SetParent(Examine_Point.transform);
        Examine_Object.transform.localPosition = new Vector3(0, 0, 0);
        Examine_Object.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Examine_Object.layer = 3;
    }
    #endregion

    #region Set old and new Transform
    public void SetNewTransform(GameObject Inspect_object) 
    {
        OriginalPosition = Inspect_object.transform.position;
        OriginalRotation = Inspect_object.transform.rotation;
        OriginalScale = Inspect_object.transform.localScale;
        OriginalLayer = Inspect_object.layer;
        Inspect_object.transform.SetParent(Examine_Point.transform);
        Inspect_object.transform.localPosition = new Vector3(0, 0, 0);
        Inspect_object.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Inspect_object.layer = 3;
        foreach (Transform child in Inspect_object.transform)
        {
            child.gameObject.layer = 3;
        }
    }
    #endregion

    #region Exit Examine Mode
    public void ExitExamineMode() 
    {
        _cursorIcon.ChangeMouseIcon(CursorLockMode.Locked, false, Color.white, 5);

        this.gameObject.GetComponent<FirstPersonController>().enabled = true;
        ExamineMode = false;
        VFX_Effect.SetActive(false);
        info_UI.SetActive(false);


        if (OriginalParentObject != null)
            Examine_Object.transform.parent = OriginalParentObject.transform;
        else
            Examine_Object.transform.parent = null;

        Examine_Object.transform.position = OriginalPosition;
        Examine_Object.transform.rotation = OriginalRotation;
        Examine_Object.transform.localScale = OriginalScale;
        Examine_Object.layer = OriginalLayer;

        foreach (Transform child in Examine_Object.transform)
        {
            child.gameObject.layer = OriginalLayer;
        }

        if (Examine_Object.GetComponent<InspectSystem>().has_pickup)
            Examine_Object.SetActive(false);

        if (TooltipTrigger.EximaneFromInventory)
        {
            _cursorIcon.ChangeMouseIcon(CursorLockMode.None, true, Color.black, 5);

            this.gameObject.GetComponent<FirstPersonController>().enabled = false;
            Inventory_UI.SetActive(true);
            PlayerInventory.InventoryIsOn = true;
            TooltipTrigger.EximaneFromInventory = false;
        }
    }
    #endregion

    #region Resize
    public void ResizeObject(GameObject Resize_object)
    {
        MainCamera.transform.localPosition = new Vector3(0, 0.8f, 0);
        MainCamera.localRotation = new Quaternion(0, 0, 0, 0);


        if (Resize_object.GetComponent<MeshFilter>() != null)
        { 
            while (Resize_object.transform.GetComponent<MeshFilter>().GetComponent<Renderer>().bounds.extents.x > parentSize.x ||
                  Resize_object.transform.GetComponent<MeshFilter>().GetComponent<Renderer>().bounds.extents.y > parentSize.y ||
                  Resize_object.transform.GetComponent<MeshFilter>().GetComponent<Renderer>().bounds.extents.z > parentSize.z)
            {
                    Resize_object.transform.localScale *= 0.9f;
            }
        }
    }
    #endregion
}

