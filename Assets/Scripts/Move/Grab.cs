using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (Controller.Instance.Grab.GetState(SteamVR_Input_Sources.RightHand) == false)
            {
                // 오른쪽
                if (other.gameObject.name == Controller.WhichIsHand.rightHand)
                {
                    Controller.Instance.GrabStates = Controller.GrabState.RightGrab;
                    Controller.Instance.GrabRightObj = this.transform.parent.gameObject;
                    Controller.Instance.IsRightGrab = true;

                    if (this.gameObject.CompareTag("Pad"))
                    {
                        Controller.Instance.IsRightScale = true;
                    }
                }
            }

            if (Controller.Instance.Grab.GetState(SteamVR_Input_Sources.LeftHand) == false)
            {
                // 왼쪽
                if (other.gameObject.name == Controller.WhichIsHand.leftHand)
                {
                    Controller.Instance.GrabStates = Controller.GrabState.LeftGrab;
                    Controller.Instance.GrabLeftObj = this.transform.parent.gameObject;
                    Controller.Instance.IsLeftGrab = true;
                    
                    if (this.gameObject.CompareTag("Pad"))
                    {
                        Controller.Instance.IsLeftScale = true;
                    }
                }
            }

            if (Controller.Instance.Grab.GetState(SteamVR_Input_Sources.RightHand) == false &&
                Controller.Instance.Grab.GetState(SteamVR_Input_Sources.LeftHand) == false)
            {
                if (Controller.Instance.IsRightScale == true && Controller.Instance.IsLeftScale == true)
                {
                    Controller.Instance.GrabStates = Controller.GrabState.Scale;
                    Controller.Instance.ScaleObj = this.transform.parent.gameObject;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (Controller.Instance.Grab.GetState(SteamVR_Input_Sources.RightHand) == false)
            {
                if (other.gameObject.name == Controller.WhichIsHand.rightHand)
                {
                    Controller.Instance.GrabStates = Controller.GrabState.Grab;
                    Controller.Instance.GrabRightObj = null;
                    Controller.Instance.IsRightGrab = false;
                    
                    if (this.gameObject.CompareTag("Pad"))
                    {
                        Controller.Instance.IsRightScale = false;
                    }
                }
            }

            if (Controller.Instance.Grab.GetState(SteamVR_Input_Sources.LeftHand) == false)
            {
                if (other.gameObject.name == Controller.WhichIsHand.leftHand)
                {
                    Controller.Instance.GrabStates = Controller.GrabState.Grab;
                    Controller.Instance.GrabLeftObj = null;
                    Controller.Instance.IsLeftGrab = false;
                    
                    if (this.gameObject.CompareTag("Pad"))
                    {
                        Controller.Instance.IsLeftScale = false;
                    }
                }
            }
        }
    }
}
