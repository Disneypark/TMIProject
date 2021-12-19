using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class SceneChanger : MonoBehaviour
{
    void Awake()
    {
        
    }
    
    void Start()
    {
        StartCoroutine(IEfadeStart());
    }
    
    private IEnumerator IEfadeStart()
    {
        yield return new WaitForSeconds(10.0f);
        
        SteamVR_LoadLevel.Begin("Test Scene", false, 1.5f, 0.0f, 0.0f, 0.0f, 1.0f);
    }
}
