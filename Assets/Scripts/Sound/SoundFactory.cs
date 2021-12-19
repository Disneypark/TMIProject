using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFactory : MonoBehaviour
{
    Dictionary<string, GameObject> soundFileCache = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Load(string resourcePath)
    {
        GameObject go = null;

        if (soundFileCache.ContainsKey(resourcePath))
        {
            go = soundFileCache[resourcePath];
        }
        else
        {
            go = Resources.Load<GameObject>(resourcePath);

            if (!go)
            {
                Debug.LogError("Resources Load Error! Path = " + resourcePath);
                return null;
            }

            soundFileCache.Add(resourcePath, go);
        }

        GameObject InstancedGO = Instantiate<GameObject>(go);
        string replaceName = InstancedGO.name.Replace("(Clone)", "");
        InstancedGO.name = replaceName;
        return InstancedGO;
    }
}
