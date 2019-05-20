using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    // Revisar si existe
    private static GameObject _instance;
    /*
     * Start
     */
    void Awake()
    {
        if (!_instance)
        {
            _instance = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }
}
