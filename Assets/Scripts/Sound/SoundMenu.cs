using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundMenuData
{
    public string instrumentName;
    public Transform instrument;
    public int soundCacheCount;
    public List<string> instrumentSoundPath = new List<string>();
}

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private string soundMenuPrefabPath;
    public string SoundMenuPrefabPath => soundMenuPrefabPath;
    
    [SerializeField] SoundMenuData[] soundMenus;
    public SoundMenuData[] SoundMenuData => soundMenus;
    
}
