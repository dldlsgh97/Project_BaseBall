using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public UIManager ui;
    
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
   
}
