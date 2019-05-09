using System;
using UnityEngine;
using VRTK;

public class GridUI : MonoBehaviour
{
    #region Enter&&Exit

    public static Action<Transform,Vector3,GameObject> OnStay;
    public static Action OnExit;
    public static Action OnEnter;

//    private static VRTK_ControllerEvents vrtkControllerEvents;
    
    private Transform canvasTransform;
    private GameObject RightCursor;

    private void Start()
    {
//        vrtkControllerEvents = GameObject.Find("ControllerRight").GetComponent<VRTK_ControllerEvents>();
        canvasTransform=GameObject.Find("Canvas").transform;
    }

    private void Update()
    {
        if (RightCursor != null)
        {
            if (!RightCursor.activeSelf)
            {
                OnTriggerExit(null);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int compareRes = String.Compare(other.name, "[VRTK][AUTOGEN][ControllerRight][StraightPointerRenderer_Cursor]",true);
        if (compareRes==0)
        {
            OnEnter();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        int compareRes = String.Compare(other.name, "[VRTK][AUTOGEN][ControllerRight][StraightPointerRenderer_Cursor]",true);
        if (compareRes==0)
        {
            RightCursor = other.gameObject;
            if (OnStay != null)
            {
                Vector3 pointerPosToCanvas = canvasTransform.InverseTransformPoint(other.transform.position);
                OnStay(transform,pointerPosToCanvas,gameObject);
            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (OnExit != null)
        {
            OnExit();
        }
    }

    #endregion

    
}
