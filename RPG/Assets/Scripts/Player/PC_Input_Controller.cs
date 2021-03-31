using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PC_Input_Controller 
{
    
    private Service_Manager _serviceManager;

    
    private Camera _cam;
    private bool _rightPointerClicked;

    public Action<Vector3, Collider> RightPointerClickHandler = delegate { };

    // Start is called before the first frame update
    public PC_Input_Controller(Service_Manager service_Manager)
    {
        _serviceManager = service_Manager;
        _cam = Camera.main;
        Time.timeScale = 1;
        _serviceManager = Service_Manager.instance;
        _serviceManager.UpdateHandler += OnUpdate;
        _serviceManager.LateUpdateHandler += OnLateUpdate;
        _serviceManager.FixedUpdateHandler += OnFixedUpdate;
        _serviceManager.DestroyHandler += OnDestroy;
    }

    // Update is called once per frame
    private void OnUpdate()
    {
        _rightPointerClicked = Input.GetButton("Fire2");
        Debug.Log("Update");
    }

    private void OnLateUpdate()
    {
        Debug.Log("LateUpdate");
    }

    private void OnFixedUpdate()
    {
        if (_rightPointerClicked)
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hitinfo, 100))
            {
                RightPointerClickHandler(hitinfo.point, hitinfo.collider);
            }
        }
        Debug.Log("FixedUpdate");
    }

    private void OnDestroy()
    {
        _serviceManager.UpdateHandler -= OnUpdate;
        _serviceManager.LateUpdateHandler -= OnLateUpdate;
        _serviceManager.FixedUpdateHandler -= OnFixedUpdate;
        _serviceManager.DestroyHandler -= OnDestroy;
    }

}
