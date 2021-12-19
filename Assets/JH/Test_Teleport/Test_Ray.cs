using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Test_Ray : MonoBehaviour
{
    public GameObject RayPos;
    RadialMenu radial;

    private void Update()
    {
        OnRadialMenu();
    }

    void OnRadialMenu()
    {
        if (Controller.Instance.Trigger.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            RaycastHit hit;

            if (Physics.Raycast(RayPos.transform.position, RayPos.transform.forward, out hit, 10.0f) && hit.transform.tag == "Pad")
            {
                if (radial == null) radial = hit.transform.GetComponent<InstrumentPad>().radial;
                if (radial != null)
                {
                    radial = hit.transform.GetComponent<InstrumentPad>().radial;
                    radial.OnEnabledMenu();
                }
            }
        }
    }
}
