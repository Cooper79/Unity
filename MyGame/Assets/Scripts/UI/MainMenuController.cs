using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : BaseGameMenuController
{
    [Header("MainMenu")]
    [SerializeField] private Button _chooseLVL;
    [SerializeField] private Button _reset;

    [SerializeField] private GameObject _lvlMenu;
    [SerializeField] private Button _closeLVLMenu;

    private int _lvl = 1;

    protected override void Start()
    {
        base.Start();
        _chooseLVL.onClick.AddListener(OnMenuLVLClicked);
        _closeLVLMenu.onClick.AddListener(OnMenuLVLClicked);
        
        if (PlayerPrefs.HasKey(GamePrefs.LastPlayedLVL.ToString()))
        {
            _play.GetComponentInChildren<TMP_Text>().text = "Resume";
            _lvl = PlayerPrefs.GetInt(GamePrefs.LastPlayedLVL.ToString());
        }
        _play.onClick.AddListener(OnPlayClicked);
        _reset.onClick.AddListener(OnResetClicked);
            
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _chooseLVL.onClick.RemoveListener(OnMenuLVLClicked);
        _closeLVLMenu.onClick.RemoveListener(OnMenuLVLClicked);
        _play.onClick.RemoveListener(OnPlayClicked);
        _reset.onClick.RemoveListener(OnResetClicked);
    }

    private void OnMenuLVLClicked()
    {
        _lvlMenu.SetActive(!_lvlMenu.activeInHierarchy);
        _audioManager.Play(UiClipNames.Levels);
        OnMenuClicked();
    }

    private void OnPlayClicked()
    {
        _serviceManager.ChangeLevel(_lvl);
        _audioManager.Play(UiClipNames.Play);
    }

    private void OnResetClicked()
    {
        _play.GetComponentInChildren<TMP_Text>().text = "Play";
        _serviceManager.ResetProgress();
        
    }
}
    
