using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGrabLine : MonoBehaviour
{
    private LineRenderer line;
    
    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLink();
    }

    void UpdateLink()
    {
        line.SetPosition(0, this.gameObject.transform.position);
        line.SetPosition(1, GetComponentInParent<SampleAudioVisualizer>().sampleNodes[0].gameObject.transform.position);
    }
}
