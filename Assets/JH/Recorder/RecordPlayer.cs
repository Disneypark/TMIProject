using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RecordPlayer : Metronome
{
    public AudioSource[] sourcePool;
    public Loop recordLoop;
    public TriggerEnterEvent shouldPlayNote;

    bool isLoop;
    public bool isPlaying;
    private float currentPlayTime = 0;

    Metronome metronome;
    public Image loop_BeatCount;

    public Image playImg;
    public Image stopImg;

    public Text playText;
    public Text stopText;

    public void OnClickPlay(bool isLoop)
    {
        this.isLoop = isLoop;
        isPlaying = !isPlaying;
        if (isPlaying) SetState(LoopState.Play);
        else if (!isPlaying) SetState(LoopState.Ready);
    }

    public LoopState state = LoopState.Ready;

    public enum LoopState
    {
        Ready,
        Play,
    }

    private void Start()
    {
        metronome = GameObject.FindGameObjectWithTag("Metronome").GetComponent<Metronome>();
        maxBeats = metronome.maxBeats;
        tempo = metronome.tempo;
        sourcePool = new AudioSource[25];
        for (int i = 0; i < 25; i++)
        {
            sourcePool[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        switch (state)
        {
            case LoopState.Ready:
                break;
            case LoopState.Play:
                break;

        }
        if (isPlaying && currentPlayTime <= recordLoop.recordLength)
        {

            currentPlayTime += Time.deltaTime;
            shouldPlayNote = recordLoop.record.FirstOrDefault(r => r.isNowPlaying(currentPlayTime) && !r.isPlaying);

            if (shouldPlayNote != null && shouldPlayNote.sound != null)
            {
                StartCoroutine(IePlayNote(shouldPlayNote));
            }
        }
        else
        {
            if (!isLoop)
            {
                isPlaying = false;
                SetState(LoopState.Ready);
            }
            currentPlayTime = 0;
        }

    }

    private IEnumerator IePlayNote(TriggerEnterEvent note)
    {
        note.isPlaying = true;
        AudioSource playableSource = sourcePool.First(source => !source.isPlaying);
        playableSource.clip = note.sound;
        playableSource.Play();
        //playableSource.loop = true;

        yield return new WaitForSeconds(note.length);

        //playableSource.Stop();
        note.isPlaying = false;
    }

    // Image FillAmount 코루틴
    //public IEnumerator ieLoopCount()
    //{
    //    while (beats <= maxBeats)
    //    {
    //        loop_BeatCount.fillAmount = ((float)beats) / maxBeats;
    //        beats = (beats + 1) % maxBeats;
    //        yield return new WaitForSeconds(60f / tempo);
    //    }
    //}

    public void SetState(LoopState state)
    {
        switch (state)
        {
            case LoopState.Ready:
                playImg.enabled = true;
                stopImg.enabled = false;
                playText.enabled = true;
                stopText.enabled = false;
                break;

            case LoopState.Play:
                playImg.enabled = false;
                stopImg.enabled = true;
                playText.enabled = false;
                stopText.enabled = true;
                break;
        }
    }
}
