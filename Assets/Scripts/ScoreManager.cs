using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Tooltip("Text displaying game total score on the screen")]
    public TMPro.TextMeshPro totalScoreText;
    [Tooltip("Text displaying current combo score on the screen")]
    public TMPro.TextMeshPro comboScoreText;

    [Header("Score settings")] 
    [Tooltip("Enlarges the score by 1 when combo fully divides by this number")]
    public const int ScoreEnlarger = 10;
    [Tooltip("Score that keeps track of an overall progress of a player")]
    private static float _totalScore;
    [Tooltip("Score that keeps track of consecutive successful hits of a player")]
    private static float _comboScore;

    [Header("Sound Effects")] 
    [Tooltip("Source of sound effect on successfully hitting the note")]
    public AudioSource hitSfx;
    [Tooltip("Source of sound effect on NOT hitting any note")]
    public AudioSource missSfx;

    private void Start()
    {
        Instance = this;
        _comboScore = 0;
        _totalScore = 0;
    }

    public static void Hit()
    {
        _totalScore += 1;
        if (!_comboScore.Equals(0.0f) && ((int)_comboScore / ScoreEnlarger) != 0)
        {
            _totalScore += _comboScore / ScoreEnlarger;
            _totalScore = (float)Math.Ceiling(_totalScore);
        }

        _comboScore += 1;

        // Instance.hitSfx.Play();
    }

    public static void Miss()
    {
        Punish();
        _comboScore = 0;
        // Instance.missSfx.Play();
    }

    public static void Punish()
    {
        if (_totalScore > 0) _totalScore -= 0.5f;
    }

    private void Update()
    {
        comboScoreText.text = _comboScore.ToString();
        totalScoreText.text = _totalScore.ToString();
    }
}