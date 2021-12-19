using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkToggle : MonoBehaviour
{
    LineRenderer line;

    public GameObject destination;
    public GameObject node;

    public LinkState linkstate;

    public enum LinkState
    {
        unLinked,
        linking,
        linked,
    }

    void Start()
    {
        linkstate = LinkState.unLinked;
        line = GetComponent<LineRenderer>();
        destination.SetActive(false);
    }

    private void Update()
    {
        switch (linkstate)
        {
            case LinkState.unLinked:
                break;
            case LinkState.linking:
                OnLinking();
                break;
            case LinkState.linked:
                OnLinking();
                break;
            default:
                break;
        }
    }

    public void OnUnlinked()
    {

    }

    public void OnLinking()
    {
        
    }

    public void OnControllerHitCanvas(RaycastHit hit)
    {
        destination.transform.parent = hit.collider.gameObject.transform;
        destination.transform.localPosition = Vector3.zero;
        SetState(LinkState.linked);
    }

    public void SetState(LinkState state)
    {
        this.linkstate = state;
        switch(this.linkstate)
        {
            case LinkState.linking:
                line.enabled = true;
                destination.SetActive(true);
                GameObject controller = GameObject.FindGameObjectWithTag("Controller");
                destination.transform.parent = controller.transform;
                destination.transform.localPosition = Vector3.zero;
                break;

            case LinkState.linked:
                node.SetActive(true);
                //node.GetComponent<RectTransform>().sizeDelta(1f, y);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            SetState(LinkState.linking);
        }
    }
}
