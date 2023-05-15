using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLab : MonoBehaviour
{
    public float speedRotation;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if( Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(new Vector3(0, 0, 1) * speedRotation * Time.deltaTime * -1);
            //transform.Rotate(new Vector3(0, 0, 1) * speedRotation * Time.deltaTime * -1, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(new Vector3(0, 0, 1) * speedRotation * Time.deltaTime);
            //transform.Rotate(new Vector3(0, 0, 1) * speedRotation * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(new Vector3(1, 0, 0) * speedRotation * Time.deltaTime);
            //transform.Rotate(new Vector3(1, 0, 0) * speedRotation * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(new Vector3(1, 0, 0) * speedRotation * Time.deltaTime * -1);
            //transform.Rotate(new Vector3(1, 0, 0) * speedRotation * Time.deltaTime * -1, Space.World);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(new Vector3(0, 1, 0) * speedRotation * Time.deltaTime);
            //transform.Rotate(new Vector3(0, 1, 0) * speedRotation * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(new Vector3(0, 1, 0) * speedRotation * Time.deltaTime * -1);
            //transform.Rotate(new Vector3(0, 1, 0) * speedRotation * Time.deltaTime * -1, Space.World);
        }
    }
}
