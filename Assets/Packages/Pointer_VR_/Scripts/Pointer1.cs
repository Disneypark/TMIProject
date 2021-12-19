using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class Pointer1 : MonoBehaviour
{
    [SerializeField] private float defaultLength = 15.0f;
    [SerializeField] private Transform startRayPos;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private SteamVR_Action_Boolean trigger;

    public Camera Camera { get; private set; } = null;

    public VRInputModule inputModule;

    private Vector3 endPosition;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
        Camera.enabled = false;

        lineRenderer.enabled = false;
    }

    private void Start()
    {
        // current.currentInputModule does not work
        //inputModule = EventSystem.current.gameObject.GetComponent<VRInputModule>();
    }

    private void Update()
    {
        if (trigger.GetState(SteamVR_Input_Sources.RightHand)) UpdateLine();
        if (trigger.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            lineRenderer.enabled = false;
        }
    }

    private void UpdateLine()
    {
        // Use default or distance
        PointerEventData data = inputModule.Data;
        RaycastHit hit = CreateRaycast();

        // If nothing is hit, set do default length
        float colliderDistance = hit.distance == 0 ? defaultLength : hit.distance;
        float canvasDistance = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;

        // Get the closest one
        float targetLength = Mathf.Min(colliderDistance, canvasDistance);

        // Default
        endPosition = transform.position + (transform.forward * targetLength);

        // Set linerenderer
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, startRayPos.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(startRayPos.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);
        Debug.DrawRay(startRayPos.position, transform.forward);

        return hit;
    }
}
