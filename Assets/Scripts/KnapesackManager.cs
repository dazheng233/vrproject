using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using VRTK;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class KnapesackManager : MonoBehaviour
{
    private Dictionary<string,Item> ItemList=new Dictionary<string, Item>();
    private static KnapesackManager _instance;
    public GridPanelUI gridPanelUi;
    public ToolTipUI toolTipUi;
    
    public VRTK_ControllerEvents vrtkControllerEvents;
    public VRTK_ObjectAutoGrab vrtkObjectAutoGrab;

    private Item selectedItem;
    private GameObject currentGrid;

    public GameObject controller;
    private BagManager bagManager;

    public static KnapesackManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        Load();

        GridUI.OnStay += GridUI_OnStay;
        GridUI.OnExit += GridUI_OnExit;
        GridUI.OnEnter += GridUI_OnEnter;
        
    }

    private void Start()
    {
        vrtkObjectAutoGrab = vrtkControllerEvents.gameObject.GetComponent<VRTK_ObjectAutoGrab>();
        bagManager = controller.GetComponent<BagManager>();
    }

    public void Load()
    {
       Item spanner=new Item(1,"spanner","这是一个扳手，你可以用它修理东西");
       Item rope=new Item(2,"rope","或许可以用它把什么东西吊上来");
       Item device=new Item(3,"device","这应该会在接地的时候起到作用");
       Item pulley=new Item(4,"pulley","听说定滑轮可以改变力的方向");
       Item hummer=new Item(5,"hummer","一个质量上乘的锤子");
       Item hook=new Item(6,"hook","这个钩子是用来干什么的呢？");
       Item wireGrap=new Item(7,"wireGrap","固定接地线的时候应该会用到");
       Item electroscope=new Item(8,"electroscope","请工人不要电死在高压电塔上，否则我会很困扰的");
       
       
       ItemList.Add("spanner",spanner);
       ItemList.Add("rope",rope);
       ItemList.Add("device",device);
       ItemList.Add("pulley",pulley);
       ItemList.Add("hummer",hummer);
       ItemList.Add("hook",hook);
       ItemList.Add("wireGrap",wireGrap);
       ItemList.Add("electroscope",electroscope);
    }

    #region callback

    private void GridUI_OnEnter()
    {
        vrtkControllerEvents.GripPressed += GrabSelectedItem;
    }

    private void GridUI_OnStay(Transform gridTransform,Vector3 pointerPosToCanvas,GameObject grid)
    {
        Item item = ItemModel.GetItem(gridTransform.name);
        selectedItem = item;
        currentGrid = grid;
        
        if (item == null)
        {
            return;
        }

        string text = GetToolTip(item);
        toolTipUi.UpdateTooltip(text,pointerPosToCanvas);
        toolTipUi.Show();
    }

    private void GridUI_OnExit()
    {
        toolTipUi.Hide();
        vrtkControllerEvents.GripPressed -= GrabSelectedItem;
        vrtkObjectAutoGrab.enabled = false;
    }

    #endregion

    private string GetToolTip(Item item)
    {
        if (item == null)
        {
            return "";
        }
        
        StringBuilder stringBuilder=new StringBuilder();
        stringBuilder.AppendFormat("<color=red>{0}</color>\n\n", item.Name);
        stringBuilder.Append(item.Description);

        return stringBuilder.ToString();
    }

    public void StoreItem(string ItemName)
    {
        if (!ItemList.ContainsKey(ItemName))
        {
            return;
        }

        Transform emptyGrid = gridPanelUi.GetEmptyGrid();
        if (emptyGrid == null)
        {
            Debug.Log("背包溢出");
            return;
        }
        Item temp = ItemList[ItemName];
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/UI/Item");
        itemPrefab.GetComponent<ItemUI>().UpdateImage(temp.Icon);
        GameObject itemGo = GameObject.Instantiate(itemPrefab);
        
        

        itemGo.transform.SetParent(emptyGrid);
        itemGo.transform.localPosition=Vector3.zero;
        itemGo.transform.localScale=Vector3.one;
        itemGo.transform.localEulerAngles=Vector3.zero;

        BoxCollider boxCollider = itemGo.GetComponent<BoxCollider>();
        boxCollider.size=new Vector3(81.5f,81.5f,1.0f);
        
        ItemModel.StoreItem(emptyGrid.name,temp);
    }
    
    private void GrabSelectedItem(object sender,ControllerInteractionEventArgs e)
    {
        GameObject root = GameObject.Find("Models");
        GameObject selectedItemObj = root.transform.Find(selectedItem.Name).gameObject;     
        
        selectedItemObj.SetActive(true);
        vrtkObjectAutoGrab.objectToGrab = selectedItemObj.GetComponent<VRTK_InteractableObject>();
        vrtkObjectAutoGrab.enabled = true;

        DeleteSelectedItemFromBackpack();
        currentGrid.transform.SetAsLastSibling();
    }

    //删除当前grid下所有的子物体并从GridItem字典中移除该项
    private void DeleteSelectedItemFromBackpack()
    {
        int childrenCount = currentGrid.transform.childCount;

        for (int i = 0; i < childrenCount; i++)
        {
            Destroy(currentGrid.transform.GetChild(0).gameObject);
        }
        ItemModel.DeleteItem(currentGrid.name);
        gridPanelUi.MoveGridToLast(currentGrid);
    }
}
