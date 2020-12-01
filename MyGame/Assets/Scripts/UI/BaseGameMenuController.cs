using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGameMenuController : MonoBehaviour
{
    protected ServiceManager _serviceManager;
    protected UIAudioManager _audioManager;

    [SerializeField] protected GameObject _menu;
    
   
    [Header("MainButtons")]
    [SerializeField] protected Button _play;
    [SerializeField] protected Button _exit;
    [SerializeField] protected Button _option;

    [Header("Settings")]
    [SerializeField] protected GameObject _settingsMenu;
    [SerializeField] protected Button _closeSettings;
    protected virtual void Start()
    {
        _serviceManager = ServiceManager.Instanse;
        _audioManager = UIAudioManager.Instance; 
        _exit.onClick.AddListener(OnQuitClicked);
        _option.onClick.AddListener(OnOptionClicked);
        _closeSettings.onClick.AddListener(OnOptionClicked);
    }

    
    protected virtual void OnDestroy()
    {
        _exit.onClick.RemoveListener(OnQuitClicked);
        _option.onClick.RemoveListener(OnOptionClicked);
        _closeSettings.onClick.RemoveListener(OnOptionClicked);
    }

    protected virtual void Update() { }
    

    protected virtual void OnMenuClicked()
    {
        _menu.SetActive(!_menu.activeInHierarchy);
    }

    private void OnQuitClicked()
    {
        _serviceManager.Exit();
        _audioManager.Play(UiClipNames.Exit);
    }

    private void OnOptionClicked()
    {
        _audioManager.Play(UiClipNames.Option);
        _settingsMenu.SetActive(!_settingsMenu.activeInHierarchy);
    }
}
