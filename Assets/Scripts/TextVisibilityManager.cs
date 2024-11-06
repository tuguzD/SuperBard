using TMPro;
using System;
using UnityEngine;

public class TextVisibilityManager : MonoBehaviour
{
    public TextMeshProUGUI comboScore;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = gameObject.GetComponent<TextMeshProUGUI>();
        _text.enabled = false;
    }

    private void Update()
    {
        _text.enabled = Convert.ToInt32(comboScore.text) >= 2;
    }
}