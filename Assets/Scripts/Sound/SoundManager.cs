using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefesSO audioClipRefesSO;
    [SerializeField] private float volume = 3f;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    
    
    public void PlaySound(AudioClip[] audioClipArray, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public AudioClipRefesSO GetAudioClipRefesSO()
    {
        return audioClipRefesSO;
    }
}
