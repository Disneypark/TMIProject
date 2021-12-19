using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundFactory soundFactory;

    [SerializeField] Dictionary<string, Queue<GameObject>> sounds = new Dictionary<string, Queue<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SoundSystem.Instance.SoundMenu.SoundMenuData.Length; i++)
        {
            string instrumentName = SoundSystem.Instance.SoundMenu.SoundMenuData[i].instrumentName;
            int soundCacheCount = SoundSystem.Instance.SoundMenu.SoundMenuData[i].soundCacheCount;
            List<string> instrumentSoundPath = SoundSystem.Instance.SoundMenu.SoundMenuData[i].instrumentSoundPath;
            Transform parent = SoundSystem.Instance.SoundMenu.SoundMenuData[i].instrument;
            
            GenerateSound(instrumentName, soundCacheCount, instrumentSoundPath, parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GenerateSound(string instrumentName, int soundCacheCount, List<string> instrumentSoundPath, Transform parent)
    {
        if (sounds.ContainsKey(instrumentName))
        {
            Debug.LogWarning("Already Sound Generated Instrument = " + instrumentName);
            return false;
        }
        else
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            List<string> soundPath = new List<string>();
            
            for (int i = 0; i < soundCacheCount; i++)
            {
                GameObject go = soundFactory.Load(SoundSystem.Instance.SoundMenu.SoundMenuPrefabPath);
            
                if (!go)
                {
                    Debug.LogError("SoundFactory GenerateSound Load Error!");
                    return false;
                }
            
                go.transform.SetParent(parent);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0);
                go.transform.localRotation = Quaternion.Euler(Vector3.zero);

                if (instrumentSoundPath[i] != String.Empty)
                {
                    AudioSource audio = go.AddComponent<AudioSource>();
                    audio.playOnAwake = false;
                    audio.clip = Resources.Load(instrumentSoundPath[i]) as AudioClip;
                }
                else
                {
                    go.AddComponent<AudioSource>();
                }

                queue.Enqueue(go);
            }
            
            sounds.Add(instrumentName, queue);
        }
        
        return true;
    }
}
