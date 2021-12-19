using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAix;
    float vAix;

    Vector3 moveVec;
    void Start()
    {

    }

    void Update()
    {
        GetInput();
        Move();
    }

    void GetInput()
    {
        hAix = Input.GetAxis("Horizontal");
        vAix = Input.GetAxis("Vertical");
    }

    void Move()
    {
        moveVec = new Vector3(hAix, 0, vAix).normalized;

        transform.position += moveVec * 1 * Time.deltaTime;
    }
}
