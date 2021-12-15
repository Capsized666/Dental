using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(CharacterController))]
public sealed class Character : MonoBehaviour
{
    public static Character Instance;

    CharacterController characterController;

    CameraBeh _cambeh;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float turnSensitivity = 4;
    public Transform head;

    public bool disable = false;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 curEuler = Vector3.zero;


    void Awake()
    {
        Instance = this;
        disable = false;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _cambeh = CameraBeh.Instance;
    }

    void Update()
    {
        cursorChek();
        if (_cambeh.parent() != transform)
        {
            return;
        }
        MovementInput();
    }

    private void cursorChek()
    {
        if (_cambeh.parent() != transform)
        {
            if (Cursor.lockState != CursorLockMode.Confined)
            {
                Cursor.lockState = CursorLockMode.Confined;
                //Cursor.visible = true;
            }
        }
        else
        {
            if (Cursor.lockState!=CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;

                //Cursor.visible = true;
            }
        }


    }

    public void setActivity(bool b) {
        disable = b;

    }

    public void setHead(Transform t) {
        t.SetParent(transform);
        t.position = new Vector3(0, .75f, 0);
        t.rotation = Quaternion.identity;
    }

    void MovementInput() {

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        float XturnAmount = Input.GetAxis("Mouse Y") * Time.deltaTime * turnSensitivity;
        curEuler = Vector3.right * Mathf.Clamp(curEuler.x - XturnAmount, -90f, 90f);
        head.localRotation = Quaternion.Euler(curEuler);

        //rotate body on y-axis (Sideways)
        float YturnAmount = Input.GetAxis("Mouse X") * Time.deltaTime * turnSensitivity;
        transform.Rotate(Vector3.up * YturnAmount);

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }


}
