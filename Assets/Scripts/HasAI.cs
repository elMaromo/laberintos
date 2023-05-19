using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasAI : MonoBehaviour
{
    public bool hasAI;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AIManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
