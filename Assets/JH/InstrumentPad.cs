using System;
using System.Collections;
using System.Collections.Generic;
using Deform;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InstrumentPad : MonoBehaviour
{
    TriggerEnterEvent triggerEvent;
    Renderer padRend;

    [SerializeField]
    public AudioClip sound;
    private AudioSource note;
    public RadialMenu radial;

    public int padIndex;

    void Start()
    {
        if (note == null) note = gameObject.AddComponent<AudioSource>();

        padRend = GetComponent<Renderer>();
        note.clip = sound;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            note.Play();
            triggerEvent = new TriggerEnterEvent(sound, Record.Instance.recordStartTime, padIndex);

            SoundBandManager.Instance.HitColor(GetComponent<MeshRenderer>().material.GetColor("_EmissionColor"));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            triggerEvent.OnTriggerExit();
            Record.Instance.AddPressButtonEvent(triggerEvent, padRend.material.GetColor("_EmissionColor"));

        }
    }

    public void SoundOutput(AudioClip clip)
    {
        sound = clip;
        note.clip = clip;
    }
}
