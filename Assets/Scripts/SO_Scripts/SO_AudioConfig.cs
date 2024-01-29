using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Config", menuName = "Custom Audio Configuration")]
public class SO_AudioConfig : ScriptableObject
{
    [Header("Sound Resource Path")]
    public string resourcePath;
    [Header("Volume")]
    [Range(0f, 1f)] public float volume;
}
