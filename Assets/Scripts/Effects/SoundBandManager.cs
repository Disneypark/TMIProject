using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundBandManager : MonoBehaviour
{
    [SerializeField] private static SoundBandManager instance = null;
    public static SoundBandManager Instance => instance;

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
    private const string path = "SoundBands/SoundBand";
    private int rnd;
    private float rndSpeed;
    [SerializeField] float rndSpeedMin;
    [SerializeField] float rndSpeedMax;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSoundBand();
        this.transform.eulerAngles = new Vector3(0, 90.0f, 0);
    }

    public GameObject Load(string resourcePath)
    {
        GameObject go = null;

        go = Resources.Load<GameObject>(resourcePath);
        if (!go)
        {
            Debug.LogError("Resources Load Error! filePath = " + resourcePath);
            return null;
        }

        GameObject instancedGo = go;
        instancedGo.name = instancedGo.name.Replace("(Clone)", "");
        return instancedGo;
    }

    public bool GenerateSoundBand()
    {
        for(int i = 0; i < 180; i++)
        {
            GameObject soundBand = Instantiate<GameObject>(Load(path));
            soundBand.SetActive(true);
            soundBand.transform.position = this.transform.position;
            soundBand.transform.parent = this.transform;
            soundBand.transform.position = Vector3.forward * 10.0f;
            //rnd = Random.Range(0, 90);
            rndSpeed = Random.Range(rndSpeedMin, rndSpeedMax);
            soundBand.GetComponent<SoundBand>().ScrollData.speed = rndSpeed;
            soundBand.GetComponent<LineRenderer>().material.SetColor("_TintColor", new Color(Random.value, Random.value, Random.value, 1.0f / 255.0f));
            this.transform.eulerAngles = new Vector3(0, i * 2.0f, 0);
            
        }

        return true;
    }

    public void HitColor(Color getColor)
    {
        SoundBand[] soundBands = GetComponentsInChildren<SoundBand>();

        foreach(SoundBand soundBand in soundBands)
        {
            soundBand.Target_r = getColor.r;
            soundBand.Target_g = getColor.g;
            soundBand.Target_b = getColor.b;
            soundBand.ChangeStart();
        }
    }
}
