using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Color_Picker : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public List<Material> materials = new List<Material>();

    public Color externalColor;
    private Color internalColor;

    // Mat 가져오는 예외처리 변수 선언
    public bool isGetMat;


    private void Start()
    {
        internalColor = externalColor;
    }

    private void Update()
    {
        if (fcp != null)
        {
            if (fcp.GetComponent<FlexibleColorPicker>()?.enabled == true)
            {
                //apply color of this script to the FCP whenever it is changed by the user
                if (internalColor != externalColor)
                {
                    fcp.color = externalColor;
                    internalColor = externalColor;
                }
                //extract color from the FCP and apply it to the object material
                for (int i = 0; i < materials.Count; i++)
                {
                    // List에 중복을 제거하는 내용
                    materials = materials.Distinct().ToList();
                    materials[i].SetColor("_EmissionColor", fcp.color * 2f);
                }
            }
            else if (fcp.GetComponent<FlexibleColorPicker>()?.enabled == false)
            {
                // 컬러UI 창이 닫히면 List의 내용을 모두 삭제한다.
                materials.RemoveAll(obj => true);
            }
        }
    }
}
