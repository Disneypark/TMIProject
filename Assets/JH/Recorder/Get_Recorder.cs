using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Get_Recorder : MonoBehaviour
{
    public SteamVR_Action_Boolean xButton;
    public SteamVR_Action_Boolean yButton;

    public GameObject metro;
    public GameObject recorderGroup;
    void Update()
    {
        if (xButton.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            metro.transform.localPosition = transform.localPosition + transform.forward * 0.5f;
            metro.transform.localEulerAngles = transform.localEulerAngles;
        }

        if (yButton.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            recorderGroup.transform.localPosition = transform.localPosition + transform.forward * 0.5f;
            recorderGroup.transform.localEulerAngles = transform.localEulerAngles;
        }
    }
}
