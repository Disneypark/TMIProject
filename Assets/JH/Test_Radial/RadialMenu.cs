using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RadialMenu : MonoBehaviour
{
    public bool isOnFirstGroup;
    private bool isOnSecondGroup;
    private bool isOnType;
    private bool isOnColor;
    private bool isOnOctave;
    private bool isOnTypeUp = true; // 시작 상태는 Up == true 

    public Toggle TypeUp;
    public Toggle TypeDown;

    public Animator radialAnim;
    public GameObject radialMenu;

    Color_Picker colorPicker;
    private void Awake()
    {
        if (TypeUp != null)
        {
            TypeUp.isOn = isOnTypeUp;
            TypeUp.isOn = !TypeDown.isOn;
        }

        colorPicker = GameObject.Find("ControllerGrp").GetComponentInChildren<Color_Picker>(true);
    }
    private void Update()
    {

    }

    public void OnEnableOctave()
    {
        if (!isOnOctave)
        {
            isOnOctave = true;
            isOnSecondGroup = true;
            radialAnim.Play("Octave_On");
        }
    }

    public void OnDisableOctave()
    {
        if (isOnOctave)
        {
            isOnOctave = false;
            isOnSecondGroup = false;
            radialAnim.Play("Octave_Off");
        }
    }

    public void OnEnableType()
    {
        if (!isOnType)
        {
            isOnType = true;
            isOnSecondGroup = true;
            if (isOnTypeUp) radialAnim.Play("Type_Enabled_Up");
            if (!isOnTypeUp) radialAnim.Play("Type_Enabled_Down");
        }
    }

    public void OnDisableType()
    {
        if (isOnType)
        {
            isOnType = false;
            isOnSecondGroup = false;
            radialAnim.Play("Type_Disabled");
        }
    }

    public void OnEnableColor()
    {
        if (!isOnColor)
        {
            isOnColor = true;
            isOnSecondGroup = true;
            radialAnim.Play("Color_On");
            colorPicker.fcp = gameObject.GetComponentInChildren<FlexibleColorPicker>(true);
        }
    }

    public void OnDisableColor()
    {
        if (isOnColor)
        {
            isOnColor = false;
            isOnSecondGroup = false;
            radialAnim.Play("Color_Off");
            foreach (var outline in gameObject.transform.parent.parent.GetComponentsInChildren<Outline>()) outline.enabled = false;
            colorPicker.materials.RemoveAll(material => true);
        }
    }

    public void OnEnabledMenu()
    {
        if (!isOnFirstGroup && !isOnSecondGroup)
        {
            isOnFirstGroup = true;
            radialAnim.Play("Radial_On");
        }
    }

    public void OnDisableMenu()
    {
        if (isOnFirstGroup && !isOnSecondGroup)
        {
            radialAnim.Play("Radial_Off");
            isOnFirstGroup = false;
        }
    }

    public void OnDeleteButton()
    {
        Destroy(gameObject.GetComponentInParent<Instrument>().gameObject);
    }

    public void OnTypeUp()
    {
        if (!isOnTypeUp && isOnType)
        {
            isOnTypeUp = true;
            radialAnim.Play("Type_Up");
        }
    }

    public void OnTypeDown()
    {
        if (isOnTypeUp && isOnType)
        {
            isOnTypeUp = false;
            radialAnim.Play("Type_Down");
        }
    }

    public void OnBackButton()
    {
        if (isOnType) OnDisableType();
        if (isOnColor) OnDisableColor();
        if (isOnOctave) OnDisableOctave();
    }
}
