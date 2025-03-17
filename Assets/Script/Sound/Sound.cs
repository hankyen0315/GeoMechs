using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volumeScale = 1f;
}
