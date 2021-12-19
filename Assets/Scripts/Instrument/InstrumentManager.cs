using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InstrumentData
{
    public string page;
    public Transform instrumentParent;
    public int instrumentCacheCount;
    public List<Sprite> instrumentImage = new List<Sprite>();
    public List<string> instrumentPath = new List<string>();
}

public class InstrumentManager : MonoBehaviour
{
    [SerializeField] private static InstrumentManager instance = null;
    public static InstrumentManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("singletone error!");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    [SerializeField] private InstrumentFactory instrumentFactory;
    [SerializeField] private string instrumentPrefabPath;
    [SerializeField] private InstrumentData[] instrumentData;
    [SerializeField] Dictionary<string, Queue<GameObject>> instruments = new Dictionary<string, Queue<GameObject>>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < instrumentData.Length; i++)
        {
            string _page = instrumentData[i].page;
            Transform _instrumentParent = instrumentData[i].instrumentParent;
            int _instrumentCacheCount = instrumentData[i].instrumentCacheCount;
            List<Sprite> _instrumentImage = instrumentData[i].instrumentImage;
            List<string> _instrumentPath = instrumentData[i].instrumentPath;

            GenerateInstrument(_page, _instrumentCacheCount, _instrumentParent, _instrumentImage, _instrumentPath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GenerateInstrument(string page, int instrumentCacheCount, Transform instrumentParent, List<Sprite> instrumentImage, List<string> instrumentPath)
    {
        if (instruments.ContainsKey(page))
        {
            Debug.LogWarning("Already instrument page Instantiate" + page);
            return false;
        }
        else
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < instrumentCacheCount; i++)
            {
                GameObject go = instrumentFactory.Load(instrumentPrefabPath);

                if (!go)
                {
                    Debug.LogError("Instrument Prefab Load Error" + instrumentPrefabPath);
                    return false;
                }

                go.transform.SetParent(instrumentParent);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0);
                go.transform.localRotation = Quaternion.Euler(Vector3.zero);
                go.GetComponent<Image>().sprite = instrumentImage[i];
                char divide = '/';
                string[] split = instrumentPath[i].Split(divide);
                go.name = split.Last();
                go.GetComponentInChildren<Text>().text = split.Last();
                go.GetComponent<InstrumentMenu>().FindName = split.Last();
                go.GetComponent<InstrumentMenu>().ResourcePath = instrumentPath[i];

                queue.Enqueue(go);
            }
            
            instruments.Add(page, queue);
        }
        
        return true;
    }
}
