using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SampleMenu : MonoBehaviour
{
    [SerializeField] private string resourcePath;
    public string ResourcePath
    {
        get => resourcePath;
        set => resourcePath = value;
    }

    [SerializeField] private string sampleClipResourcePath;

    public string SampleClipResourcePath
    {
        get => sampleClipResourcePath;
        set => sampleClipResourcePath = value;
    }

    [SerializeField] private string findName;
    public string FindName
    {
        get => findName;
        set => findName = value;
    }
    
    private GameObject sampleMarker;
    
    // Start is called before the first frame update
    void Start()
    {
        sampleMarker = GameObject.Find("SampleMarker").transform.Find(findName).gameObject;

        if (!sampleMarker)
        {
            Debug.LogError("Sample Load Error");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (Controller.Instance.Menu2.GetState(SteamVR_Input_Sources.RightHand))
            {
                other.GetComponent<ControllerSample>().SampleInput(sampleMarker, resourcePath, sampleClipResourcePath, true, true);
            }
        }
    }
}
