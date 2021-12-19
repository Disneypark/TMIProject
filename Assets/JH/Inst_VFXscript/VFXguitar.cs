using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deform;
public class VFXguitar : MonoBehaviour
{
    SquashAndStretchDeformer squash;

    // 강 = 1.5 // 중 = 1.0 // 약 = 0.5
    public float velocity = 0.2f;
    public float timeOffset;
    public SquashState squashstate = SquashState.Off;
    public enum SquashState
    {
        On,
        Off,
    }
    void Start()
    {
        squash = GetComponentInChildren<SquashAndStretchDeformer>();
    }

    void Update()
    {
        switch (squashstate)
        {
            case SquashState.On:
                squash.Factor = Mathf.Lerp(squash.Factor, velocity, Time.deltaTime * 10);
                VFXcycle();
                break;
            case SquashState.Off:
                squash.Factor = Mathf.Lerp(squash.Factor, 0, Time.deltaTime * 10);
                break;
        }
    }

    public void SetState(SquashState state)
    {
        this.squashstate = state;
        switch (this.squashstate)
        {
            case SquashState.On:
                break;

            case SquashState.Off:
                break;
        }
    }
    void VFXcycle()
    {
        if (Time.time >= timeOffset + 0.4f)
            SetState(SquashState.Off);
    }

    private void OnTriggerEnter(Collider other)
    {
        timeOffset = Time.time;
        if (other.gameObject.tag == "Controller")
            SetState(SquashState.On);
    }
}
