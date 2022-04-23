using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Inputs mInputs;
 
    private Vector2 m_Rotation;
    
    public float moveSpeed;
    public float rotateSpeed;
    public float burstSpeed;
    
    

    public void Awake()
    {
        mInputs = new Inputs();
    }
    private void OnEnable()
    {
        mInputs.Enable();
    }
    private void OnDisable()
    {
        mInputs.Disable();
    }

    public void Update()
    {  
        var move = mInputs.Player.Moving.ReadValue<Vector2>();
        var look = mInputs.Player.Look.ReadValue<Vector2>();

        if (ScenaManager.Instance.currentState == gamestate.moving)
        {

        Move(move);
        Look(look);

        }

    }

    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
        m_Rotation.y += rotate.x * scaledRotateSpeed;
        //m_Rotation.x = Mathf.Clamp(m_Rotation.x - rotate.y * scaledRotateSpeed, -89, 89);
        transform.localEulerAngles = m_Rotation;
    }

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
            return;
        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        // For simplicity's sake, we just keep movement in a single plane here. Rotate
        // direction according to world Y rotation of player.
        var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        //m_Rotation.y += rotate.x * rotateSpeed;
        //print(rotate);
        transform.localEulerAngles = m_Rotation;
        transform.position += move * scaledMoveSpeed;
    }


}
