using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentRay : MonoBehaviour
{
    private int floor;
    private const string instrumentRayMarkerPath = "Prefabs/InstrumentRayMarker";
    private GameObject instrumentRayMarker;

    // Start is called before the first frame update
    void Start()
    {
        floor = 1 << LayerMask.NameToLayer("Floor");

        StartRay(transform.position, Vector3.down);
    }

    void StartRay(Vector3 pos, Vector3 dir)
    {
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, float.MaxValue, floor))
        {
            GameObject parent = transform.parent.transform.Find("InstrumentRayMarker").gameObject;
            instrumentRayMarker = Instantiate<GameObject>(Load(instrumentRayMarkerPath), parent.transform, true);
            instrumentRayMarker.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x, transform.localScale.z);
            instrumentRayMarker.transform.position = hit.point + hit.normal * 0.1f;
            instrumentRayMarker.transform.forward = hit.normal;
            instrumentRayMarker.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRay(transform.position, Vector3.down);
    }

    void UpdateRay(Vector3 pos, Vector3 dir)
    {
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, floor))
        {
            instrumentRayMarker.transform.position = hit.point + hit.normal * 0.1f;
            instrumentRayMarker.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.x, transform.localScale.z);
        }
    }

    private GameObject Load(string resourcePath)
    {
        GameObject go = null;

        go = Resources.Load<GameObject>(resourcePath);

        if (!go)
        {
            Debug.LogError("Instrument Ray Marker Load Error FilePath = " + resourcePath);
            return null;
        }

        GameObject InstancedGO = go;
        return InstancedGO;
    }
}
