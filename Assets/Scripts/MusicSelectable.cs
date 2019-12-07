using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelectable : Selectable
{
    AudioClip audioClip;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();

        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    public override void SetSelectable(SongInfo song)
    {
        audioClip = song.audioClip;
        songId = song.songId;

        SetupUI($"Choice #{songId + 1}"); 
    }

    void PlaySound()
    {        
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void SetCorrectDisplay() { }

    void SetWrongDisplay() { }   
}
