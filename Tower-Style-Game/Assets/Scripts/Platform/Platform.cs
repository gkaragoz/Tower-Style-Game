using System;
using System.Collections;
using UnityEngine;

namespace GK {

    public class Platform : MonoBehaviour, IPlatform {
        [SerializeField]
        private PlayerMotor _playerMotor;

        [SerializeField]
        private float _destroyTime = 3f;

        private float normalizedTime = 0;
        private Coroutine destroyCoroutine;



        private void Start() {
          
        }

      

        private void StopTimer() {
            if (destroyCoroutine!=null) {

            StopCoroutine(destroyCoroutine);
            }

        }


        public void DestroyPlatform(Action onDestroyed) {
            StartTimer(onDestroyed);            
        }

        private void StartTimer(Action onDestroyed) {
          
            // destroyCoroutine= StartCoroutine(Countdown(onDestroyed));
        }



        private IEnumerator Countdown(Action onDestroyed) {

            while (normalizedTime <= 1f) {
                normalizedTime += Time.deltaTime / _destroyTime;
                yield return null;
            }
            StopCoroutine(destroyCoroutine);
            this.gameObject.SetActive(false);
            onDestroyed();
        }
    }

}
