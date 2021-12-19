using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [SerializeField] GameObject playerBallSelect;
    private new AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorChange(bool isSelect)
    {
        playerBallSelect.SetActive(isSelect);
    }

    public void AudioRemove()
    {
        audio.clip = null;
    }
}
