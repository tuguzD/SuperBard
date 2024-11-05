using System.Collections;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    
    [Header("Audio source settings")]
    [Tooltip("Delay an audio, or play it after the provided time (in seconds)")]
    public float startDelay;
    [Tooltip("Margin by which player can be incorrect while pressing keys (in seconds)")]
    public double errorMargin;
    [Tooltip("Delay in case there are some issues with a keyboard (in milliseconds)")]
    public int inputDelay;

    [Header("Notes settings")]
    [Tooltip("Player reaction time, or how much time note is gonna be on the screen")]
    public float noteTime;
    [Tooltip("Position where the note is gonna appear on the screen")]
    public float noteSpawnY;
    [Tooltip("Position where the note is gonna be tapped by the player")]
    public float noteTapY;
    public float noteDespawnY => noteTapY - (noteSpawnY - noteTapY);

    private void Start()
    {
        Instance = this;
        Invoke(nameof(StartSong), startDelay);
    }

    private void StartSong()
    {
        audioSource.Play();
        StartCoroutine(WaitAudio());
    }
    
    private IEnumerator WaitAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        Application.Quit();
    }

    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }
}