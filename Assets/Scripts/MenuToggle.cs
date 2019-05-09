using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class MenuToggle : MonoBehaviour
{
    private bool menuState = false;
    public VRTK_ControllerEvents controllerEvents;
    public GameObject menu;
    
    
    void OnEnable()
    {
        controllerEvents.ButtonOneReleased += EnableMenu;
    }

    private void OnDisable()
    {
        controllerEvents.ButtonOneReleased -= EnableMenu;
    }

    void EnableMenu(object sender,ControllerInteractionEventArgs args)
    {
        menuState = !menuState;
        menu.SetActive(menuState);
    }
}
