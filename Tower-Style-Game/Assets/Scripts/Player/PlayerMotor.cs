using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace GK {

    public class PlayerMotor : MonoBehaviour {

        public Action OnJumped;

        public GameObject _cameraParent;

        private const float MAX_ROT_LEFT = -89.9f;
        private const float MAX_ROT_RIGHT = 89.9f;
        private const float MIN_ROT_INPUT = -2.5f;
        private const float MAX_ROT_INPUT = 2.5f;
        [SerializeField]
        private float _rotationSpeed=0;
        [SerializeField]
        private float _baseJumpForce = 1f;
        [SerializeField]
        private ForceMode2D _forceMode2D = ForceMode2D.Impulse;


        private Rigidbody2D _rb2D = null;
        private PlayerGroundChecker _playerGroundChecker;
        private void Start() {
            _rb2D = GetComponent<Rigidbody2D>();
            _playerGroundChecker = GetComponent<PlayerGroundChecker>();
            _playerGroundChecker.OnPeeked += OnPeeked;
            InputManager.instance.OnInputEnd += OnInputEnd;
        }
        private void OnInputEnd(Vector2 endPos, Vector2 direction, float magnitude) {
            float preMapValue = direction.x * magnitude;
            RotateTo(ExtensionMethods.Map(preMapValue, MIN_ROT_INPUT, MAX_ROT_INPUT, MAX_ROT_RIGHT, MAX_ROT_LEFT));
            RotateToCamera(ExtensionMethods.Map(preMapValue, MIN_ROT_INPUT, MAX_ROT_INPUT, 15, -15));
        }
        private void OnPeeked() {
            RotateTo(0);
            RotateToCamera(0);
        }
        private IEnumerator IRotateTo(float targetX) {
            LeanTween.rotateY(this.gameObject, targetX, _rotationSpeed);
            yield return null;
        }
        private IEnumerator IRotateToCamera(float targetX) {
            LeanTween.rotateY(_cameraParent, targetX, 0.5f);
            yield return null;
        }
        public void Jump(Vector2 direction, float selectedInputPower) {
            _rb2D.velocity = Vector2.zero;
            _rb2D.AddForce(direction * _baseJumpForce * selectedInputPower, _forceMode2D);
            OnJumped?.Invoke();
        }
        public void RotateTo(float targetX) {
            StartCoroutine(IRotateTo(targetX));
        }
        public void RotateToCamera(float targetX) {
            StartCoroutine(IRotateToCamera(targetX));
        }

    }
}
