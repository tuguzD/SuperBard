using System;
using System.Globalization;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

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
    [Tooltip("Source of sound effect on hitting the note too early, resulting in score penalty")]
    public AudioSource punishingSound;
    [Tooltip("Source of sound effect on successfully hitting the note, adding score points")]
    public AudioSource successHitSound;
    [Tooltip("Source of sound effect on skipping the note completely, canceling current combo")]
    public AudioSource missingSound;

    private void Start()
    {
        Instance = this;
        _comboScore = 0;
        _totalScore = 0;
    }

    public static void Hit(float modifier)
    {
        if (!_comboScore.Equals(0.0f) && ((int)_comboScore / ScoreEnlarger) != 0)
        {
            _totalScore += (_comboScore / ScoreEnlarger) * modifier;
            _totalScore = (float)Math.Floor(_totalScore);
        }
        _totalScore += 1 * modifier;
        _comboScore += 1;

        // Instance.successHitSound.Play();
    }

    public static void Miss(float modifier)
    {
        Punish(modifier);
        _comboScore = 0;
        
        // Instance.missingSound.Play();
    }

    public static void Punish(float modifier)
    {
        if (_totalScore - 0.5f * modifier > 0) 
            _totalScore -= 0.5f * modifier;
        
        // Instance.punishingSound.Play();
    }

    private void Update()
    {
        comboScore.text = _comboScore.ToString(CultureInfo.InvariantCulture);
        totalScore.text = _totalScore.ToString(CultureInfo.InvariantCulture);
    }
}