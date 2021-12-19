using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private static SoundSystem instance = null;
    public static SoundSystem Instance => instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("singleton error!");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    [SerializeField] private SoundMenu soundMenu;
    public SoundMenu SoundMenu => soundMenu;
}
