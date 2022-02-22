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
        //TODO: Extract to external script
        _canvas = GetComponentInParent<Canvas>();
        _canvas.worldCamera = Camera.main;
        //-----------------------------------

        _textField = GetComponent<Text>();
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
