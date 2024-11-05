using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lane : MonoBehaviour
{
    [Tooltip("Location of the MIDI file in the 'StreamingAssets' folder")]
    public string fileLocation;
    private MidiFile _midiFile;
    
    [Tooltip("Input action to register by lane")]
    public InputAction input;
    [Tooltip("Note object that will be spawned in this lane")]
    public GameObject notePrefab;
    
    [Tooltip("List of already spawned notes")]
    private readonly List<Note> _notes = new();

    [Tooltip("List of moments when notes will be tapped")]
    public List<double> timeStamps = new();
    private int _spawnIndex;
    private int _inputIndex;

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        _midiFile = MidiFile.Read($"{Application.streamingAssetsPath}/{fileLocation}");
        
        var notes = _midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);
        
        SetTimeStamps(array);
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] notes)
    {
        foreach (var note in notes)
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(
                note.Time, _midiFile.GetTempoMap());

            timeStamps.Add(
                (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds +
                (double)metricTimeSpan.Milliseconds / 1000f);
        }
    }

    private void Update()
    {
        double timeStamp;

        if (_spawnIndex < timeStamps.Count)
        {
            timeStamp = timeStamps[_spawnIndex];
            if (SongManager.GetAudioSourceTime() >= timeStamp - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefab, transform);
                _notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignTime = (float)timeStamp;
                _spawnIndex++;
            }
        }

        if (_inputIndex < timeStamps.Count)
        {
            timeStamp = timeStamps[_inputIndex];
            var errorMargin = SongManager.Instance.errorMargin;
            var audioTime =
                SongManager.GetAudioSourceTime() - (SongManager.Instance.inputDelay / 1000.0);

            if (input.triggered)
            {
                if (Math.Abs(audioTime - timeStamp) < errorMargin)
                {
                    ScoreManager.Hit();
                    print($"Hit on {_inputIndex} note");
                    Destroy(_notes[_inputIndex].gameObject);
                    _inputIndex++;
                }
                else
                {
                    ScoreManager.Punish();
                    print($"Hit inaccurate on {_inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
                }
            }

            if (timeStamp + errorMargin <= audioTime)
            {
                ScoreManager.Miss();
                print($"Missed {_inputIndex} note");
                _inputIndex++;
            }
        }
    }
}