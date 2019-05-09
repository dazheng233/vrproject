using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BagManager : MonoBehaviour
{
    public VRTK_ControllerEvents VrtkControllerEvents;
//    public VRTK_InteractGrab vrtkInteractGrab;
    private List<GameObject> tools=new List<GameObject>();
    private List<string> toolNames=new List<string>();
    private VRTK_InteractTouch vrtkInteractTouch;
    public GameObject controllerRight;
    
    void Start()
    {
        controllerRight = VrtkControllerEvents.gameObject;
        vrtkInteractTouch = controllerRight.GetComponent<VRTK_InteractTouch>();
        VrtkControllerEvents.ButtonTwoPressed+=AddTouchingGameobjectToBackPack;
    }

    void AddTouchingGameobjectToBackPack(object sender,ControllerInteractionEventArgs e)
    {
        
        if (vrtkInteractTouch.GetTouchedObject())
        {
            GameObject obj=vrtkInteractTouch.GetTouchedObject();
            tools.Add(obj);
            toolNames.Add(obj.name);

            obj.SetActive(false);
            KnapesackManager.Instance.StoreItem(obj.name);
            
        }
        else
        {
            Debug.Log("object is not exist");
        }
    }
}
