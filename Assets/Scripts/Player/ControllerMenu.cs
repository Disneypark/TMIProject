using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerMenu : MonoBehaviour
{
    private bool isMainMenu = false;

    // Update is called once per frame
    void Update()
    {
        if (Controller.Instance.Menu.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            isMainMenu = !isMainMenu;
            
            if (!isMainMenu)
            {
                return;
            }
            
            ButtonManager.Instance.OnButtonMovePinch();
            ButtonManager.Instance.OnButtonFind("MainMenu");
        }

    }
}
