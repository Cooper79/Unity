using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Service_Manager : MonoBehaviour
{
    #region Singleton
    public static Service_Manager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public PC_Input_Controller InputController { get; private set; } 

    public bool Paused { get; private set; }

    public Action UpdateHandler = delegate { };
    public Action LateUpdateHandler = delegate { };
    public Action FixedUpdateHandler = delegate { };
    public Action DestroyHandler = delegate { };


    
    private void Start()
    {
        InputController = new PC_Input_Controller(this);
    }

    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0;
    }

    public void UnPaused()
    {
        Paused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && Paused)
        {
            UnPaused();
        }
        else if(Input.GetKeyUp(KeyCode.Space) && !Paused)
        {
            Pause();
        }

        if (Paused)
        {
            return;
        }
        
        UpdateHandler();
    }

    private void LateUpdate()
    {
        if (Paused)
        {
            return;
        }

        LateUpdateHandler();
    }

    private void FixedUpdate()
    {
        if (Paused)
        {
            return;
        }
        FixedUpdateHandler();
    }

    private void OnDestroy()
    {

    }
}
