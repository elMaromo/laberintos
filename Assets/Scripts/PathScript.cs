using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public bool activated;

    private void Awake()
    {
        activated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        activated = true;
        gameObject.SetActive(false);
    }
}
