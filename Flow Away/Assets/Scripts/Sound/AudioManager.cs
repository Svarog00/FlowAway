using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundArray;
    public static AudioManager instance;

    // Awake before Start
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        foreach(Sound s in soundArray) //Получаем звуки из массива
        {
            s.source = gameObject.AddComponent<AudioSource>(); //Добавляем источник звука
            s.source.clip = s.clip; //Источнику звука добавляем клип, который находится в массиве

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }


    // Update is called once per frame
    public void Play(string soundName)
    {
        Sound s = Array.Find(soundArray, sound => sound.name == soundName);
        if(s == null)
        {
            Debug.LogError("Sound " + soundName + " not found");
            return;
        }
        s.source.Play();
    }
}
