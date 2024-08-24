using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXController : MonoBehaviour
{
    [SerializeField] private AudioSource _soundFX;
    [SerializeField] private float _delayTime;
    //audio source
    //audio clip

    public void PlaySound()
    {
        _soundFX.PlayDelayed(_delayTime);
    }
}
