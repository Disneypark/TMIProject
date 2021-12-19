using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform startRayPos;
    [SerializeField] GameObject pointer;
    [SerializeField] GameObject rotatePlayer;

    //private SteamVR_Behaviour_Pose m_Pose = null;
    private bool hasPosition = false;
    private bool isTeleporting = false;
    private float fadeTime = 0.5f;
    private float angle = 45;

    private int floor;

    private void Awake()
    {

    }

    private void Start()
    {
        floor = 1 << LayerMask.NameToLayer("Floor");
    }

    private void Update()
    {
        // Pointer
        hasPosition = UpdatePointer();
        pointer.SetActive(hasPosition);

        // Teleport
        if (Controller.Instance.Trigger.GetStateDown(SteamVR_Input_Sources.RightHand)) TryTeleport();
        if (Controller.Instance.TurnRight.GetStateDown(SteamVR_Input_Sources.RightHand)) StartCoroutine(RotateRig(angle));
        if (Controller.Instance.TurnLeft.GetStateDown(SteamVR_Input_Sources.RightHand)) StartCoroutine(RotateRig(-angle));
    }

    private void TryTeleport()
    {
        // Check for valid position, and if already teleporting
        if (!hasPosition || isTeleporting)
            return;

        // Get camera rig, and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // Figure out translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y - 3.7f, headPosition.z);
        Vector3 translateVector = pointer.transform.position - groundPosition;

        // Move
        StartCoroutine(MoveRig(cameraRig, translateVector));

    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        // Flag
        isTeleporting = true;

        // Fade to black
        SteamVR_Fade.Start(Color.black, fadeTime, true);

        // Apply translation
        yield return new WaitForSeconds(fadeTime);
        cameraRig.position += translation;

        // Fade to clear
        SteamVR_Fade.Start(Color.clear, fadeTime, true);

        // De-flag
        isTeleporting = false;
    }

    private bool UpdatePointer()
    {
        // Ray from the controller
        Ray ray = new Ray(startRayPos.transform.position, transform.forward);
        RaycastHit hit;

        // If it's a hit
        if (Physics.Raycast(ray, out hit, float.MaxValue, floor))
        {
            pointer.transform.position = hit.point;
            pointer.transform.up = hit.normal;
            return true;
        }

        // If not a hit
        return false;
    }

    private IEnumerator RotateRig(float angle)
    {
        // Flag
        isTeleporting = true;

        // Fade to black
        SteamVR_Fade.Start(Color.black, fadeTime, true);

        // Apply translation
        yield return new WaitForSeconds(fadeTime);
        rotatePlayer.transform.localEulerAngles += new Vector3(0, angle, 0);

        // Fade to clear
        SteamVR_Fade.Start(Color.clear, fadeTime, true);

        // De-flag
        isTeleporting = false;
    }

}