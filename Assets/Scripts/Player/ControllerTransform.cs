using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerTransform : MonoBehaviour
{
    [SerializeField] private Transform leftBall;
    [SerializeField] private Transform rightBall;
    [SerializeField] private Transform instrumentParent;
    [SerializeField] private Transform menuParent;
    [SerializeField] private Transform sampleParent;
    [SerializeField] private Transform rampParent;

    // Update is called once per frame
    void Update()
    {
        switch (Controller.Instance.GrabStates)
        {
            case Controller.GrabState.Grab:
            case Controller.GrabState.RightGrab:
            case Controller.GrabState.LeftGrab:
                UpdateGrab();
                break;
            case Controller.GrabState.Scale:
                UpdateScale();
                break;
        }
    }

    void UpdateGrab()
    {
        if (this.gameObject.name == Controller.WhichIsHand.rightHand)
        {
            TransformChange(SteamVR_Input_Sources.RightHand, Controller.Instance.IsRightGrab, Controller.Instance.GrabRightObj);
        }
        
        if (this.gameObject.name == Controller.WhichIsHand.leftHand)
        {
            TransformChange(SteamVR_Input_Sources.LeftHand, Controller.Instance.IsLeftGrab, Controller.Instance.GrabLeftObj);
        }
    }

    void TransformChange(SteamVR_Input_Sources hand, bool isGrab, GameObject grabObj)
    {
        if (Controller.Instance.Grab.GetState(hand) && isGrab)
        {
            if (grabObj == null)
            {
                Debug.LogError("Grab Object is null");
                return;
            }

            if (gameObject.transform.childCount > 0)
            {
                return;
            }

            grabObj.transform.parent = gameObject.transform;
        }
        else if (Controller.Instance.Grab.GetStateUp(hand) && isGrab)
        {
            if (gameObject.transform.childCount != 0)
            {
                if (GetComponentInChildren<Grab>().transform.parent.gameObject.CompareTag("Sample"))
                {
                    grabObj.transform.parent = sampleParent;
                }
                else if (GetComponentInChildren<Grab>().transform.parent.gameObject.CompareTag("Instrument"))
                {
                    grabObj.transform.parent = instrumentParent;
                }
                else if (GetComponentInChildren<Grab>().transform.parent.gameObject.CompareTag("Menu"))
                {
                    grabObj.transform.parent = menuParent;
                }
                else if (GetComponentInChildren<Grab>().transform.parent.gameObject.CompareTag("RampHead"))
                {
                    grabObj.transform.parent = rampParent;
                }
                else
                {
                    grabObj.transform.parent = null;
                }
            }

            if (this.gameObject.name == Controller.WhichIsHand.rightHand)
            {
                Controller.Instance.GrabStates = Controller.GrabState.Grab;
                Controller.Instance.IsRightGrab = false;
                Controller.Instance.GrabRightObj = null;
            }
            else if (this.gameObject.name == Controller.WhichIsHand.leftHand)
            {
                Controller.Instance.GrabStates = Controller.GrabState.Grab;
                Controller.Instance.IsLeftGrab = false;
                Controller.Instance.GrabLeftObj = null;
            }
        }
    }
    
    void UpdateScale()
    {
        ScaleChange(SteamVR_Input_Sources.RightHand, SteamVR_Input_Sources.LeftHand, Controller.Instance.IsRightScale, Controller.Instance.IsLeftScale, Controller.Instance.ScaleObj);
    }

    void ScaleChange(SteamVR_Input_Sources hand1, SteamVR_Input_Sources hand2, bool isRightScale, bool isLeftScale, GameObject scaleObj)
    {
        if (Controller.Instance.Grab.GetState(hand1) && Controller.Instance.Grab.GetState(hand2) && isRightScale && isLeftScale)
        {
            if (scaleObj == null)
            {
                Debug.LogError("Scale Obj is null");
                return;
            }
            
            if (gameObject.transform.childCount > 0)
            {
                return;
            }

            if (gameObject.transform.childCount != 0)
            {
                scaleObj.transform.parent = instrumentParent;
            }

            float ballDist = Vector3.Distance(rightBall.transform.position, leftBall.transform.position);
            float clampBallDist = Mathf.Clamp(ballDist, 0.5f, 1.0f);

            if (scaleObj.transform.localScale.x >= 0.5f && scaleObj.transform.localScale.x <= 1.0f)
            {
                scaleObj.transform.localScale = new Vector3(clampBallDist, clampBallDist, clampBallDist);
            }
            else if (scaleObj.transform.localScale.x < 0.5f)
            {
                scaleObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (scaleObj.transform.localScale.x > 1.0f)
            {
                scaleObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        else if (Controller.Instance.Grab.GetStateUp(hand1) || Controller.Instance.Grab.GetStateUp(hand2))
        {
            Controller.Instance.GrabStates = Controller.GrabState.Grab;
            Controller.Instance.IsRightScale = false;
            Controller.Instance.IsLeftScale = false;
            Controller.Instance.ScaleObj = null;
        }
    }
}
