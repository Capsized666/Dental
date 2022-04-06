using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCabinet : MonoBehaviour
{
    public Inputs mInputs;
    public float rotateSpeed;
    private Vector2 m_Rotation;
    // Start is called before the first frame update
    void Awake()
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
    // Update is called once per frame
    void Update()
    {
        var look = mInputs.Player.Look.ReadValue<Vector2>();
        Look(look);
    }
    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.01)
            return;
        var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
        //m_Rotation.y += rotate.x * scaledRotateSpeed;
        m_Rotation.x = Mathf.Clamp(m_Rotation.x - rotate.y * scaledRotateSpeed, -89, 89);
        transform.localEulerAngles = m_Rotation;
    }
}
