using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaneManager : MonoBehaviour
{
    [Tooltip("Location of the MIDI file in the 'StreamingAssets' folder")]
    public string fileLocation;
    private MidiFile _midiFile;
    
    [Header("User interaction")]
    [Tooltip("Note object that will be spawned in this lane")]
    public GameObject notePrefab;
    [Tooltip("Input action to register by lane")]
    public InputAction input;
    
    [Tooltip("List of already spawned notes")]
    private readonly List<NoteManager> _notes = new();
    [Tooltip("List of moments when notes will be tapped")]
    private readonly List<double> _timeStamps = new();
    
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
        SetTimeStamps(GetDataFromMidi());
    }

    private Note[] GetDataFromMidi()
    {
        _midiFile = MidiFile.Read($"{Application.streamingAssetsPath}/{fileLocation}");
        
        var notes = _midiFile.GetNotes();
        var array = new Note[notes.Count];
        notes.CopyTo(array, 0);
        
        return array;
    }

    private void SetTimeStamps(Note[] notes)
    {
        foreach (var note in notes)
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(
                note.Time, _midiFile.GetTempoMap());

            _timeStamps.Add(
                (double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds +
                (double)metricTimeSpan.Milliseconds / 1000f);
        }
    }

    private void Update()
    {
        if (_spawnIndex < _timeStamps.Count) 
            CheckSpawnIndex();
        
        if (_inputIndex < _timeStamps.Count)
            CheckInputIndex();
    }

    private void CheckSpawnIndex()
    {
        var timeStamp = _timeStamps[_spawnIndex];

        if (GameManager.GetAudioSourceTime() < timeStamp - GameManager.Instance.aliveTime) return;
        var note = Instantiate(notePrefab, transform).GetComponent<NoteManager>();
        
        _notes.Add(note);
        note.assignTime = (float)timeStamp;
        
        _spawnIndex++;
    }
    
    private void CheckInputIndex()
    {
        var timeStamp = _timeStamps[_inputIndex];
        var errorMargin = GameManager.Instance.errorMargin;
        var audioSourceTime =
            GameManager.GetAudioSourceTime() - (GameManager.Instance.inputDelay / 1000.0);

        if (input.triggered && _inputIndex < _notes.Count)
        {
            if (_notes[_inputIndex].isColliding)
            {
                ScoreManager.Hit();
                if (_notes[_inputIndex].gameObject)
                    Destroy(_notes[_inputIndex].gameObject);
                
                print($"Hit on {_inputIndex} note");
                _inputIndex++;
            }
            else
            {
                ScoreManager.Punish();
                print($"Hit with {Math.Abs(audioSourceTime - timeStamp)} delay on {_inputIndex} note");
            }
        }

        if (timeStamp + errorMargin <= audioSourceTime)
        {
            ScoreManager.Miss();
            print($"Missed {_inputIndex} note");
            _inputIndex++;
        }
    }
}