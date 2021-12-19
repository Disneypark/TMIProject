using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
public class VFXmarimba : MonoBehaviour
{
    StarDeformer star;

    // 강 = 1.5 // 중 = 1.0 // 약 = 0.5
    public float velocity = 1.0f;
    public float timeOffset;
    public StarState starstate = StarState.Off;
    public enum StarState
    {
        On,
        Off,
    }
    void Start()
    {
        star = GetComponentInChildren<StarDeformer>();
    }

    void Update()
    {
        switch (starstate)
        {
            case StarState.On:
                VFXcycle();
                break;
            case StarState.Off:
                break;
        }
    }

    public void SetState(StarState state)
    {
        this.starstate = state;
        switch (this.starstate)
        {
            case StarState.On:
                star.Frequency = 0.1f;
                star.Magnitude = 0.2f * velocity;
                star.Speed = 5;
                break;

            case StarState.Off:
                star.Frequency = 0f;
                star.Magnitude = 0f * velocity;
                star.Speed = 0;
                break;
        }
    }
    void VFXcycle()
    {
        if (Time.time >= timeOffset + 0.7f)
            SetState(StarState.Off);
    }

    private void OnTriggerEnter(Collider other)
    {
        timeOffset = Time.time;
        if (other.gameObject.tag == "Controller")
            SetState(StarState.On);
    }
}
