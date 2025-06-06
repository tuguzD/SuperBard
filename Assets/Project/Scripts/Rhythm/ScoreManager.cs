using System;
using System.Globalization;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Game score display")]
    [Tooltip("Text displaying game total score on the screen")]
    public TMPro.TextMeshProUGUI totalScore;
    [Tooltip("Text displaying current combo score on the screen")]
    public TMPro.TextMeshProUGUI comboScore;

    [Header("Score settings")] 
    [Tooltip("Enlarges the score by 1 when combo fully divides by this number")]
    private const int ScoreEnlarger = 10;
    [Tooltip("Score that keeps track of an overall progress of a player")]
    private static float _totalScore;
    [Tooltip("Score that keeps track of consecutive successful hits of a player")]
    private static float _comboScore;

    [Header("Sound Effects")]
    [Tooltip("Source of sound effect on successfully hitting the note, adding score points")]
    public AudioSource successHitSound;
    [Tooltip("Source of sound effect on skipping the note completely, canceling current combo")]
    public AudioSource missingSound;

    public static float ComboPower => _comboScore / ScoreEnlarger;
    
    private void Start()
    {
        _comboScore = 0;
        _totalScore = 0;
    }

    public static void Hit(float priority)
    {
        if (!_comboScore.Equals(0.0f) && (int)ComboPower != 0)
        {
            _totalScore += ComboPower * priority;
            _totalScore = (float)Math.Floor(_totalScore);
        }
        _totalScore += 1 * priority;
        _comboScore += 1;

        // Instance.successHitSound.Play();
    }

    public static void Miss(float priority)
    {
        if (priority < 1) return;

        var punishment = (float)Math.Ceiling(priority);
        if (_totalScore - punishment < 0) 
            _totalScore = 0;
        else _totalScore -= punishment;

        _comboScore = 0;
            
        // Instance.missingSound.Play();
    }

    private void Update()
    {
        comboScore.text = 
            _comboScore.ToString(CultureInfo.InvariantCulture);

        totalScore.text = 
            ((int)_totalScore).ToString(CultureInfo.InvariantCulture);
    }
}