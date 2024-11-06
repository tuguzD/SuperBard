using TMPro;
using System;
using UnityEngine;

public class TextOpacityController : MonoBehaviour
{
    public TextMeshProUGUI comboScore;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = gameObject.GetComponent<TextMeshProUGUI>();
        _text.faceColor = new Color(
            _text.faceColor.r, _text.faceColor.g, _text.faceColor.b, 0);
    }

    private void Update()
    {
        _text.faceColor = new Color(_text.faceColor.r, _text.faceColor.g, _text.faceColor.b,
            Mathf.Lerp(_text.faceColor.a, Convert.ToInt32(comboScore.text) < 2 ? 0 : 1, Time.deltaTime * 8));
    }
}