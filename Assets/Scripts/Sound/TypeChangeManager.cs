using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TypeChangeManager : MonoBehaviour
{
    #region 변수들

    List<string> typeInst = new List<string>(3);

    // 타입 바꾸려고 하는 오디오소스
    [SerializeField] private GameObject[] sounds;

    private string instName;
    private string typeName;
    private string typeFolder;

    private const string path = "Sounds";

    private bool isClassic = false;
    private bool isElectric = false;
    private int instCount;

    #endregion

    private void Start()
    {
        #region 시작시에 타입리스트 생성

        typeInst.Add("Bass");
        typeInst.Add("Guitar");
        typeInst.Add("Piano");

        #endregion

        if (GetComponentInParent<Instrument>().padList == null) return;

        instCount = GetComponentInParent<Instrument>().padList.Length;
        sounds = new GameObject[instCount];

        for (int i = 0; i < instCount; i++)
        {
            sounds[i] = GetComponentInParent<Instrument>().padList[i].gameObject;
        }
    }

    public void OnToClassicTypeChange()
    {
        if (isClassic) return;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].GetComponent<AudioSource>().clip = TypeChange(TypeChangeParsing(sounds[i].GetComponent<AudioSource>().clip.name));
            sounds[i].GetComponent<InstrumentPad>().sound = sounds[i].GetComponent<AudioSource>().clip;
        }
    }

    public void OnToElectricTypeChange()
    {
        if (isElectric) return;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].GetComponent<AudioSource>().clip = TypeChange(TypeChangeParsing(sounds[i].GetComponent<AudioSource>().clip.name));
            sounds[i].GetComponent<InstrumentPad>().sound = sounds[i].GetComponent<AudioSource>().clip;
        }
    }

    #region 함수들

    private AudioClip TypeChange(string resourcePath)
    {
        AudioClip soundClip = null;

        soundClip = Resources.Load<AudioClip>(resourcePath);

        if (!soundClip)
        {
            return null;
        }

        return soundClip;
    }

    private string TypeChangeParsing(string clipName)
    {
        char slash = '/';
        char divide = '_';
        string[] splite = clipName.Split(divide);
        for (int i = 0; i < typeInst.Count; i++)
        {
            if (typeInst[i] == splite.First())
            {
                instName = typeInst[i];

                if (splite[1] == "Grand" || splite[1] == "Classic")
                {
                    typeFolder = instName + "Electric";
                    typeName = "Electric";

                    isClassic = true;
                    isElectric = false;
                }
                else if (splite[1] == "Electric")
                {
                    typeFolder = instName + "Classic";

                    if (instName == "Piano")
                    {
                        typeName = "Grand";
                    }
                    else if (instName == "Guitar" || instName == "Bass")
                    {
                        typeName = "Classic";
                    }

                    isClassic = false;
                    isElectric = true;
                }
            }
        }

        string resourcePath = path + slash + typeFolder + slash + instName + divide + typeName + divide + splite[2] +
                       divide + splite[3];

        return resourcePath;
    }
    #endregion

}
