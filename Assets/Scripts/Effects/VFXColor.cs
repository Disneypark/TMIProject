using System.Collections;
using System.Collections.Generic;
using Deform;
using UnityEngine;

public class VFXColor : MonoBehaviour
{
    public enum HitState
    {
        Ready,
        Hit,
        Reset
    }
    
    public HitState hitState = HitState.Ready;
    
    private Material mat;
    private float speed;
    private float glow = 40.0f;
    private float defaultGlow = 40.0f;
    private float targetGlow;
    
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        switch(hitState)
        {
            case HitState.Ready:
                UpdateReady();
                break;
            case HitState.Hit:
                UpdateHit();
                break;
            case HitState.Reset:
                UpdateReset();
                break;
        }
        mat.SetColor("_Color", new Color(1,1,1, glow / 255.0f));
    }
    
    private void UpdateHit()
    {
        if (glow >= targetGlow - 1.0f)
        {
            targetGlow = defaultGlow;
            hitState = HitState.Reset;
            return;
        }

        glow = Mathf.Lerp(glow, targetGlow, Time.deltaTime * speed);
    }

    private void UpdateReset()
    {
        if (glow <= targetGlow + 1.0f)
        {
            hitState = HitState.Ready;
            return;
        }
        glow = Mathf.Lerp(glow, targetGlow, Time.deltaTime * speed);
    }
    
    private void UpdateReady()
    {
        
    }

    public void Hit(float targetGlowPower, float speedPower)
    {
        targetGlow = targetGlowPower;
        speed = speedPower;
        hitState = HitState.Hit;
    }
}
