using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerInstrument : MonoBehaviour
{
    [SerializeField] private GameObject rightBall;
    [SerializeField] private Transform instrumentParent;
    [SerializeField] private Transform playerCam;
    [SerializeField] private GameObject[] instrumentMarkerCheckList;
    
    private GameObject instrumentMarker;
    public bool _isInstrumentDisplay = false;
    private string _resourcePath = String.Empty;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.Instance.Menu2.GetStateUp(SteamVR_Input_Sources.RightHand) && !Controller.Instance.IsPadTouch)
        {
            if (_resourcePath == String.Empty)
            {
                return;
            }
            
            InstrumentGenerate(InstrumentLoad(_resourcePath), instrumentParent, false);
            
            InstrumentGenerateFail(false);
        }

        if (instrumentMarker != null)
        {
            for (int i = 0; i < instrumentMarkerCheckList.Length; i++)
            {
                if (instrumentMarkerCheckList[i] == instrumentMarker)
                {
                    instrumentMarkerCheckList[i].SetActive(true);
                }
                else
                {
                    instrumentMarkerCheckList[i].SetActive(false);
                }
            }
            
        }
    }

    void InstrumentGenerateFail(bool isSelect)
    {
        _resourcePath = String.Empty;
        
        if (instrumentMarker == null)
        {
            return;
        }
        rightBall.GetComponent<PlayerBall>().ColorChange(isSelect);
        instrumentMarker.SetActive(false);
        instrumentMarker = null;
    }
    
    public void InstrumentInput(GameObject go, string resourcePath, bool isInstrumentDisplay, bool isSelect)
    {
        _isInstrumentDisplay = isInstrumentDisplay;
        _resourcePath = resourcePath;
        instrumentMarker = go;
        rightBall.GetComponent<PlayerBall>().ColorChange(isSelect);
    }
    
    private GameObject InstrumentLoad(string resourcePath)
    {
        GameObject go = null;
        
        go = Resources.Load<GameObject>(resourcePath);

        if (!go)
        {
            Debug.Log("Resources Load Error Path = " + resourcePath);
            return null;
        }

        GameObject instancedGo = go;
        return instancedGo;
    }

    private bool InstrumentGenerate(GameObject instancedGo, Transform parent, bool isSelect)
    {
        GameObject instrument = Instantiate<GameObject>(instancedGo, parent, true);
        string replace = instrument.name.Replace("(Clone)", "");
        instrument.name = replace;
        instrument.transform.position = rightBall.transform.position + rightBall.transform.forward * 0.1f;
        instrument.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
        instrument.transform.forward = playerCam.forward;
        instrument.transform.localEulerAngles = new Vector3(0.0f, instrument.transform.localEulerAngles.y, instrument.transform.localEulerAngles.z);
        instrumentMarker.SetActive(false);
        instrumentMarker = null;
        rightBall.GetComponent<PlayerBall>().ColorChange(isSelect);

        return true;
    }
}
