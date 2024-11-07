using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioSource audioSource;
    
    [Tooltip("Function to be called after audio source ends")]
    public Action Callback;
    
    [Header("Audio source settings")]
    [Tooltip("Delay an audio, or play it after the provided time (in seconds)")]
    public float startDelay;
    [Tooltip("Margin by which player can be incorrect while pressing keys (in seconds)")]
    public double errorMargin;
    [Tooltip("Delay in case there are some issues with a keyboard (in milliseconds)")]
    public int inputDelay;

    [Header("Notes settings")]
    [Tooltip("Player reaction time, or how much time note is gonna be on the screen")]
    public float aliveTime;
    [Tooltip("Position where the note is gonna appear on the screen")]
    public float spawnPosition;
    [Tooltip("Position where the note is gonna be tapped by the player")]
    public float tapPosition;
    public float despawnPosition => tapPosition - (spawnPosition - tapPosition);

    private void Start()
    {
        Instance = this;
        Invoke(nameof(StartSong), startDelay);
    }

    private void StartSong()
    {
        audioSource.Play();
        StartCoroutine(WaitAudio(Callback));
    }
    
    private IEnumerator WaitAudio(Action callback)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        
        callback.Invoke();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}