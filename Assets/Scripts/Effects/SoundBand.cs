using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScrollDatas
{
    public float offsetX;
    public float speed;
}

public class SoundBand : MonoBehaviour
{
    [SerializeField] ScrollDatas scrollData;
    public ScrollDatas ScrollData => scrollData;

    [SerializeField] bool isReturn = false;
    public bool IsReturn
    {
        get => isReturn;
        set => isReturn = value;
    }

    private Material mat;

    private float value_r;
    private float value_g;
    private float value_b;

    [SerializeField] private float target_r;
    public float Target_r
    {
        get => target_r;
        set => target_r = value;
    }
    [SerializeField] private float target_g;
    public float Target_g
    {
        get => target_g;
        set => target_g = value;
    }
    [SerializeField] private float target_b;
    public float Target_b
    {
        get => target_b;
        set => target_b = value;
    }

    public enum ColorState
    {
        Ready,
        Change,
        Return
    }

    public ColorState colorState = ColorState.Ready;

    // Start is called before the first frame update
    void Start()
    {
        isReturn = true;
        mat = GetComponent<LineRenderer>().material;

        value_r = Random.value;
        value_g = Random.value;
        value_b = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScroll();

        mat.SetColor("_TintColor", new Color(value_r, value_g, value_b, 1.0f / 255.0f));

        switch (colorState)
        {
            case ColorState.Ready:
                UpdateReady();
                break;
            case ColorState.Change:
                UpdateChange();
                break;
            case ColorState.Return:
                UpdateReturn();
                break;
        }
    }

    void UpdateScroll()
    {
        scrollData.offsetX += (float) scrollData.speed * Time.deltaTime;

        if (scrollData.offsetX > 1.0f)
        {
            scrollData.offsetX = scrollData.offsetX % 1.0f;
        }
        
        Vector2 offset = new Vector2(scrollData.offsetX * -1.0f, 0);
        
        mat.SetTextureOffset("_MainTex", offset);
    }

    void UpdateReady()
    {

    }

    void UpdateChange()
    {
        if (value_r == target_r && value_g == target_g && value_b == target_b)
        {
            target_r = Random.value;
            target_g = Random.value;
            target_b = Random.value;

            colorState = ColorState.Return;
            return;
        }

        value_r = Mathf.MoveTowards(value_r, target_r, Time.deltaTime);
        value_g = Mathf.MoveTowards(value_g, target_g, Time.deltaTime);
        value_b = Mathf.MoveTowards(value_b, target_b, Time.deltaTime);
    }

    void UpdateReturn()
    {
        if (value_r == target_r && value_g == target_g && value_b == target_b)
        {
            colorState = ColorState.Ready;
            return;
        }

        value_r = Mathf.MoveTowards(value_r, target_r, Time.deltaTime);
        value_g = Mathf.MoveTowards(value_g, target_g, Time.deltaTime);
        value_b = Mathf.MoveTowards(value_b, target_b, Time.deltaTime);
    }

    public void ChangeStart()
    {
        colorState = ColorState.Change;
    }
}
