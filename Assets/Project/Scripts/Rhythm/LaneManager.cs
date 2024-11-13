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

    [Tooltip("Name of the instrument (weapon) played during this lane")]
    public GunType instrument;

    [Header("User interaction")]
    [Tooltip("Note object that will be spawned in this lane")]
    public GameObject notePrefab;
    [Tooltip("Allows changing note scale and it's hit points")]
    public float priorityModifier = 1.0f;
    [Tooltip("Input action to register by lane")]
    public InputAction input;

    public GameManager gameManager;

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

    protected void Start()
    {
        SetTimeStamps(GetDataFromMidi());
    }

    private Note[] GetDataFromMidi()
    {
        _midiFile = MidiFile.Read(
            $"{Application.streamingAssetsPath}/{gameManager.audioSource.clip.name}/{fileLocation}");

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
        var instantiateTime = 
            (float)_timeStamps[_spawnIndex] - GameManager.Instance.aliveTime;

        if (GameManager.GetAudioSourceTime() < instantiateTime) return;

        _notes.Add(CreateNode(instantiateTime));
        _spawnIndex++;
    }

    private NoteManager CreateNode(float instantiateTime)
    {
        var note = Instantiate(notePrefab, transform)
            .GetComponent<NoteManager>();
        
        note.lane = this;
        note.instantiateTime = instantiateTime;
        
        return note;
    }

    private void CheckInputIndex()
    {
        var timeStamp = _timeStamps[_inputIndex];
        var audioSourceTime =
            GameManager.GetAudioSourceTime() - (GameManager.Instance.inputDelay / 1000.0);

        if (input.triggered && _inputIndex < _notes.Count)
        {
            if (_notes[_inputIndex].isColliding)
            {
                ScoreManager.Hit(priorityModifier);
                if (_notes[_inputIndex].gameObject)
                    Destroy(_notes[_inputIndex].gameObject);

                print($"Hit on {_inputIndex} note of {instrument}");
                _inputIndex++;
            }
            else
            {
                ScoreManager.Miss(priorityModifier);
                print($"Early hit on {_inputIndex} note with {Math.Abs(audioSourceTime - timeStamp)} delay");
            }
        }

        if (GameManager.Instance.errorMargin + timeStamp <= audioSourceTime)
        {
            ScoreManager.Miss(priorityModifier);
            print($"Missed {_inputIndex} note of {instrument}");
            _inputIndex++;
        }
    }
}