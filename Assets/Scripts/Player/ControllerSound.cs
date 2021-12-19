using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerSound : MonoBehaviour
{
    [SerializeField] private GameObject rightBall;
    public GameObject RightBall => rightBall;

    [SerializeField] private GameObject soundMarker;
    public GameObject SoundMarker => soundMarker;

    [SerializeField] private ParticleSystem soundParticle;

    // Update is called once per frame
    void Update()
    {
        if (Controller.Instance.Menu2.GetStateUp(SteamVR_Input_Sources.RightHand) && !Controller.Instance.IsPadTouch)
        {
            SoundInputFail(false);
        }
    }
    
    void SoundInputFail(bool isSelect)
    {
        rightBall.GetComponent<PlayerBall>().AudioRemove();
        rightBall.GetComponent<PlayerBall>().ColorChange(isSelect);
        soundMarker.transform.Find("SoundMarker").gameObject.SetActive(false);
    }
    
    void SoundOutput(GameObject sound, bool isSelect)
    {
        if (rightBall.GetComponent<AudioSource>().clip == null)
        {
            Debug.Log("inputSound is null");
            return;
        }
         
        sound.GetComponent<InstrumentPad>().SoundOutput(rightBall.GetComponent<AudioSource>().clip);
        rightBall.GetComponent<PlayerBall>().ColorChange(isSelect);
        rightBall.GetComponent<AudioSource>().clip = null;
        soundMarker.transform.Find("SoundMarker").gameObject.SetActive(false);
        soundParticle.transform.position = sound.transform.position;
        soundParticle.gameObject.SetActive(true);
        StartCoroutine(SoundParticleFalse());
    }

    IEnumerator SoundParticleFalse()
    {
        yield return new WaitForSeconds(1.0f);
        soundParticle.gameObject.SetActive(false);
    }

    
    // 오른쪽 컨트롤러가 악기에 닿았을때 사운드를 악기에 할당시킨다
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pad"))
        {
            if (Controller.Instance.Menu2.GetStateUp(SteamVR_Input_Sources.RightHand) && Controller.Instance.IsPadTouch)
            {
                SoundOutput(other.gameObject, false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pad"))
        {
            Controller.Instance.IsPadTouch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pad"))
        {
            Controller.Instance.IsPadTouch = false;
        }
    }
}
