using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RampRay : MonoBehaviour
{
    private bool isOne = false;
    GameObject soundInfo;
    private float dist;
    private float multDist;
    [SerializeField] private Camera playerCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRay();
    }

    void UpdateRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            if (hit.transform.gameObject.CompareTag("Pad"))
            {
                if (hit.transform.gameObject.GetComponent<AudioSource>() == null)
                {
                    return;
                }

                char divide = '_';
                string[] split = hit.transform.gameObject.GetComponent<AudioSource>().clip.name.Split(divide);
                string name1 = split[split.Length - 2];
                string name2 = Parsing(split.Last(), name1);
                string fullName = name1 + "_" + name2;

                if (!isOne)
                {
                    soundInfo = Load("SoundInfo/SoundInfo");
                    isOne = true;
                }

                dist = Vector3.Distance(transform.position, hit.point);
                multDist = Mathf.Pow(dist * 2.0f, 5.0f);

                soundInfo.GetComponentInChildren<Text>().text = fullName;
                soundInfo.GetComponentInChildren<Text>().color = new Color(1.0f, 1.0f, 1.0f, ((255.0f/multDist)/255.0f)*2);
                soundInfo.GetComponentInChildren<Image>().color = new Color(0.0f,225.0f/255.0f,1.0f,((255.0f/multDist)/255.0f)*2);
                soundInfo.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.1f, hit.transform.position.z);
                soundInfo.transform.rotation = Quaternion.Euler(soundInfo.transform.eulerAngles.x, playerCam.transform.eulerAngles.y, soundInfo.transform.eulerAngles.z);
            }
            else
            {
                if (isOne)
                {
                    soundInfo.GetComponentInChildren<Image>().color = new Color(0.0f,225.0f/255.0f,1.0f,0.0f);
                    Destroy(soundInfo);
                }
                isOne = false;
            }
        }
    }

    string Parsing(string soundName, string soundCode)
    {
        if (soundCode == "C2" || soundCode == "C3" || soundCode == "C4" || soundCode == "C5" || soundCode == "C6")
        {
            if (soundName == "1" || soundName == "7")
            {
                return "도";
            }else if (soundName == "2" || soundName == "8")
            {
                return "레";
            }else if (soundName == "3" || soundName == "9")
            {
                return "미";
            }else if (soundName == "4" || soundName == "10")
            {
                return "파";
            }else if (soundName == "5" || soundName == "11")
            {
                return "솔";
            }else if (soundName == "6" || soundName == "12")
            {
                return "라";
            }
            else
            {
                return soundName;
            }
        }
        else
        {
            return soundName;
        }
        
    }

    GameObject Load(string resourcePath)
    {
        GameObject go = null;

        go = Resources.Load<GameObject>(resourcePath);

        if (!go)
        {
            Debug.Log("Resources Load Error FilePath = " + resourcePath);
            return null;
        }

        GameObject InstantiateGo = Instantiate<GameObject>(go);
        return InstantiateGo;
    }
}
