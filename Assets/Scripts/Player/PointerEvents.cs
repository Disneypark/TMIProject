using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public UnityEvent OnClick = new UnityEvent(); 
    private Animator anim;
    private static readonly int Highlighted = Animator.StringToHash("Highlighted");
    private static readonly int Disabled = Animator.StringToHash("Disabled");
    private static readonly int Pressed = Animator.StringToHash("Pressed");
    private static readonly int Selected = Animator.StringToHash("Selected");

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetTrigger(Highlighted);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger(Disabled);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetTrigger(Pressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anim.SetTrigger(Selected);
        OnClick.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }
}
