using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleNode : MonoBehaviour
{
    private LineRenderer line;

    private Vector3 value = Vector3.one;
    private Vector3 _target;
    private float colorValue = 40.0f;
    private float _targetGlow;
    private bool isBeat = true;
    private bool isColorBeat = true;
    private float speed = 20.0f;
    private float colorSpeed = 500.0f;

    public enum SampleState
    {
        Ready,
        Anim,
        Return
    }

    public enum SampleColorState
    {
        Ready,
        Anim,
        Return
    }

    public enum SampleLineColorState
    {
        Ready,
        Anim,
        Return
    }

    public SampleState sampleState = SampleState.Ready;
    public SampleColorState sampleColorState = SampleColorState.Ready;
    public SampleLineColorState sampleLineColorState = SampleLineColorState.Ready;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLink();
        
        switch (sampleState)
        {
            case SampleState.Ready:
                UpdateReady();
                break;
            case SampleState.Anim:
                UpdateAnim();
                break;
            case SampleState.Return:
                UpdateReturn();
                break;
        }

        switch (sampleColorState)
        {
            case SampleColorState.Ready:
                UpdateColorReady();
                break;
            case SampleColorState.Anim:
                UpdateColorAnim();
                break;
            case SampleColorState.Return:
                UpdateColorReturn();
                break;
        }
        
        switch (sampleLineColorState)
        {
            case SampleLineColorState.Ready:
                UpdateColorReady();
                break;
            case SampleLineColorState.Anim:
                UpdateColorAnim();
                break;
            case SampleLineColorState.Return:
                UpdateColorReturn();
                break;
        }
        
        transform.localScale = Vector3.Lerp(transform.localScale, value, Time.deltaTime * speed);

        gameObject.transform.Find("NodeOut").GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, colorValue/255.0f));
        gameObject.transform.Find("NodeIn").GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, colorValue/255.0f));
        
        //line.material.SetColor("_Color", new Color(lineColorValue, lineColorValue, lineColorValue, 1));
    }
    
    void UpdateLink()
    {
        for (int i = 0; i < GetComponentInParent<SampleAudioVisualizer>().sampleNodes.Count; i++)
        {
            int num = i + 1;
            if (gameObject.name == "SampleNode" + num)
            {
                line.SetPosition(0, this.gameObject.transform.position);
                
                if (i >= 7)
                {
                    line.SetPosition(1, GetComponentInParent<SampleAudioVisualizer>().sampleNodes[0].gameObject.transform.position);
                    return;
                }
                
                line.SetPosition(1, GetComponentInParent<SampleAudioVisualizer>().sampleNodes[i + 1].gameObject.transform.position);
            }
        }
    }
    
    private void UpdateColorReady()
    {
        
    }
    
    private void UpdateColorAnim()
    {
        if (colorValue == _targetGlow)
        {
            _targetGlow = 40.0f;
            sampleColorState = SampleColorState.Return;
            return;
        }
        
        colorValue = Mathf.MoveTowards(colorValue, _targetGlow, Time.deltaTime * colorSpeed);
    }
    
    private void UpdateColorReturn()
    {
        if (colorValue == 40.0f)
        {
            isColorBeat = true;
            sampleColorState = SampleColorState.Return;
            return;
        }
        
        colorValue = Mathf.MoveTowards(colorValue, _targetGlow, Time.deltaTime * colorSpeed);
    }

    void UpdateReady()
    {
        
    }

    void UpdateAnim()
    {
        if (value == _target)    
        {
            _target = Vector3.one;
            sampleState = SampleState.Return;
            return;
        }

        value = Vector3.MoveTowards(value, _target, Time.deltaTime * speed);
    }

    void UpdateReturn()
    {
        if (value == Vector3.one)
        {
            isBeat = true;
            sampleState = SampleState.Ready;
            return;
        }
		
        value = Vector3.MoveTowards(value, _target, Time.deltaTime * speed);
    }

    public void SendData(string beatSampleNodeName, Vector3 target, float targetGlow)
    {
        if (this.gameObject.name == beatSampleNodeName && isBeat)
        {
            isBeat = false;
            _target = target;
            
            sampleState = SampleState.Anim;
        }

        if (this.gameObject.name == beatSampleNodeName && isColorBeat)
        {
            isColorBeat = false;
            _targetGlow = targetGlow;

            sampleColorState = SampleColorState.Anim;
        }
    }
}
