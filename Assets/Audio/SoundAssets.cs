using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(MonoSender))]
public class SoundAssets : MonoBehaviour
{
    private static SoundAssets _sa;

    public static SoundAssets sa
    {
        get
        {
            if (_sa == null)                //added <SoundAssets>
                _sa = (Instantiate(Resources.Load<SoundAssets>("SoundAssets")));//as GameObject).GetComponent<SoundAssets>();
            return _sa;
        }
    }

    public SoundAudioClip[] soundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public AudioMixer MasterMixer;
}
