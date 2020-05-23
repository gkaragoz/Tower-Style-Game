using System;
using System.Collections;
using UnityEngine;

namespace GK {

    public class Platform : MonoBehaviour, IPlatform {
        [SerializeField]
        private Transform lifeBar=null;
        private PlayerMotor _playerMotor;

        [SerializeField]
        private float _destroyTime = 3f;

        private float normalizedTime = 0;
        private Coroutine destroyCoroutine;



        private void Start() {
            _playerMotor = FindObjectOfType<PlayerMotor>();

            _playerMotor.OnJumped += OnJumped;
        }

        private void OnJumped() {
            StopTimer();
        }

        private void StopTimer() {
            if (destroyCoroutine!=null) {

            StopCoroutine(destroyCoroutine);
            }

        }

        public IEnumerator IDestroy(Action onDestroyed) {
            while (true) {
                yield return new WaitForSeconds(_destroyTime);

                onDestroyed();
                this.gameObject.SetActive(false);

                break;
            }
        }

        public void DestroyPlatform(Action onDestroyed) {
            StartTimer(onDestroyed);            
        }

        private void StartTimer(Action onDestroyed) {
           destroyCoroutine= StartCoroutine(Countdown(onDestroyed));
        }



        private IEnumerator Countdown(Action onDestroyed) {

            while (normalizedTime <= 1f) {
                normalizedTime += Time.deltaTime / _destroyTime;
                lifeBar.localScale = new Vector3(lifeBar.localScale.x,1- normalizedTime, lifeBar.localScale.z);
                yield return null;
            }
            Debug.Log("Bitti");
            StopCoroutine(destroyCoroutine);
            this.gameObject.SetActive(false);
            onDestroyed();
        }
    }

}
