using UnityEngine;

namespace GK {

    public class PopIconTween : MonoBehaviour {

        [SerializeField]
        private RectTransform _targetTween = null;
        [SerializeField]
        private float _delaySpeed = 0.5f;
        [SerializeField]
        private float _scaleAmount = 1.1f;
        [SerializeField]
        private float _scaleSpeed = 0.1f;

        private void Awake() {
            LeanTween.delayedCall(_delaySpeed, () => {
                // Pop size of button
                LeanTween.scale(_targetTween, Vector3.one * _scaleAmount, _scaleSpeed).setLoopPingPong().setEaseInOutBack().setRepeat(2);
            }).setRepeat(-1);
        }

    }

}
