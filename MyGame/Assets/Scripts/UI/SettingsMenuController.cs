using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{

    [SerializeField] private Slider _volume;
    [SerializeField] private Toggle _fullscreen;

    [SerializeField] private AudioMixer _masterMixer;
    // Start is called before the first frame update
    void Start()
    {
        _volume.onValueChanged.AddListener(OnVolumeChange);
        _fullscreen.onValueChanged.AddListener(OnFullScreenChanged);
    }

    private void OnDestroy()
    {
        _volume.onValueChanged.RemoveListener(OnVolumeChange);
        _fullscreen.onValueChanged.RemoveListener(OnFullScreenChanged);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnVolumeChange(float volume)
    {
        _masterMixer.SetFloat("Volume", volume);
    }

    private void OnFullScreenChanged(bool value)
    {
        Screen.fullScreen = value;
    }
}
