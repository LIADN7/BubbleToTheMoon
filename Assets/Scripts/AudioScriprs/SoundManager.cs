using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

[System.Serializable]
    public class Sound
    {
        public String name;
        public AudioClip[] clips;
        public AudioMixerGroup Output; 
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0f, 3f)] public float pitch = 1f;
        public bool loop; 
        [HideInInspector] public AudioSource source;


    }

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;
    public Sound[] sounds;
    private void Awake()
    {
        
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad (gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            if (s.clips == null || s.clips.Length == 0)
            {
                Debug.LogError($"Sound {s.name} has no clips assigned!");
                continue;
        }

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clips[0];
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.Output;
            // s.source.outputAudioMixerGroup = s.outputGroup;
        }
        
    }
    public void Play(SoundsNames name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name.ToString());
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        Debug.Log("Player hit sound play!");
        s.source.Play();
    }
    public void Stop(SoundsNames name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name.ToString());
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
    }

    public void PlayOneShot(SoundsNames name, int i)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name.ToString());
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        if (i<s.clips.Length)
        {
            s.source.PlayOneShot(s.clips[i]);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum SoundsNames 
{
BlowinGumUP, 
BlowinGumDown,
GumExplosion,
SnotUp,
SnotDown,
SnotExplosion,
BeesIdleSound,
BeesExplotion,
BirdIdle,
BirdExplotion,
WinSound,
LoseSound,
StartGame,
CountToGame,
MainMenuMusic,
MainGameMusic,
AmbianceSound,
PauseButton,
ResumeButton,
RestartGame,
StartGameButton,
BirdAnimation,

}
