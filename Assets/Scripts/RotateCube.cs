using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float speedRotate;

    public void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * speedRotate * Time.deltaTime);
    }

}
