using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoteTextScript : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    public void Appear(string text, float duration)
    {
        _text.CrossFadeAlpha(1f, duration, false);
        _text.text = text;
    }

    public void Disappear(float duration)
    {
        _text.CrossFadeAlpha(0f, duration, false);
    }

}
