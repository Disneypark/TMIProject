using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private static ButtonManager instance = null;
    public static ButtonManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("singletone error");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public float buttonRotateSpeed;
    public Transform camRig;

    #region MovePinch

    private GameObject movePinch;

    #endregion

    #region 0st

    private GameObject startMenu;

    #endregion
    
    #region 1st

    private GameObject mainMenu;
    private GameObject applicationMenu;

    #endregion
    
    #region 2st
    
    // Main Menu
    private GameObject soundMenu;
    private GameObject effectsMenu;
    private GameObject instrumentMenu;
    private GameObject sampleMenu;
    
    // Application Menu
    private GameObject settingMenu;
    private GameObject layoutMenu;
    
    #endregion 2st
    
    #region 3st
    // Main Menu - Sound Menu
    private GameObject pianoMenu;
    private GameObject guitarMenu;
    private GameObject bassMenu;
    private GameObject drumMenu;
    private GameObject marimbarMenu;
    private GameObject vibraphoneMenu;
    private GameObject synthMenu;

    // Main Menu - Effects Menu
    private GameObject lightMenu;
    private GameObject colorMenu;
    
    // Main Menu - Instrument Menu
    private GameObject subInstrumentMenu;
    
    // Main Menu - Sample Menu
    private GameObject samplePianoMenu;
    private GameObject sampleDrumMenu;
    private GameObject sampleBassMenu;
    
    // Application - layout
    private GameObject loadMenu;
    private GameObject saveMenu;
    
    #endregion 3st

    #region 4st

    // Main Menu - Sound Menu - SubSoundMenu
    private GameObject pianoClassicMenu;
    private GameObject pianoElectricMenu;
    private GameObject guitarClassicMenu;
    private GameObject guitarElectricMenu;
    private GameObject bassClassicMenu;
    private GameObject bassElectricMenu;
    private GameObject synthAMenu;
    private GameObject synthBMenu;
    private GameObject synthCMenu;
    private GameObject synthDMenu;
    private GameObject synthEMenu;

    #endregion 4st

    #region Data

    // List

    [SerializeField] List<GameObject> menuList1st = new List<GameObject>();
    public List<GameObject> MenuList1st => menuList1st;
    
    [SerializeField] List<GameObject> menuList2st = new List<GameObject>();
    public List<GameObject> MenuList2st => menuList2st;
    
    [SerializeField] List<GameObject> menuList3st = new List<GameObject>();
    public List<GameObject> MenuList3st => menuList3st;
    
    [SerializeField] List<GameObject> menuList4st = new List<GameObject>();
    private static readonly int Zero = Animator.StringToHash("Zero");
    public List<GameObject> MenuList4st => menuList4st;

    #endregion Data

    // Start is called before the first frame update
    void Start()
    {
        #region MovePinch

        movePinch = GameObject.Find("Menu").transform.Find("MovePinch").gameObject;
        
        #endregion

        #region 0st

        //startMenu = GameObject.Find("0st_Menu_Group").transform.Find("StartMenu").gameObject;
        
        #endregion
        
        #region 1st

        mainMenu = GameObject.Find("1st_Menu_Group").transform.Find("MainMenu").gameObject;
        applicationMenu = GameObject.Find("1st_Menu_Group").transform.Find("ApplicationMenu").gameObject;
        
        menuList1st.Add(mainMenu);
        menuList1st.Add(applicationMenu);

        #endregion 1st
        
        #region 2st
        
        // Main Menu
        soundMenu = GameObject.Find("SubMainMenu").transform.Find("SoundMenu").gameObject;
        effectsMenu = GameObject.Find("SubMainMenu").transform.Find("EffectsMenu").gameObject;
        instrumentMenu = GameObject.Find("SubMainMenu").transform.Find("InstrumentMenu").gameObject;
        sampleMenu = GameObject.Find("SubMainMenu").transform.Find("SampleMenu").gameObject;
        
        menuList2st.Add(soundMenu);
        menuList2st.Add(effectsMenu);
        menuList2st.Add(instrumentMenu);
        menuList2st.Add(sampleMenu);
        
        // Application Menu
        //settingMenu = GameObject.Find("SubApplicationMenu").transform.Find("SoundMenu").gameObject;
        layoutMenu = GameObject.Find("SubApplicationMenu").transform.Find("LayOutMenu").gameObject;
        
        menuList2st.Add(layoutMenu);
        
        #endregion 2st
        
        #region 3st
        
        // Main Menu - Sound Menu
        pianoMenu = GameObject.Find("SubSoundMenu").transform.Find("PianoMenu").gameObject;
        guitarMenu = GameObject.Find("SubSoundMenu").transform.Find("GuitarMenu").gameObject;
        bassMenu = GameObject.Find("SubSoundMenu").transform.Find("BassMenu").gameObject;
        drumMenu = GameObject.Find("SubSoundMenu").transform.Find("DrumMenu").gameObject;
        marimbarMenu = GameObject.Find("SubSoundMenu").transform.Find("MarimbarMenu").gameObject;
        vibraphoneMenu = GameObject.Find("SubSoundMenu").transform.Find("VibraphoneMenu").gameObject;
        synthMenu = GameObject.Find("SubSoundMenu").transform.Find("SynthMenu").gameObject;
        
        menuList3st.Add(pianoMenu);
        menuList3st.Add(guitarMenu);
        menuList3st.Add(bassMenu);
        menuList3st.Add(drumMenu);
        menuList3st.Add(marimbarMenu);
        menuList3st.Add(vibraphoneMenu);
        menuList3st.Add(synthMenu);
        
        // Main Menu - Effects Menu
        lightMenu = GameObject.Find("SubEffectsMenu").transform.Find("LightMenu").gameObject;
        colorMenu = GameObject.Find("SubEffectsMenu").transform.Find("ColorMenu").gameObject;
        
        menuList3st.Add(lightMenu);
        menuList3st.Add(colorMenu);

        // Main Menu - Sample Menu
        samplePianoMenu = GameObject.Find("SubSampleMenu").transform.Find("SamplePianoMenu").gameObject;
        sampleDrumMenu = GameObject.Find("SubSampleMenu").transform.Find("SampleDrumMenu").gameObject;
        sampleBassMenu = GameObject.Find("SubSampleMenu").transform.Find("SampleBassMenu").gameObject;
        
        menuList3st.Add(samplePianoMenu);
        menuList3st.Add(sampleDrumMenu);
        menuList3st.Add(sampleBassMenu);
        
        // Application - layout Menu
        loadMenu = GameObject.Find("SubSubApplicationMenu").transform.Find("LoadMenu").gameObject;
        saveMenu = GameObject.Find("SubSubApplicationMenu").transform.Find("SaveMenu").gameObject;
        
        menuList3st.Add(loadMenu);
        menuList3st.Add(saveMenu);
        
        #endregion 3st

        #region 4st

        // Main Menu - Sound Menu - SubSoundMenu
        pianoClassicMenu = GameObject.Find("SubSubSoundMenu").transform.Find("PianoClassicMenu").gameObject;
        pianoElectricMenu = GameObject.Find("SubSubSoundMenu").transform.Find("PianoElectricMenu").gameObject;
        guitarClassicMenu = GameObject.Find("SubSubSoundMenu").transform.Find("GuitarClassicMenu").gameObject;
        guitarElectricMenu = GameObject.Find("SubSubSoundMenu").transform.Find("GuitarElectricMenu").gameObject;
        bassClassicMenu = GameObject.Find("SubSubSoundMenu").transform.Find("BassClassicMenu").gameObject;
        bassElectricMenu = GameObject.Find("SubSubSoundMenu").transform.Find("BassElectricMenu").gameObject;
        synthAMenu = GameObject.Find("SubSubSoundMenu").transform.Find("SynthAMenu").gameObject;
        synthBMenu = GameObject.Find("SubSubSoundMenu").transform.Find("SynthBMenu").gameObject;
        synthCMenu = GameObject.Find("SubSubSoundMenu").transform.Find("SynthCMenu").gameObject;
        synthDMenu = GameObject.Find("SubSubSoundMenu").transform.Find("SynthDMenu").gameObject;
        synthEMenu = GameObject.Find("SubSubSoundMenu").transform.Find("SynthEMenu").gameObject;
        
        menuList4st.Add(pianoClassicMenu);
        menuList4st.Add(pianoElectricMenu);
        menuList4st.Add(guitarClassicMenu);
        menuList4st.Add(guitarElectricMenu);
        menuList4st.Add(bassClassicMenu);
        menuList4st.Add(bassElectricMenu);
        menuList4st.Add(synthAMenu);
        menuList4st.Add(synthBMenu);
        menuList4st.Add(synthCMenu);
        menuList4st.Add(synthDMenu);
        menuList4st.Add(synthEMenu);

        #endregion 4st
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonMovePinch()
    {
        movePinch.SetActive(true);
    }
    
    public void OnButtonFind(string path)
    {
        for (int i = 0; i < menuList1st.Count; i++)
        {
            if (menuList1st[i].name == path)
            {
                for (int j = 0; j < menuList1st.Count; j++)
                {
                    if (menuList1st[j].activeSelf == true)
                    {
                        menuList1st[j].SetActive(false);
                        
                        for (int k = 0; k < menuList2st.Count; k++)
                        {
                            if (menuList2st[k].activeSelf == true)
                            {
                                menuList2st[k].SetActive(false);
                                
                                for (int m = 0; m < menuList3st.Count; m++)
                                {
                                    if (menuList3st[m].activeSelf == true)
                                    {
                                        menuList3st[m].SetActive(false);

                                        for (int t = 0; t < menuList4st.Count; t++)
                                        {
                                            if (menuList4st[t].activeSelf == true)
                                            {
                                                menuList4st[t].SetActive(false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                ButtonPivot.Instance.ForwardRotate(new Vector3(0, 0, 0), buttonRotateSpeed);
                menuList1st[i].SetActive(true);
                
                for (int q = 0; q < menuList1st.Count; q++)
                {
                    BoxCollider[] allCollider = menuList1st[q].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = true;
                    }
                }
                
                for (int w = 0; w < menuList2st.Count; w++)
                {
                    BoxCollider[] allCollider = menuList2st[w].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = true;
                    }
                }
                
                for (int e = 0; e < menuList3st.Count; e++)
                {
                    BoxCollider[] allCollider = menuList3st[e].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = true;
                    }
                }
                
                for (int r = 0; r < menuList4st.Count; r++)
                {
                    BoxCollider[] allCollider = menuList4st[r].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = true;
                    }
                }
                
                ButtonPivot.Instance.EnablePosRot();
            }
        }
        
        for (int i = 0; i < menuList2st.Count; i++)
        {
            if (menuList2st[i].name == path)
            {
                for (int j = 0; j < menuList2st.Count; j++)
                {
                    if (menuList2st[j].activeSelf == true)
                    {
                        menuList2st[j].SetActive(false);

                        for (int k = 0; k < menuList3st.Count; k++)
                        {
                            if (menuList3st[k].activeSelf == true)
                            {
                                menuList3st[k].SetActive(false);

                                for (int m = 0; m < menuList4st.Count; m++)
                                {
                                    if (menuList4st[m].activeSelf == true)
                                    {
                                        menuList4st[m].SetActive(false);
                                    }
                                }
                            }
                        }
                    }
                }
                
                ButtonPivot.Instance.ForwardRotate(new Vector3(0, 90, 0), buttonRotateSpeed);
                menuList2st[i].SetActive(true);
                
                for (int q = 0; q < menuList1st.Count; q++)
                {
                    BoxCollider[] allCollider = menuList1st[q].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = false;
                    }
                }
            }
        }
        
        for (int i = 0; i < menuList3st.Count; i++)
        {
            if (menuList3st[i].name == path)
            {
                for (int j = 0; j < menuList3st.Count; j++)
                {
                    if (menuList3st[j].activeSelf == true)
                    {
                        menuList3st[j].SetActive(false);

                        for (int k = 0; k < menuList4st.Count; k++)
                        {
                            if (menuList4st[k].activeSelf == true)
                            {
                                menuList4st[k].SetActive(false);
                            }
                        }
                    }
                }
                
                ButtonPivot.Instance.ForwardRotate(new Vector3(0, 180, 0), buttonRotateSpeed);
                menuList3st[i].SetActive(true);
                
                for (int q = 0; q < menuList2st.Count; q++)
                {
                    BoxCollider[] allCollider = menuList2st[q].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = false;
                    }
                }
            }
        }

        for (int i = 0; i < menuList4st.Count; i++)
        {
            if (menuList4st[i].name == path)
            {

                for (int j = 0; j < menuList4st.Count; j++)
                {
                    if (menuList4st[j].activeSelf == true)
                    {
                        menuList4st[j].SetActive(false);
                    }
                }
                
                ButtonPivot.Instance.ForwardRotate(new Vector3(0, 270, 0), buttonRotateSpeed);
                menuList4st[i].SetActive(true);
                
                for (int q = 0; q < menuList3st.Count; q++)
                {
                    BoxCollider[] allCollider = menuList3st[q].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = false;
                    }
                }
            }
        }
    }

    public void OnBackButton(string backPath)
    {
        for (int i = 0; i < menuList1st.Count; i++)
        {
            if (menuList1st[i].name == backPath)
            {
                for (int j = 0; j < menuList2st.Count; j++)
                {
                    if (menuList2st[j].activeSelf == true)
                    {
                        menuList2st[j].SetActive(false);
                        
                        for (int k = 0; k < menuList3st.Count; k++)
                        {
                            if (menuList3st[k].activeSelf == true)
                            {
                                menuList3st[k].SetActive(false);

                                for (int m = 0; m < menuList4st.Count; m++)
                                {
                                    if (menuList4st[m].activeSelf == true)
                                    {
                                        menuList4st[m].SetActive(false);
                                    }
                                }
                            }
                        }
                    }
                }

                ButtonPivot.Instance.BackwardRotate(new Vector3(0, 0, 0), buttonRotateSpeed);
                StartCoroutine(BackMovePinchDelay());
                StartCoroutine(BackDelay(menuList1st, i));
            }
        }
        
        for (int i = 0; i < menuList2st.Count; i++)
        {
            if (menuList2st[i].name == backPath)
            {
                for (int j = 0; j < menuList3st.Count; j++)
                {
                    if (menuList3st[j].activeSelf == true)
                    {
                        menuList3st[j].SetActive(false);

                        for (int k = 0; k < menuList4st.Count; k++)
                        {
                            if (menuList4st[k].activeSelf == true)
                            {
                                menuList4st[k].SetActive(false);
                            }
                        }
                    }
                }
                
                ButtonPivot.Instance.BackwardRotate(new Vector3(0, 0, 0), buttonRotateSpeed);
                StartCoroutine(BackDelay(menuList2st, i));
                
                for (int q = 0; q < menuList1st.Count; q++)
                {
                    BoxCollider[] allCollider = menuList1st[q].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = true;
                    }
                }
            }
        }
        
        for (int i = 0; i < menuList3st.Count; i++)
        {
            if (menuList3st[i].name == backPath)
            {
                for (int j = 0; j < menuList4st.Count; j++)
                {
                    if (menuList4st[j].activeSelf == true)
                    {
                        menuList4st[j].SetActive(false);
                    }
                }
                
                ButtonPivot.Instance.BackwardRotate(new Vector3(0, 90, 0), buttonRotateSpeed);
                StartCoroutine(BackDelay(menuList3st, i));
                
                for (int q = 0; q < menuList2st.Count; q++)
                {
                    BoxCollider[] allCollider = menuList2st[q].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = true;
                    }
                }
            }
        }

        for (int i = 0; i < menuList4st.Count; i++)
        {
            if (menuList4st[i].name == backPath)
            {
                ButtonPivot.Instance.BackwardRotate(new Vector3(0, 180, 0), buttonRotateSpeed);
                StartCoroutine(BackDelay(menuList4st, i));
                
                for (int q = 0; q < menuList3st.Count; q++)
                {
                    BoxCollider[] allCollider = menuList3st[q].GetComponentsInChildren<BoxCollider>();

                    foreach (BoxCollider collider in allCollider)
                    {
                        if (collider.name == transform.name)
                        {
                            return;
                        }

                        collider.enabled = true;
                    }
                }
            }
        }
    }

    IEnumerator BackMovePinchDelay()
    {
        movePinch.GetComponent<Animator>().SetBool(Zero, true);
        yield return new WaitForSeconds(0.5f);
        movePinch.GetComponent<Animator>().SetBool(Zero, false);
        movePinch.SetActive(false);
    }
    
    IEnumerator BackDelay(List<GameObject> menuList, int index)
    {
        menuList[index].GetComponent<Animator>().SetBool(Zero, true);
        yield return new WaitForSeconds(0.5f);
        menuList[index].GetComponent<Animator>().SetBool(Zero, false);
        menuList[index].SetActive(false);
    }

    public void InstrumentNext()
    {
        GameObject item1 = GameObject.Find("Instrument Item Factory").transform.Find("Instrument Item1").gameObject;
        GameObject item2 = GameObject.Find("Instrument Item Factory").transform.Find("Instrument Item2").gameObject;

        item1.SetActive(false);
        item2.SetActive(true);
    }
    
    public void InstrumentBack()
    {
        GameObject item1 = GameObject.Find("Instrument Item Factory").transform.Find("Instrument Item1").gameObject;
        GameObject item2 = GameObject.Find("Instrument Item Factory").transform.Find("Instrument Item2").gameObject;

        item1.SetActive(true);
        item2.SetActive(false);
    }
}
