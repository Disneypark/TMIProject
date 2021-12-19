using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GetMat : MonoBehaviour
{
    Color_Picker change;

    public int i;

    private void Awake()
    {
        change = GetComponent<Color_Picker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Touch Object의 Mat를 List로 추가하고 이미 List에 추가된 상태라면 삭제한다.
        if (other.gameObject.tag == "Pad" && change.fcp != null)
        {
            if (change.materials.Contains(other.gameObject.GetComponent<Renderer>().material) && change.fcp.gameObject.activeSelf==true)
            {
                // List에 이미 가지고있는 원소라면 삭제를 한다.
                i = change.materials.IndexOf(other.gameObject.GetComponent<Renderer>().material);
                change.materials.RemoveAt(i);
                other.GetComponent<Outline>().enabled = false;
            }
            else if (change.fcp.gameObject.activeSelf == true)
            {
                change.materials.Add(other.gameObject.GetComponent<Renderer>().material);
                other.GetComponent<Outline>().enabled = true;
            }
        }
    }
}
