using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
public class VFXpiano : MonoBehaviour
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
                ripple.Frequency = -1f * velocity;
                ripple.Speed = 1.0f * velocity;
                break;

            case RippleState.Off:
                ripple.Frequency = 0;
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
