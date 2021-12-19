using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
public class VFXdrum : MonoBehaviour
{
    BulgeDeformer bulge;

    // 강 = 1.5 // 중 = 1.0 // 약 = 0.5
    public float velocity = -2f;
    public float timeOffset;
    public BulgeState bulgeState = BulgeState.Off;
    public enum BulgeState
    {
        On,
        Off,
    }
    void Start()
    {
        bulge = GetComponentInChildren<BulgeDeformer>();
    }

    void Update()
    {
        switch (bulgeState)
        {
            case BulgeState.On:
                bulge.Bottom = Mathf.Lerp(bulge.Bottom, velocity, Time.deltaTime * 30);
                VFXcycle();
                break;
            case BulgeState.Off:
                break;
        }
    }

    public void SetState(BulgeState state)
    {
        bulgeState = state;
        switch (bulgeState)
        {
            case BulgeState.On:
                break;

            case BulgeState.Off:
                bulge.Bottom = -1f;
                break;
        }
    }
    void VFXcycle()
    {
        if (Time.time >= timeOffset + 0.2f)
        {
            SetState(BulgeState.Off);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        timeOffset = Time.time;
        if (other.gameObject.tag == "Controller")
            SetState(BulgeState.On);
    }
}
