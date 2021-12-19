using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPointer : MonoBehaviour
{
    [SerializeField] private float defaultLength = 5.0f;
    [SerializeField] private GameObject dot = null;

    private LineRenderer lineRenderer = null;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        RaycastHit hit = CreateRaycast();

        // If nothing is hit, set do default length
        //float colliderDistance = hit.distance == 0 ? defaultLength : hit.distance;
        //float canvasDistance = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;
        float colliderDistance = hit.distance == 0 ? defaultLength : hit.distance;

        // Get the closest one
        float targetLength = Mathf.Min(defaultLength, colliderDistance);


        // Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        // Set position of the dot
        dot.transform.position = endPosition;

        // Set linerenderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength);

        return hit;
    }
}
