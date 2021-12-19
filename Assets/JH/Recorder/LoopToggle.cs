using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoopToggle : MonoBehaviour
{
    Toggle toggle;
    public LoopGroupButton isLoop;
    public RecordPlayer rp;
    public bool toggleCheck;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            toggle.isOn = !toggle.isOn;
            toggleCheck = !toggleCheck;
            isLoop.isLoop = toggleCheck;
            if (rp.isPlaying)
            {
                rp.isPlaying = false;
                rp.playImg.enabled = true;
                rp.stopImg.enabled = false;
                rp.playText.enabled = true;
                rp.stopText.enabled = false;
            }
        }
    }
}
