# TMIProject



![initial](https://user-images.githubusercontent.com/78453968/120595481-6a66a500-c47d-11eb-93df-322a04859899.PNG)

[시연영상 전체기능](https://www.youtube.com/watch?v=4c7q10VifzI)

안녕하세요 박종현 입니다.

TMI프로젝트 TRACK MAKER INTERFACE의 약자로

'악기를 연주하고 녹음을 하는 방식으로 소리가 쌓이는 과정을 직접 경험하는'

악기 연주 콘텐츠를 만든 프로젝트입니다.


> 사운드 매니저 (동적 할당)
<details markdown="1">
<summary>코드보기</summary>

```c#
======== 사운드 팩토리 ========

public class SoundFactory : MonoBehaviour
{
    Dictionary<string, GameObject> soundFileCache = new Dictionary<string, GameObject>();

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

======== 사운드 매니저 ========

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundFactory soundFactory;

    [SerializeField] Dictionary<string, Queue<GameObject>> sounds = new Dictionary<string, Queue<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SoundSystem.Instance.SoundMenu.SoundMenuData.Length; i++)
        {
            string instrumentName = SoundSystem.Instance.SoundMenu.SoundMenuData[i].instrumentName;
            int soundCacheCount = SoundSystem.Instance.SoundMenu.SoundMenuData[i].soundCacheCount;
            List<string> instrumentSoundPath = SoundSystem.Instance.SoundMenu.SoundMenuData[i].instrumentSoundPath;
            Transform parent = SoundSystem.Instance.SoundMenu.SoundMenuData[i].instrument;
            
            GenerateSound(instrumentName, soundCacheCount, instrumentSoundPath, parent);
        }
    }

    public bool GenerateSound(string instrumentName, int soundCacheCount, List<string> instrumentSoundPath, Transform parent)
    {
        if (sounds.ContainsKey(instrumentName))
        {
            Debug.LogWarning("Already Sound Generated Instrument = " + instrumentName);
            return false;
        }
        else
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            List<string> soundPath = new List<string>();
            
            for (int i = 0; i < soundCacheCount; i++)
            {
                GameObject go = soundFactory.Load(SoundSystem.Instance.SoundMenu.SoundMenuPrefabPath);
            
                if (!go)
                {
                    Debug.LogError("SoundFactory GenerateSound Load Error!");
                    return false;
                }
            
                go.transform.SetParent(parent);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0);
                go.transform.localRotation = Quaternion.Euler(Vector3.zero);

                if (instrumentSoundPath[i] != String.Empty)
                {
                    AudioSource audio = go.AddComponent<AudioSource>();
                    audio.playOnAwake = false;
                    audio.clip = Resources.Load(instrumentSoundPath[i]) as AudioClip;
                }
                else
                {
                    go.AddComponent<AudioSource>();
                }

                queue.Enqueue(go);
            }
            
            sounds.Add(instrumentName, queue);
        }
        
        return true;
    }
}

======== 사운드 메뉴 ========

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

======== 사운드 ========

public class Sound : MonoBehaviour
{
    private bool isPlay = false;

    public void Play(GameObject sound)
    {
        sound.GetComponent<AudioSource>().Play();
    }

    public void InputSound(GameObject sound, GameObject player)
    {
        player.GetComponent<ControllerSound>().RightBall.GetComponent<AudioSource>().clip = sound.GetComponent<AudioSource>().clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (!isPlay)
            {
                isPlay = true;
                GetComponentInChildren<Transform>().Find("Marker").GetComponent<Image>().color = new Color(1,1,1,1);
                Play(this.gameObject);
            }

            StartCoroutine(PlayDelay());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (Controller.Instance.Menu2.GetState(SteamVR_Input_Sources.RightHand))
            {
                InputSound(this.gameObject, other.gameObject);
                other.gameObject.GetComponent<ControllerSound>().RightBall.GetComponent<PlayerBall>().ColorChange(true);
                other.gameObject.GetComponent<ControllerSound>().SoundMarker.transform.Find("SoundMarker").gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInChildren<Transform>().Find("Marker").GetComponent<Image>().color = new Color(1,1,1,25.0f / 255.0f);
    }

    IEnumerator PlayDelay()
    {
        yield return new WaitForSeconds(1f);
        isPlay = false;
    }
}

```

</details>

> 사운드 옥타브 및 타입 변경
<details markdown="1">
<summary>코드보기</summary>

```c#

// 옥타브를 바꾸고 싶은 오디오소스가 달린 오브젝트
    [SerializeField] private GameObject[] sounds;

    private const int plus = 12;
    private const int minus = -12;

    private string[] split;
    private int changeIndex;
    private string path;

    private int instCount;

    #endregion

    void Start()
    {

        if (GetComponentInParent<Instrument>().padList == null)
        {
            return;
        }

        instCount = GetComponentInParent<Instrument>().padList.Length;
        sounds = new GameObject[instCount];

        for (int i = 0; i < instCount; i++)
        {
            sounds[i] = GetComponentInParent<Instrument>().padList[i].gameObject;
        }
    }

    void Update()
    {

    }

    public void OnPlusOctave()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].GetComponent<AudioSource>().clip = OctaveChange(PathFinder(sounds[i].GetComponent<AudioSource>().clip.name, plus));
            sounds[i].GetComponent<InstrumentPad>().sound = sounds[i].GetComponent<AudioSource>().clip;
        }
    }

    public void OnMinusOctave()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].GetComponent<AudioSource>().clip = OctaveChange(PathFinder(sounds[i].GetComponent<AudioSource>().clip.name, minus));
            sounds[i].GetComponent<InstrumentPad>().sound = sounds[i].GetComponent<AudioSource>().clip;
        }
    }
	
private string PathFinder(string soundName, int changeNum)
    {
        char slash = '/';

        foreach (SoundMenuData soundMenuData in SoundSystem.Instance.SoundMenu.SoundMenuData)
        {
            for (int i = 0; i < soundMenuData.instrumentSoundPath.Count; i++)
            {
                split = soundMenuData.instrumentSoundPath[i].Split(slash);

                if (split.Last() == soundName)
                {
                    changeIndex = OctaveMath(i, changeNum, soundMenuData.instrumentSoundPath.Count);
                    path = soundMenuData.instrumentSoundPath[changeIndex];
                }
            }
        }
        return path;
    }

    private int OctaveMath(int index, int changeNum, int count)
    {
        int reIndex = index + changeNum;

        if (reIndex < 0 || reIndex >= count)
        {
            Debug.Log("Octave is too lower or higher");
            return index;
        }

        return reIndex;
    }

    private AudioClip OctaveChange(string resourcePath)
    {
        AudioClip audioClip = null;

        audioClip = Resources.Load<AudioClip>(resourcePath);
        if (!audioClip)
        {
            Debug.LogError("Resources Load Error! FilePath = " + resourcePath);
            return null;
        }

        return audioClip;
    }
```

</details>

> 레코드 기능

<details markdown="1">
<summary>코드보기</summary>

```c#
public class TriggerEnterEvent
{
    public AudioClip sound;
    public float timeOffset;
    public float length;
    public bool isPlaying;
    private float startTime;
    public int padIndex;

    public TriggerEnterEvent(AudioClip sound, float recordStartTime, int padIndex)
    {
        this.timeOffset = Time.time - recordStartTime;
        this.sound = sound;
        startTime = Time.time;
        this.length = 0;
        this.padIndex = padIndex;
    }

    public void OnTriggerExit()
    {
        this.length = Time.time - startTime;
    }

    public bool isNowPlaying(float t)
    {
        return t >= timeOffset && t <= timeOffset + length;
    }
}

public struct Loop
{
    public float recordLength;
    public List<TriggerEnterEvent> record;

    public Loop(List<TriggerEnterEvent> record, float length)
    {
        this.recordLength = length;
        this.record = record;
    }
}

public class Record : MonoBehaviour
{
    private List<TriggerEnterEvent> record;

    [Tooltip("녹음버튼 누른시간")]
    public float recordStartTime;
    public bool isRecording;
    public bool isCounting;

    // Recorder count setting
    public Image recorderPlayImg;
    public Text recorderCountText;

    [Tooltip("녹음 진행시간")]
    public float recordingTime;
    public float padCount;

    public static Record Instance;
    public Metronome metronome;
    public Loop recordLoop;

    public RawImage recordBg;
    public Image trackGroup;
    public RectTransform recordBgTransform;

    public GameObject nodeUIprefab;
    public GameObject loopGroup;
    public GameObject deleteGroup;

    const float TRACKGROUP_HEIGHT = 1.2f;
    const float TRACKGROUP_WIDTH = 4f;
    const float SECONDS = 60f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {


        if (isRecording)
        {
            recordingTime += Time.deltaTime;

            // Track Canvas 생성
            recordBgTransform.sizeDelta = new Vector2((recordingTime * TRACKGROUP_WIDTH / SECONDS), TRACKGROUP_HEIGHT);
            recordBg.uvRect = new Rect(0, 0, metronome.tempo, padCount);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Controller")
        {
            if (isRecording && !isCounting) Recording();
            else StartCountDown();
        }
    }

    [ContextMenu("StartCountDown")]
    public void StartCountDown()
    {
        recorderPlayImg.enabled = false;
        isCounting = true;
    }

    public void Recording()
    {
        isCounting = false;
        if (!isRecording)
        {
            recorderPlayImg.enabled = true;
            prevRowNote = -1;
            prevTopNote = -1;
            recordBgTransform.sizeDelta = new Vector2(0, TRACKGROUP_HEIGHT);
            recordingTime = 0;
            record = new List<TriggerEnterEvent>();
            isRecording = true;
            recordStartTime = Time.time;
        }
        else
        {
            isRecording = false;
            recordLoop = new Loop(record, Time.time - recordStartTime);
            metronome.recorder_BeatCount.fillAmount = 0;

            // 레코드 종료되면 Loop 그룹 생성
            GameObject loop = Instantiate(loopGroup);
            loop.transform.position = transform.position;
            loop.transform.rotation = transform.rotation;
            loop.GetComponent<RecordPlayer>().recordLoop = recordLoop;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.165f, transform.position.z);

            // Track을 Loop그룹으로 전달
            Image cloneTrack = Instantiate(trackGroup.gameObject, trackGroup.transform.parent).GetComponent<Image>();
            cloneTrack.transform.parent = loop.transform.GetChild(0).transform;
            cloneTrack.rectTransform.localPosition = new Vector2(80, 0);
            cloneTrack.rectTransform.localRotation = Quaternion.identity;
            trackGroup.rectTransform.sizeDelta = new Vector2(0, TRACKGROUP_HEIGHT);

            // Loop그룹으로 전달된 Track 우측에 Delete Object 생성
            GameObject deleteObj = Instantiate(deleteGroup);
            deleteObj.transform.parent = cloneTrack.transform;
            deleteObj.transform.localRotation = Quaternion.identity;
            deleteObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(1.5f, 0);

            // delete z포지션 리셋
            Vector3 deletePosition = deleteObj.transform.localPosition;
            deletePosition.z = 0;
            deleteObj.transform.localPosition = deletePosition;

            // 레코드 Track node 초기화 부분
            Transform trackTile = trackGroup.transform.GetChild(0);
            for (int i = 1; i < trackTile.childCount; i++)
            {
                Destroy(trackTile.GetChild(i).gameObject);
            }
        }
    }

    int prevRowNote;
    int prevTopNote;

    public void AddPressButtonEvent(TriggerEnterEvent padEvent, Color padColor)
    {
        if (!isRecording)
        {
            return;
        }
        record.Add(padEvent);

        int lowestNote = record.Min(note => note.padIndex);
        int highestNote = record.Max(note => note.padIndex);
        int rowCount = Mathf.Max(highestNote - lowestNote + 1, 5);

        // Track node 출력 부분
        GameObject temp = Instantiate(nodeUIprefab, recordBg.rectTransform);
        float noteHeight = TRACKGROUP_HEIGHT / rowCount;
        temp.GetComponent<RectTransform>().sizeDelta = new Vector2((padEvent.length / SECONDS * TRACKGROUP_WIDTH) * 2, noteHeight);
        temp.GetComponent<RectTransform>().anchoredPosition = new Vector2((padEvent.timeOffset * TRACKGROUP_WIDTH / SECONDS), -(noteHeight * (highestNote - padEvent.padIndex)));
        
        //recordBg.uvRect = new Rect(0, 0, 50, noteHeight);
        
        temp.gameObject.name = padEvent.padIndex.ToString();
        temp.SetActive(true);
        temp.GetComponent<Image>().color = padColor;
        if (prevRowNote != lowestNote) SortingHeight(highestNote, noteHeight);
        if (prevTopNote != highestNote) SortingHeight(highestNote, noteHeight);
        prevTopNote = highestNote;
        prevRowNote = lowestNote;
    }

    public void SortingHeight(int highestNote, float noteHeight)
    {
        Vector2 temp;
        recordBg.GetComponentsInChildren<Image>()
            .Where(x => x.tag == "Node")
            .ToList()
            .ForEach(note =>
            {
                temp = note.rectTransform.sizeDelta;
                temp.y = noteHeight;
                note.rectTransform.sizeDelta = temp;

                temp = note.rectTransform.anchoredPosition;
                temp.y = -(noteHeight * (highestNote - int.Parse(note.gameObject.name)));
                note.rectTransform.anchoredPosition = temp;
            });
    }

    [ContextMenu("PrintLoopInfo")]
    public void PrintLoopInfo()
    {
        print($"length : {recordLoop.recordLength}");
        recordLoop.record.ForEach(note => print($"note info: [{note.sound.name}] : [{note.length}]"));
    }
}
```

</details>
    
> 컨트롤러의 그랩 및 스케일 변경

<details markdown="1">
<summary>코드보기</summary>

```c#
// Update is called once per frame
    void Update()
    {
        switch (Controller.Instance.GrabStates)
        {
            case Controller.GrabState.Grab:
            case Controller.GrabState.RightGrab:
            case Controller.GrabState.LeftGrab:
                UpdateGrab();
                break;
            case Controller.GrabState.Scale:
                UpdateScale();
                break;
        }
    }

    void UpdateGrab()
    {
        if (this.gameObject.name == Controller.WhichIsHand.rightHand)
        {
            TransformChange(SteamVR_Input_Sources.RightHand, Controller.Instance.IsRightGrab, Controller.Instance.GrabRightObj);
        }
        
        if (this.gameObject.name == Controller.WhichIsHand.leftHand)
        {
            TransformChange(SteamVR_Input_Sources.LeftHand, Controller.Instance.IsLeftGrab, Controller.Instance.GrabLeftObj);
        }
    }

    void TransformChange(SteamVR_Input_Sources hand, bool isGrab, GameObject grabObj)
    {
        if (Controller.Instance.Grab.GetState(hand) && isGrab)
        {
            if (grabObj == null)
            {
                Debug.LogError("Grab Object is null");
                return;
            }

            if (gameObject.transform.childCount > 0)
            {
                return;
            }

            grabObj.transform.parent = gameObject.transform;
        }
        else if (Controller.Instance.Grab.GetStateUp(hand) && isGrab)
        {
            if (gameObject.transform.childCount != 0)
            {
                if (GetComponentInChildren<Grab>().transform.parent.gameObject.CompareTag("Sample"))
                {
                    grabObj.transform.parent = sampleParent;
                }
                else if (GetComponentInChildren<Grab>().transform.parent.gameObject.CompareTag("Instrument"))
                {
                    grabObj.transform.parent = instrumentParent;
                }
                else if (GetComponentInChildren<Grab>().transform.parent.gameObject.CompareTag("Menu"))
                {
                    grabObj.transform.parent = menuParent;
                }
                else if (GetComponentInChildren<Grab>().transform.parent.gameObject.CompareTag("RampHead"))
                {
                    grabObj.transform.parent = rampParent;
                }
                else
                {
                    grabObj.transform.parent = null;
                }
            }

            if (this.gameObject.name == Controller.WhichIsHand.rightHand)
            {
                Controller.Instance.GrabStates = Controller.GrabState.Grab;
                Controller.Instance.IsRightGrab = false;
                Controller.Instance.GrabRightObj = null;
            }
            else if (this.gameObject.name == Controller.WhichIsHand.leftHand)
            {
                Controller.Instance.GrabStates = Controller.GrabState.Grab;
                Controller.Instance.IsLeftGrab = false;
                Controller.Instance.GrabLeftObj = null;
            }
        }
    }
    
    void UpdateScale()
    {
        ScaleChange(SteamVR_Input_Sources.RightHand, SteamVR_Input_Sources.LeftHand, Controller.Instance.IsRightScale, Controller.Instance.IsLeftScale, Controller.Instance.ScaleObj);
    }

    void ScaleChange(SteamVR_Input_Sources hand1, SteamVR_Input_Sources hand2, bool isRightScale, bool isLeftScale, GameObject scaleObj)
    {
        if (Controller.Instance.Grab.GetState(hand1) && Controller.Instance.Grab.GetState(hand2) && isRightScale && isLeftScale)
        {
            if (scaleObj == null)
            {
                Debug.LogError("Scale Obj is null");
                return;
            }
            
            if (gameObject.transform.childCount > 0)
            {
                return;
            }

            if (gameObject.transform.childCount != 0)
            {
                scaleObj.transform.parent = instrumentParent;
            }

            float ballDist = Vector3.Distance(rightBall.transform.position, leftBall.transform.position);
            float clampBallDist = Mathf.Clamp(ballDist, 0.5f, 1.0f);

            if (scaleObj.transform.localScale.x >= 0.5f && scaleObj.transform.localScale.x <= 1.0f)
            {
                scaleObj.transform.localScale = new Vector3(clampBallDist, clampBallDist, clampBallDist);
            }
            else if (scaleObj.transform.localScale.x < 0.5f)
            {
                scaleObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (scaleObj.transform.localScale.x > 1.0f)
            {
                scaleObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        else if (Controller.Instance.Grab.GetStateUp(hand1) || Controller.Instance.Grab.GetStateUp(hand2))
        {
            Controller.Instance.GrabStates = Controller.GrabState.Grab;
            Controller.Instance.IsRightScale = false;
            Controller.Instance.IsLeftScale = false;
            Controller.Instance.ScaleObj = null;
        }
    }
}
```

</details>

> 배경 효과
<details markdown="1">
<summary>코드보기</summary>

```c#
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
```

</details>
