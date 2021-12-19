using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLoopPlayer : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Controller") Destroy(gameObject.GetComponentInParent<RecordPlayer>().gameObject);
    }
}
