using UnityEngine;


namespace GY{

public class FireHolder : MonoBehaviour
 {
        [SerializeField]
        private  float fireLenght=5;
        [SerializeField]
        private float fireSpeed=1;
        [SerializeField]
        private float startWaitTime = 1;
        [SerializeField]
        private float endWaitTime = 1;
        private void Start() {
            OnStart();
            
        }

        public void OnComp() {
            LeanTween.scaleX(transform.gameObject, 0, fireSpeed).setDelay(startWaitTime).setOnComplete(OnStart);
        }
        public void OnStart() {
            LeanTween.scaleX(transform.gameObject, fireLenght, fireSpeed).setDelay(endWaitTime).setOnComplete(OnComp);

        }
    }
}