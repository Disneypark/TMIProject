using System;
using System.Collections;
using System.Collections.Generic;
using Deform;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class ControllerPadHit : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rig.WakeUp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pad"))
        {
            other.GetComponent<VFXColor>().Hit(150, 12.5f);
            ball.GetComponent<TrailRenderer>().material = other.GetComponent<MeshRenderer>().material;
        }
    }

}
