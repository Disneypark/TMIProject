using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
public class VFXbase : MonoBehaviour
{
    SineDeformer sine;

    // 강 = 1.5 // 중 = 1.0 // 약 = 0.5
    public float velocity = 1.0f;
    public float timeOffset;
    public SineState ripplestate = SineState.Off;
    public enum SineState
    {
        On,
        Off,
    }
    void Start()
    {
        sine = GetComponentInChildren<SineDeformer>();
    }

    void Update()
    {
        switch (ripplestate)
        {
            case SineState.On:
                VFXcycle();
                break;
            case SineState.Off:
                break;
        }
    }

    public void SetState(SineState state)
    {
        this.ripplestate = state;
        switch (this.ripplestate)
        {
            case SineState.On:
                if (sine == null)
                {
                    return;
                }
                sine.Frequency = 0.3f;
                sine.Amplitude = 0.1f;
                sine.Speed = 2 * velocity;
                break;

            case SineState.Off:
                if (sine == null)
                {
                    return;
                }
                sine.Frequency = 0;
                sine.Amplitude = 0;
                sine.Speed = 0;
                break;
        }
    }
    void VFXcycle()
    {
        if (Time.time >= timeOffset + 0.7f)
            SetState(SineState.Off);
    }

    private void OnTriggerEnter(Collider other)
    {
        timeOffset = Time.time;
        if (other.gameObject.tag == "Controller")
            SetState(SineState.On);
    }
}
