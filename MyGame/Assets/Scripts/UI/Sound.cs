using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public UiClipNames ClipName;
    public AudioClip AudioClip;

    [Range(0, 11)]
    public float Volume;
    [Range(-3, 3)]
    public float Pitch;

    public bool Loop;

    [HideInInspector]
    public AudioSource AudioSource;
}

