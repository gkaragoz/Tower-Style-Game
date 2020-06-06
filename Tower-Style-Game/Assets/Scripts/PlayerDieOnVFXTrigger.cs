using GY;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieOnVFXTrigger : MonoBehaviour {
    ParticleSystem _particleSystem;
    public AudioSource _myAudio;
    public AudioClip _effectSound;
    bool isShouted=true;



    private void Start() {
        _particleSystem = transform.GetComponent<ParticleSystem>();
        PlayerSoundManager.instance.SoundSettingsChanged += SoundsChanged;
    }

    private void SoundsChanged() {
        _myAudio.mute = PlayerSoundManager.instance.IsMute;
    }

    private void OnParticleCollision(GameObject other) {
        PlayerController.instance.Die();
    }
    private void Update() {
    
        Debug.Log(_particleSystem.particleCount);
        if (_particleSystem.particleCount >= 1f && isShouted) {
            _myAudio.PlayOneShot(_effectSound);
            isShouted = false;
        }
        if (_particleSystem.particleCount==0) {
            isShouted = true;
        }
    }
}
