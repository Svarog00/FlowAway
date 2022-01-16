using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoteTextScript : MonoBehaviour
{
    private const string CanvasName = "Canvas";
    private Text _textField;

    private void Start()
    {
        _textField = GetComponent<Text>();
        transform.SetParent(GameObject.Find(CanvasName).transform);
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
