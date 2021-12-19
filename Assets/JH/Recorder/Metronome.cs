using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metronome : MonoBehaviour
{
    public AudioSource audioSource;
    public bool isOnMetroSound = false;

    public Image metronome_BeatsCount;
    public Image recorder_BeatCount;
    public Text metronomeText;
    public Text recorderText;

    public int beats;
    public int tempo = 100;
    public int maxBeats = 4;

    Coroutine countCoroutine;

    private void Start()
    {
        StartCoroutine(ieCountDown());
    }

    private void Update()
    {

    }

    public void OnChangeTempo(int tempo)
    {
        this.tempo = tempo;
    }

    public void OnChangeBeats(int beats)
    {
        this.maxBeats = beats;
    }

    //[ContextMenu("play metronome")]
    public void PlayMetronome()
    {
        StopMetronome();
        countCoroutine = StartCoroutine(ieCountDown());
    }

    //[ContextMenu("stop metronome")]
    public void StopMetronome()
    {
        if (countCoroutine != null)
        {
            StopCoroutine(countCoroutine);
            countCoroutine = null;
        }
    }

    IEnumerator ieCountDown()
    {
        while (beats <= maxBeats)
        {
            if (metronomeText) metronomeText.text = (beats + 1).ToString();
            if (recorder_BeatCount && Record.Instance.isRecording || Record.Instance.isCounting) recorder_BeatCount.fillAmount = ((float)beats) / (maxBeats);
            if (recorderText) recorderText.text = (maxBeats - beats).ToString();

            metronome_BeatsCount.fillAmount = ((float)beats) / (maxBeats);
            beats = (beats + 1) % maxBeats;

            if (Record.Instance.isCounting && beats == 1) Record.Instance.Recording();
            if (recorderText) recorderText.enabled = Record.Instance.isCounting;

            if (isOnMetroSound) audioSource.Play();
            yield return new WaitForSeconds(60f / tempo);
        }
    }
    public void OnMetroSound()
    {
        isOnMetroSound = !isOnMetroSound;
    }

}
