using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentFactory : MonoBehaviour
{    
    private Dictionary<string, GameObject> instrumentFileCache = new Dictionary<string, GameObject>();

    public GameObject Load(string resourcePath)
    {
        GameObject go = null;

        if (instrumentFileCache.ContainsKey(resourcePath))
        {
            go = instrumentFileCache[resourcePath];
        }
        else
        {
            go = Resources.Load<GameObject>(resourcePath);
            if (!go)
            {
                Debug.LogError("Resources Load Error filePath = " + resourcePath);
                return null;
            }
            
            instrumentFileCache.Add(resourcePath, go);
        }

        GameObject instancedGo = Instantiate<GameObject>(go);
        string replaceName = instancedGo.name.Replace("(Clone)", "");
        instancedGo.name = replaceName;
        return instancedGo;
    }
}
