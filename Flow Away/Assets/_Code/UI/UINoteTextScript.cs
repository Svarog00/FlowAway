using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoteTextScript : MonoBehaviour
{
    private Text _textField;
    private Canvas _canvas;

    private void Start()
    {
        _textField = GetComponent<Text>();

        _canvas = GetComponentInParent<Canvas>();
        _canvas.worldCamera = Camera.main;
    }

    public void Appear(string text, float duration)
    {
        _textField.CrossFadeAlpha(1f, duration, false);
        _textField.text = text;
    }

    public void Disappear(float duration)
    {
        _textField.CrossFadeAlpha(0f, duration, false);
    }

}
