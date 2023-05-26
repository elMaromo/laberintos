using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
class Music : MonoBehaviour
{
    private static Music instance = null;
    public static Music Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (Music)FindObjectOfType(typeof(Music));
            }
            return instance;
        }
    }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}