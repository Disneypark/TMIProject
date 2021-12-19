using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Sound : MonoBehaviour
{
    private bool isPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(GameObject sound)
    {
        sound.GetComponent<AudioSource>().Play();
    }

    public void InputSound(GameObject sound, GameObject player)
    {
        player.GetComponent<ControllerSound>().RightBall.GetComponent<AudioSource>().clip = sound.GetComponent<AudioSource>().clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (!isPlay)
            {
                isPlay = true;
                GetComponentInChildren<Transform>().Find("Marker").GetComponent<Image>().color = new Color(1,1,1,1);
                Play(this.gameObject);
            }

            StartCoroutine(PlayDelay());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (Controller.Instance.Menu2.GetState(SteamVR_Input_Sources.RightHand))
            {
                InputSound(this.gameObject, other.gameObject);
                other.gameObject.GetComponent<ControllerSound>().RightBall.GetComponent<PlayerBall>().ColorChange(true);
                other.gameObject.GetComponent<ControllerSound>().SoundMarker.transform.Find("SoundMarker").gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInChildren<Transform>().Find("Marker").GetComponent<Image>().color = new Color(1,1,1,25.0f / 255.0f);
    }

    IEnumerator PlayDelay()
    {
        yield return new WaitForSeconds(1f);
        isPlay = false;
    }
}
