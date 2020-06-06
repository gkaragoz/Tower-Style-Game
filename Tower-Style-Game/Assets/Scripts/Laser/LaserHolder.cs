using System;
using System.Collections;
using UnityEngine;

namespace GY {

    public class LaserHolder : MonoBehaviour {
        [SerializeField]
        private float _laserCloseSpeed = 1;
        [SerializeField]
        private LineRenderer _lineRenderer = null;
        [SerializeField]
        private ParticleSystem[] _VFX = null;
        [SerializeField]
        private BoxCollider2D _collider = null;
        [SerializeField]
        private AudioSource _myAudioSource;

        private void Start() {
            PlayerSoundManager.instance.SoundSettingsChanged += SoundSettingsChanged;
        }

        private void SoundSettingsChanged() {
            _myAudioSource.mute= PlayerSoundManager.instance.IsMute;
        }

        public void CloseLaser() {
            
            float value = _lineRenderer.startWidth;
            if (transform.gameObject.activeSelf) {
                LeanTween.value(_lineRenderer.gameObject, value, 0, _laserCloseSpeed).setOnUpdate((float newValue) => {
                    _lineRenderer.startWidth = newValue;
                    _lineRenderer.endWidth = newValue;
                })
                .setOnComplete(() => {
                    _collider.enabled  = false;
                });

                LeanTween.value(this.gameObject, 255f, 0f, _laserCloseSpeed).setOnUpdate((float newValue) => {
                    foreach (var item in _VFX) {
                        ParticleSystem.MainModule settings = item.main;
                        Color color = settings.startColor.color;
                        color.a = newValue;

                        settings.startColor = new ParticleSystem.MinMaxGradient(color);
                    }
                });
            }
            _myAudioSource.Stop();

        }
    }
}