using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivate : MonoBehaviour
{
    private Animator anim;
    private static readonly int AnimOn = Animator.StringToHash("AnimOn");
    private int button;
    private bool isButton;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool(AnimOn, false);
        button = 1 << LayerMask.NameToLayer("Button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            if (!isButton)
            {
                StartCoroutine(TouchDelay());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Controller"))
        {
            anim.SetBool(AnimOn, false);
            isButton = false;
        }
    }

    IEnumerator TouchDelay()
    {
        anim.SetBool(AnimOn, true);
        GetComponent<Button>().onClick.Invoke();
        GameObject menu = GameObject.Find("Menu");
        isButton = true;
        
        Collider[] cols = Physics.OverlapSphere(transform.position, menu.transform.localScale.x*10, button);
        if (cols != null && cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].enabled = false;
            }
            
            yield return new WaitForSeconds(0.1f);
    
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i].enabled = true;
                cols[i].GetComponent<Animator>().SetBool(AnimOn, false);
            }
        }
    }
    
}
