using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
public class VFXpad5 : MonoBehaviour
{
    RippleDeformer ripple;

    // 강 = 1.5 // 중 = 1.0 // 약 = 0.5
    public float velocity = 1.0f;
    public float timeOffset;
    public RippleState ripplestate = RippleState.Off;
    public enum RippleState
    {
        On,
        Off,
    }
    void Start()
    {
        ripple = GetComponentInChildren<RippleDeformer>();
    }

    void Update()
    {
        switch (ripplestate)
        {
            case RippleState.On:
                VFXcycle();
                break;
            case RippleState.Off:
                break;
        }
    }

    public void SetState(RippleState state)
    {
        this.ripplestate = state;
        switch (this.ripplestate)
        {
            case RippleState.On:
                ripple.Frequency = 1f;
                ripple.Amplitude = 1f;
                ripple.Speed = 2.0f * velocity;
                break;

            case RippleState.Off:
                ripple.Frequency = 0f;
                ripple.Amplitude = 0f;
                ripple.Speed = 0;
                break;
        }
    }
    void VFXcycle()
    {
        if (Time.time >= timeOffset + 0.7f)
            SetState(RippleState.Off);
    }

    private void OnTriggerEnter(Collider other)
    {
        timeOffset = Time.time;
        if (other.gameObject.tag == "Controller")
            SetState(RippleState.On);
    }
}
