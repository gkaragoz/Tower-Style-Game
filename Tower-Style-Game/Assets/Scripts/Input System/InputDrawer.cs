using UnityEngine;

namespace GK {

    public class InputDrawer : MonoBehaviour {

        [SerializeField]
        private Transform _targetTransform = null;
        [SerializeField]
        private Vector2 _drawOffset;
        [SerializeField]
        private GameObject _drawPrefab = null;
        [SerializeField]
        private int _projectileCount = 10;
        [SerializeField]
        private float _localScaleDivider = 1;
        [SerializeField]
        private float _projectileLengthMultiplier = 1;

        private Vector3 _startPos;
        private Vector3 _baseLocalScale;
        private GameObject[] _balls;

        private void Start() {
            InputManager.instance.OnInputBegin += OnInputBegin;
            InputManager.instance.OnInputDragging += OnInputDragging;
            InputManager.instance.OnInputEnd += OnInputEnd;

            _baseLocalScale = _drawPrefab.transform.localScale;
            _balls = new GameObject[_projectileCount];

            for (int ii = 0; ii < _projectileCount; ii++) {
                _balls[ii] = Instantiate(_drawPrefab, this.transform);
                _balls[ii].SetActive(false);
            }
        }

        private void OnInputBegin(Vector2 startPos) {
            _startPos = startPos;
        }

        private void OnInputDragging(Vector2 currentPos, Vector2 dragPos) {
            SetPositionProjectiles(currentPos, dragPos);
        }

        private void OnInputEnd(Vector2 startPos, Vector2 endPos, float arg3) {
            for (int ii = 0; ii < _balls.Length; ii++) {
                _balls[ii].SetActive(false);
                _balls[ii].transform.localScale = _baseLocalScale;
            }
        }

        private void SetPositionProjectiles(Vector3 currentPos, Vector3 dragPos) {
            Vector3 distanceProjectile = dragPos / _balls.Length;

            for (int ii = 0; ii < _balls.Length; ii++) {
                _balls[ii].SetActive(true);
                _balls[ii].transform.localPosition = (_targetTransform.position + new Vector3(_drawOffset.x, _drawOffset.y, 0)) + distanceProjectile * ii * _projectileLengthMultiplier;
                _balls[ii].transform.localScale = _baseLocalScale / (_localScaleDivider * ii + 1);
            }
        }
    }
}