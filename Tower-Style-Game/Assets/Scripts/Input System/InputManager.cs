using System;
using UnityEngine;
using GY;
using UnityEngine.EventSystems;

namespace GK {

    public class InputManager : MonoBehaviour {
        public Action<Vector2> OnInputBegin;
        public Action<Vector2, Vector2> OnInputDragging;
        public Action<Vector2, Vector2, float> OnInputEnd;

        #region Singleton

        public static InputManager instance;
        private void Awake() {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        #endregion

        [SerializeField]
        private Camera _camera = null;
        [SerializeField]
        private bool _clampInputActive = false;
        [SerializeField]
        private float _clampedInputMagnitude = 10f;
        [SerializeField]
        private float _inputMultiplier = 4;

        private Vector2 _startPosition = Vector2.zero;
        private Vector2 _endPosition = Vector2.zero;
        private Vector2 _currentPosition = Vector2.zero;
        private Vector2 _direction = Vector2.zero;
        private float _endMagnitude;
        private bool _isMouseButtonDown;

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                bool noUI = EventSystem.current.IsPointerOverGameObject();
                if (!noUI) {
                    Vector3 mousePos = Input.mousePosition;
                    _startPosition = _camera.ScreenToViewportPoint(mousePos);

                    _currentPosition = _startPosition;

                    _isMouseButtonDown = true;

                    OnInputBegin?.Invoke(_startPosition * _inputMultiplier);
                }

            }
            if (Input.GetMouseButton(0) && _isMouseButtonDown) {
                Vector2 mousePos = Input.mousePosition;
                _currentPosition = _camera.ScreenToViewportPoint(mousePos);
                _direction = (_startPosition - _currentPosition) * _inputMultiplier;

                if (_clampInputActive) {
                    _direction = _direction.normalized * Mathf.Clamp(_direction.magnitude, 0f, _clampedInputMagnitude);
                }

                OnInputDragging?.Invoke(_currentPosition, InputDirectionModifier.UserDirectionVector(_direction));
            }
            if (Input.GetMouseButtonUp(0) && _isMouseButtonDown) {
                Vector2 mousePos = Input.mousePosition;
                _endPosition = _camera.ScreenToViewportPoint(mousePos);

                _direction = (_startPosition - _currentPosition) * _inputMultiplier;
                _endMagnitude = _direction.magnitude;

                if (_clampInputActive) {
                    _endMagnitude = Mathf.Clamp(_direction.magnitude, 0f, _clampedInputMagnitude);
                }

                OnInputEnd?.Invoke(
                _endPosition,
                InputDirectionModifier.UserDirectionVector(_direction).normalized,
                _endMagnitude);

                ResetInputs();

                _isMouseButtonDown = false;
            }
        }
        private void ResetInputs() {
            _startPosition = Vector2.zero;
            _endPosition = Vector2.zero;
            _currentPosition = Vector2.zero;
            _direction = Vector2.zero;
        }

    }

}
