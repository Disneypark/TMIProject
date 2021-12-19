using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
        // 민찬
        //DataManager.Instance.OnRecordSave(padEvent.sound.name, padEvent.timeoffset, padEvent.length, padEvent.startTime);
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