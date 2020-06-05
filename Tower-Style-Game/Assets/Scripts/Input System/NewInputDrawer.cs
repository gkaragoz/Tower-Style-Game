using UnityEngine;

namespace GK {

    public class NewInputDrawer : MonoBehaviour {

        [SerializeField]
        private Transform _targetTransform = null;
        [SerializeField]
        private Vector2 _drawOffset = Vector2.zero;  
        [SerializeField]
        private float _projectileLengthMultiplier = 1;
        [SerializeField]
        LineRenderer _lr;
        [SerializeField]
        private int _indicatorPointCount=2;
        [SerializeField]
        private Gradient _indicatorLowPowerPalette;
        [SerializeField]
        private Gradient _indicatorMediumPowerPalette;
        [SerializeField]
        private Gradient _indicatorHighPowePalette;


        private void Start() {
            InputManager.instance.OnInputBegin += OnInputBegin;
            InputManager.instance.OnInputDragging += OnInputDragging;
            InputManager.instance.OnInputEnd += OnInputEnd;
            _lr.positionCount = _indicatorPointCount;         
        }

        private void OnInputBegin(Vector2 startPos) {
            _lr.enabled = true;
            
        }

        private void OnInputDragging(Vector2 currentPos, Vector2 dragPos) {
            SetPositionProjectiles(currentPos, dragPos);
        }

        private void OnInputEnd(Vector2 startPos, Vector2 endPos, float arg3) {
            _lr.enabled = false;
        }

        private void SetPositionProjectiles(Vector3 currentPos, Vector3 dragPos) {
            Vector3 distanceProjectile = dragPos / _indicatorPointCount;

            for (int ii = 0; ii < _indicatorPointCount; ii++) {
                Vector3 lastPos = (_targetTransform.position + new Vector3(_drawOffset.x, _drawOffset.y, 0)) + distanceProjectile * ii * _projectileLengthMultiplier;
                _lr.SetPosition(ii, lastPos);
                Debug.Log(distanceProjectile.magnitude);
                if (distanceProjectile.magnitude<0.2f) {
                    _lr.colorGradient = _indicatorLowPowerPalette;
                } else if (distanceProjectile.magnitude<.4f) {
                    _lr.colorGradient = _indicatorMediumPowerPalette;
                }else if (distanceProjectile.magnitude<.5f) {
                    _lr.colorGradient = _indicatorHighPowePalette;

                }
                
            }
        }
    }
}