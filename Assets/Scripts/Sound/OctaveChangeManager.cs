using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OctaveChangeManager : MonoBehaviour
{
    #region 변수들

    // 옥타브를 바꾸고 싶은 오디오소스가 달린 오브젝트
    [SerializeField] private GameObject[] sounds;
    //

    private const int plus = 12;
    private const int minus = -12;

    private string[] split;
    private int changeIndex;
    private string path;

    private int instCount;

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        if (GetComponentInParent<Instrument>().padList == null)
        {
            return;
        }

        instCount = GetComponentInParent<Instrument>().padList.Length;
        sounds = new GameObject[instCount];

        for (int i = 0; i < instCount; i++)
        {
            sounds[i] = GetComponentInParent<Instrument>().padList[i].gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlusOctave()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].GetComponent<AudioSource>().clip = OctaveChange(PathFinder(sounds[i].GetComponent<AudioSource>().clip.name, plus));
            sounds[i].GetComponent<InstrumentPad>().sound = sounds[i].GetComponent<AudioSource>().clip;
        }
    }

    public void OnMinusOctave()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].GetComponent<AudioSource>().clip = OctaveChange(PathFinder(sounds[i].GetComponent<AudioSource>().clip.name, minus));
            sounds[i].GetComponent<InstrumentPad>().sound = sounds[i].GetComponent<AudioSource>().clip;
        }
    }

    #region 함수들

    private string PathFinder(string soundName, int changeNum)
    {
        char slash = '/';

        foreach (SoundMenuData soundMenuData in SoundSystem.Instance.SoundMenu.SoundMenuData)
        {
            for (int i = 0; i < soundMenuData.instrumentSoundPath.Count; i++)
            {
                split = soundMenuData.instrumentSoundPath[i].Split(slash);

                if (split.Last() == soundName)
                {
                    changeIndex = OctaveMath(i, changeNum, soundMenuData.instrumentSoundPath.Count);
                    path = soundMenuData.instrumentSoundPath[changeIndex];
                }
            }
        }
        return path;
    }

    private int OctaveMath(int index, int changeNum, int count)
    {
        int reIndex = index + changeNum;

        if (reIndex < 0 || reIndex >= count)
        {
            Debug.Log("Octave is too lower or higher");
            return index;
        }

        return reIndex;
    }

    private AudioClip OctaveChange(string resourcePath)
    {
        AudioClip audioClip = null;

        audioClip = Resources.Load<AudioClip>(resourcePath);
        if (!audioClip)
        {
            Debug.LogError("Resources Load Error! FilePath = " + resourcePath);
            return null;
        }

        return audioClip;
    }

    #endregion

}
