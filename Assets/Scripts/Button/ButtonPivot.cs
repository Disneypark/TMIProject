using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPivot : MonoBehaviour
{
    [SerializeField] private static ButtonPivot instance = null;
    public static ButtonPivot Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("singletone error");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public enum State
    {
        None,
        Ready,
        Forward,
        Backward
    }

    public State currentState = State.None;
    public Transform menuPos;
    public Transform playerCam;
    public Transform camRig;

    private Vector3 targetRotate;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        targetRotate = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.None:
                break;
            case State.Ready:
                break;
            case State.Forward:
            case State.Backward:
                UpdateRotate();
                break;
        }
    }

    void UpdateRotate()
    {
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, targetRotate, Time.deltaTime * speed);
    }

    public void ForwardRotate(Vector3 rotate, float currentSpeed)
    {
        targetRotate = rotate;
        speed = currentSpeed;
        currentState = State.Forward;
    }

    public void BackwardRotate(Vector3 rotate, float currentSpeed)
    {
        targetRotate = rotate;
        speed = currentSpeed;
        currentState = State.Backward;
    }

    public void EnablePosRot()
    {
        transform.parent.gameObject.transform.position = menuPos.transform.position;
        transform.parent.gameObject.transform.rotation = Quaternion.Euler(transform.parent.gameObject.transform.rotation.x, playerCam.rotation.y + camRig.transform.localEulerAngles.y, transform.parent.gameObject.transform.rotation.z);
        transform.parent.transform.forward = playerCam.forward;
    }
}
