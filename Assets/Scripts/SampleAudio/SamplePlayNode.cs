using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamplePlayNode : MonoBehaviour
{
    private bool isPlayDelay = false;
    private bool isPlayStop = false;

    public enum PlayState
    {
        None,
        Play,
        Stop
    }

    public PlayState playState = PlayState.None;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<AudioSource>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectState()
    {
        switch (playState)
        {
            case PlayState.None:
                break;
            case PlayState.Play:
                UpdatePlay();
                break;
            case PlayState.Stop:
                UpdateStop();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Controller") && !isPlayDelay)
        {
            isPlayDelay = true;
            StartCoroutine(PlayDelay());
            isPlayStop = !isPlayStop;
            if (isPlayStop)
            {
                playState = PlayState.Play;
                SelectState();
                return;
            }

            playState = PlayState.Stop;
            SelectState();
        }
    }

    void UpdatePlay()
    {
        GetComponentInParent<AudioSource>().Stop();
        GetComponentInParent<AudioSource>().Play();
        GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 180.0f/255.0f));
        GetComponentInChildren<Text>().text = "II";
    }

    void UpdateStop()
    {
        GetComponentInParent<AudioSource>().Stop();
        GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1,1,1, 40.0f/255.0f));
        GetComponentInChildren<Text>().text = "▶";
    }

    IEnumerator PlayDelay()
    {
        yield return new WaitForSeconds(1.0f);
        isPlayDelay = false;
    }
}
