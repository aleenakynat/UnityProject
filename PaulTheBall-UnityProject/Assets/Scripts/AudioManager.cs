using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;
    public float volume=1, pitch=1;
    public bool playOnAwake=false, loop = false;
    [HideInInspector]
    public AudioSource source;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    public void Start()
    {
        foreach(Audio a in audiolist)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.playOnAwake = a.playOnAwake;
            a.source.loop = a.loop;
        }
    }
    public List<Audio> audiolist = new List<Audio>();
    public void PlayAudio(string name)
    {
        Debug.Log("Sound Manager: Playing Sound "+name);
        Audio a = audiolist.Find(audio => audio.name == name);
        a.source.Play();
        if (name == "Background")
        {
            a.source.loop = true;
            a.source.volume = 0.7f;
        }
    }
}
