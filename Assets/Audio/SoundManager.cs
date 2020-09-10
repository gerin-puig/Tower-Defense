using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager
{
    public enum Sound
    {
        EnemyDeath,
        TowerShoot1,
        BGM,
    }

    private static AudioMixer masterMixer;

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    private static bool fading;
    private static MonoBehaviour myMono;
    private static List<AudioSource> sources;
    
    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.TowerShoot1] = 0;
        //masterMixer = Resources.Load<AudioMixer>("Master");
        masterMixer = SoundAssets.sa.MasterMixer;
        sources = new List<AudioSource>();
    }

    //for oneshot
    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                oneShotAudioSource.outputAudioMixerGroup = masterMixer.FindMatchingGroups("Sfx")[0];
                
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound), 0.7f);
        }
    }


    public static void PlayMusic(Sound sound, bool looping = true, bool shouldPlay = true)
    {
        GameObject music = new GameObject("BGM");
        //music.transform.position = (new Vector3(0,0,0));
        //music.tag = "Audio";
        AudioSource audioSource = music.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = masterMixer.FindMatchingGroups("BGM")[0];
        //audioSource.outputAudioMixerGroup = bgmMixer;
        audioSource.loop = looping;
        audioSource.clip = GetAudioClip(sound);

        if(shouldPlay)
            audioSource.Play();
        else
            sources.Add(audioSource);
    }

    //change or remove
    public static void PlayMusic2(Sound sound)
    {
        GameObject music = new GameObject("BGM");
        
        AudioSource audioSource = music.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = masterMixer.FindMatchingGroups(" ")[0];
        audioSource.loop = true;
        audioSource.clip = GetAudioClip(sound);
        audioSource.Play();
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.TowerShoot1:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePLayed = soundTimerDictionary[sound];
                    //delay
                    float rewindTimer = 2f;
                    if(lastTimePLayed + rewindTimer < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.sa.soundAudioClips)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    public static void CrossfadeGroups(float duration, string crossfadeFrom, string crossFadeTo)
    {
        if (!fading)
        {
            myMono.StartCoroutine(CrossFade(duration, crossfadeFrom, crossFadeTo));
        }
    }

    public static void MonoParser(MonoBehaviour mono)
    {
        myMono = mono;
    }

    private static IEnumerator CrossFade(float fadetime, string mixer1, string mixer2)
    {
        fading = true;
        float currentTime = 0;

        while (currentTime <= fadetime)
        {
            currentTime += Time.deltaTime;
            
            masterMixer.SetFloat(mixer1, Mathf.Log10(Mathf.Lerp(1, 0.0001f, currentTime / fadetime)) * 20);
            masterMixer.SetFloat(mixer2, Mathf.Log10(Mathf.Lerp(0.0001f, 1, currentTime / fadetime)) * 20);

            yield return null;
        }

        fading = false;
    }

    public static void FadeAudioSources(int firstClip, int secondClip)
    {
        double clipDuration = (double)sources[firstClip].clip.samples / sources[firstClip].clip.frequency;
        sources[firstClip].PlayScheduled(AudioSettings.dspTime + 0.1);
        sources[secondClip].PlayScheduled(AudioSettings.dspTime + 0.1 + clipDuration);
    }
}
