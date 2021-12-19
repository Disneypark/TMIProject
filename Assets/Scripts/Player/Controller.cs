using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controller : MonoBehaviour
{
    [SerializeField] private static Controller instance = null;
    public static Controller Instance => instance;

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

    #region 오른쪽 왼쪽 구분 게임 오브젝트 네임을 기준으로

    public struct WhichIsHand
    {
        public const string rightHand = "ControllerRight";
        public const string leftHand = "ControllerLeft";
    }

    #endregion

    [Header ("Controller Key")]
    #region 컨트롤러 키 선언
    
    [SerializeField] private SteamVR_Action_Boolean select;
    public SteamVR_Action_Boolean Select => select;
    
    [SerializeField] private SteamVR_Action_Boolean menu;
    public SteamVR_Action_Boolean Menu => menu;

    [SerializeField] private SteamVR_Action_Boolean menu2;
    public SteamVR_Action_Boolean Menu2 => menu2;

    [SerializeField] private SteamVR_Action_Boolean grab;
    public SteamVR_Action_Boolean Grab => grab;

    [SerializeField] private SteamVR_Action_Boolean trigger;
    public SteamVR_Action_Boolean Trigger => trigger;
    
    [SerializeField] private SteamVR_Action_Boolean turnRight;
    public SteamVR_Action_Boolean TurnRight => turnRight;
    
    [SerializeField] private SteamVR_Action_Boolean turnLeft;
    public SteamVR_Action_Boolean TurnLeft => turnLeft;

    [SerializeField] private SteamVR_Action_Boolean getMetro;
    public SteamVR_Action_Boolean GetMetro => getMetro;

    [SerializeField] private SteamVR_Action_Boolean getRecorder;
    public SteamVR_Action_Boolean GetRecorder => getRecorder;
    
    #endregion

    [Space(10f)]
    [Header ("Sound")]
    #region 사운드 할당과 악기 생성시 셀렉트(텔레포트) 버튼 사용을 구분짓기 위한 불값 저장소
    
    [SerializeField] private bool isPadTouch = false;
    public bool IsPadTouch
    {
        get => isPadTouch;
        set => isPadTouch = value;
    }
    
    #endregion

    [Space (10f)]
    [Header ("Grab")]
    #region 그립 정보 저장소

    [SerializeField] private bool isRightGrab = false;
    public bool IsRightGrab
    {
        get => isRightGrab;
        set => isRightGrab = value;
    }
    
    [SerializeField] private bool isLeftGrab = false;
    public bool IsLeftGrab
    {
        get => isLeftGrab;
        set => isLeftGrab = value;
    }

    [SerializeField] private GameObject grabRightObj = null;
    public GameObject GrabRightObj
    {
        get => grabRightObj;
        set => grabRightObj = value;
    }
    
    [SerializeField] private GameObject grabLeftObj = null;
    public GameObject GrabLeftObj
    {
        get => grabLeftObj;
        set => grabLeftObj = value;
    }

    #endregion
    
    #region 그립 상태머신 저장소

    public enum GrabState
    {
        Grab,
        RightGrab,
        LeftGrab,
        Scale
    }

    [SerializeField] private GrabState grabState = GrabState.Grab;
    public GrabState GrabStates
    {
        get => grabState;
        set => grabState = value;
    }

    #endregion
    
    [Space (10f)]
    [Header ("Instrument Scale")]
    #region 인스트루먼트 스케일을 위한 불 값과 게임 오브젝트 저장소

    [SerializeField] private GameObject scaleObj;
    public GameObject ScaleObj
    {
        get => scaleObj;
        set => scaleObj = value;
    }

    [SerializeField] private bool isRightScale;
    public bool IsRightScale
    {
        get => isRightScale;
        set => isRightScale = value;
    }
    
    [SerializeField] private bool isLeftScale;
    public bool IsLeftScale
    {
        get => isLeftScale;
        set => isLeftScale = value;
    }

    #endregion
}
