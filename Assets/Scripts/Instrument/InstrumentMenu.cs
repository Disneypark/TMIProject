using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class InstrumentMenu : MonoBehaviour
{
    [SerializeField] private string resourcePath;
    public string ResourcePath
    {
        get => resourcePath;
        set => resourcePath = value;
    }

    [SerializeField] private string findName;
    public string FindName
    {
        get => findName;
        set => findName = value;
    }

    private GameObject instrumentMarker;

    void Start()
    {
        instrumentMarker = GameObject.Find("InstrumentMarker").transform.Find(findName).gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (Controller.Instance.Menu2.GetState(SteamVR_Input_Sources.RightHand)) other.GetComponent<ControllerInstrument>().InstrumentInput(instrumentMarker, resourcePath, true, true);
        }
    }
}
