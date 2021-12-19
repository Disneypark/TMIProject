using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAudioFactory : MonoBehaviour
{
    private Dictionary<string, GameObject> sampleAudioFileCaches = new Dictionary<string, GameObject>();

    public GameObject Load(string resourcePath)
    {
        GameObject go = null;

        if (sampleAudioFileCaches.ContainsKey(resourcePath))
        {
            go = sampleAudioFileCaches[resourcePath];
        }
        else
        {
            go = Resources.Load<GameObject>(resourcePath);

            if (!go)
            {
                Debug.Log("Load Error filePath = " + resourcePath);
                return null;
            }
            
            sampleAudioFileCaches.Add(resourcePath, go);
        }

        GameObject instancedGO = Instantiate<GameObject>(go);
        string replaceName = instancedGO.name.Replace("(Clone)", "");
        instancedGO.name = replaceName;
        return instancedGO;
    }
}
