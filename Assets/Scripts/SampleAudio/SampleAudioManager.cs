using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SampleAudioData
{
    public string sampleCategory;
    public Transform sampleParent;
    public int sampleCacheCount;
    public List<Sprite> sampleImage = new List<Sprite>();
    public List<string> samplePath = new List<string>();
    public List<string> sampleSoundPath = new List<string>();
}

public class SampleAudioManager : MonoBehaviour
{
    [SerializeField] private static SampleAudioManager instance = null;
    public static SampleAudioManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Singleton error");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    [SerializeField] private SampleAudioFactory sampleAudioFactory;
    [SerializeField] private string sampleAudioPrefabPath;
    [SerializeField] private SampleAudioData[] sampleAudioData;
    [SerializeField] Dictionary<string, Queue<GameObject>> samples = new Dictionary<string, Queue<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sampleAudioData.Length; i++)
        {
            string _sampleCategory = sampleAudioData[i].sampleCategory;
            Transform _sampleParent = sampleAudioData[i].sampleParent;
            int _sampleCacheCount = sampleAudioData[i].sampleCacheCount;
            List<Sprite> _sampleImage = sampleAudioData[i].sampleImage;
            List<string> _samplePath = sampleAudioData[i].samplePath;
            List<string> _sampleSoundPath = sampleAudioData[i].sampleSoundPath;

            GenerateSample(_sampleCategory, _sampleParent, _sampleCacheCount, _sampleImage, _samplePath, _sampleSoundPath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GenerateSample(string sampleCategory, Transform sampleParent, int sampleCacheCount, List<Sprite> sampleImage, List<string> samplePath, List<string> sampleSoundPath)
    {
        if (samples.ContainsKey(sampleCategory))
        {
            Debug.LogWarning("Already SampleCategory Instantiate sample = " + sampleCategory);
            return false;
        }
        else
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < sampleCacheCount; i++)
            {
                GameObject go = sampleAudioFactory.Load(sampleAudioPrefabPath);

                if (!go)
                {
                    Debug.LogError("sample Prefab Load Error = " + sampleAudioPrefabPath);
                    return false;
                }
                
                go.transform.SetParent(sampleParent);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0);
                go.transform.localRotation = Quaternion.Euler(Vector3.zero);
                go.GetComponent<Image>().sprite = sampleImage[i];
                char divide = '/';
                string[] split = samplePath[i].Split(divide);
                go.name = split.Last();
                go.GetComponentInChildren<Text>().text = split.Last();
                go.GetComponent<SampleMenu>().FindName = split.Last();
                go.GetComponent<SampleMenu>().ResourcePath = samplePath[i];
                go.GetComponent<SampleMenu>().SampleClipResourcePath = sampleSoundPath[i];
                
                queue.Enqueue(go);
            }
            
            samples.Add(sampleCategory, queue);
        }

        return true;
    }
}
