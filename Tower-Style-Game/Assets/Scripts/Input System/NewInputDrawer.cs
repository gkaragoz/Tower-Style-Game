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
        private int _indicatorPointCount = 2;
        [SerializeField]
        private float _zOffset = -1f;

        [SerializeField]
        private Color _minColor;
        [SerializeField]
        private Color _maxColor;

        [SerializeField]
        [Utils.ReadOnly]
        private float t = 1;
        [SerializeField]
        [Utils.ReadOnly]
        private float minT = 0;
        [SerializeField]
        [Utils.ReadOnly]
        private float maxT = 100;

        [SerializeField]
        [Utils.ReadOnly]
        private float _minClampMag = 0.1f;
        [SerializeField]
        [Utils.ReadOnly]
        private float _maxClampMag = 0.12f;

        private LineRenderer _lr;

        private void Start() {
            _lr = GetComponent<LineRenderer>();

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
                lastPos.z = _zOffset;
                _lr.SetPosition(ii, lastPos);
            }

            maxT = _projectileLengthMultiplier;
            t = distanceProjectile.magnitude.Map(_minClampMag, maxT, minT, maxT);

            _lr.endColor = Color.Lerp(_minColor, _maxColor, t);
        }
    }
}