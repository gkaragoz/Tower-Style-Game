using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GY {

    public class GoldLight : MonoBehaviour {
        public Light light;
        public float minVal = 3;
        public float maxVal = 8;
        public float speed;
        public LeanTweenType type;
        private void Awake() {

            LeanTween.value(this.gameObject, minVal, maxVal, speed).setLoopPingPong().setEase(type).setOnUpdate((float value) => {
                light.intensity = value;
            });
        }
    }
}