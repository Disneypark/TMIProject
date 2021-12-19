using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour
{
    public GameObject m_Pointer;
    public GameObject m_RotatePlayer;
    public SteamVR_Action_Boolean m_TeleportAction;
    public SteamVR_Action_Boolean m_TurnLeft;
    public SteamVR_Action_Boolean m_TurnRight;

    //private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;
    private bool m_IsTeleporting = false;
    private float m_FadeTime = 0.5f;
    private float m_Angle = 45;

    private void Awake()
    {
        //m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        // Pointer
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);

        // Teleport
        if (m_TeleportAction.GetStateDown(SteamVR_Input_Sources.RightHand))
            TryTeleport();
        if (m_TurnRight.GetStateDown(SteamVR_Input_Sources.LeftHand))
            StartCoroutine(RotateRig(m_Angle));
        if (m_TurnLeft.GetStateDown(SteamVR_Input_Sources.LeftHand))
            StartCoroutine(RotateRig(-m_Angle));

    }

    private void TryTeleport()
    {
        // Check for valid position, and if already teleporting
        if (!m_HasPosition || m_IsTeleporting)
            return;


        // Get camera rig, and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // Figure out translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y - 10.0f, headPosition.z);
        Vector3 translateVector = m_Pointer.transform.position - groundPosition;

        // Move
        StartCoroutine(MoveRig(cameraRig, translateVector));

    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        // Flag
        m_IsTeleporting = true;

        // Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        // Apply translation
        yield return new WaitForSeconds(m_FadeTime);
        cameraRig.position += translation;

        // Fade to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

        // De-flag
        m_IsTeleporting = false;
    }

    private bool UpdatePointer()
    {
        // Ray from the controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // If it's a hit
        if (Physics.Raycast(ray, out hit))
        {
            m_Pointer.transform.position = hit.point;
            return true;
        }

        // If not a hit
        return false;
    }

    private IEnumerator RotateRig(float m_Angle)
    {
        // Flag
        m_IsTeleporting = true;

        // Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        // Apply translation
        yield return new WaitForSeconds(m_FadeTime);
        m_RotatePlayer.transform.localEulerAngles += new Vector3(0, m_Angle, 0);

        // Fade to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

        // De-flag
        m_IsTeleporting = false;
    }

}
