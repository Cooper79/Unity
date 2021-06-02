using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PC_Input_Controller 
{
    
    private Service_Manager _serviceManager;

    
    private Camera _cam;
    private bool _rightPointerClicked;

    public Action<Vector3, Collider> RightPointerClickHandler = delegate { };
    public Action CharacterWindowClicked = delegate { };

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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _rightPointerClicked = Input.GetButton("Fire1");
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            CharacterWindowClicked();
        }
        
    }

    private void OnLateUpdate()
    {
        
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
        
    }

    private void OnDestroy()
    {
        _serviceManager.UpdateHandler -= OnUpdate;
        _serviceManager.LateUpdateHandler -= OnLateUpdate;
        _serviceManager.FixedUpdateHandler -= OnFixedUpdate;
        _serviceManager.DestroyHandler -= OnDestroy;
    }

}
