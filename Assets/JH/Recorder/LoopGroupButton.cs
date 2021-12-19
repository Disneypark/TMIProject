using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoopGroupButton : MonoBehaviour
{
    public RecordPlayer rp;
    public bool isLoop;

    private void Awake()
    {
        rp = GetComponentInParent<RecordPlayer>();
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            rp.OnClickPlay(isLoop);
        }
    }
}
