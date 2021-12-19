using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : MonoBehaviour
{

    private void Update()
    {

    }

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    public void OnLinkToggle()
    {
    }

    public void OnMuteToggle()
    {

    }

    public void OnRowButton()
    {
    }

    public void OnDeleteButton()
    {
        Destroy(gameObject.transform.parent.transform.parent.gameObject);
    }
}
