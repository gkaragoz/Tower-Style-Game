using UnityEngine;
using GK;
using System;

namespace GY {

    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour {

        #region Singleton

        public static PlayerController instance;
        private void Awake() {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }

        #endregion
        [SerializeField]
        private UIManager _uIManager;
        private PlayerMotor _playerMotor;
        private PlayerAnimation _playerAnimation;
        private CollisionDedector _colDedector;
        private PlayerGroundChecker _playerGroundChecker;
        private int _jumpTimes = 0;
        [Header("Debug")]
        [SerializeField]
        [Utils.ReadOnly]
        private bool _hasDoubleJump;
        [SerializeField]
        [Utils.ReadOnly]
        private bool _hasArmor;

        public bool HasArmor {
            get { return _hasArmor; }
            set { _hasArmor = value; }
        }
        public bool HasDoubleJump {
            get { return _hasDoubleJump; }
            set { _hasDoubleJump = value; }
        }

        private void Start() {
            _playerMotor = GetComponent<PlayerMotor>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _colDedector = GetComponentInChildren<CollisionDedector>();
            _playerGroundChecker = GetComponent<PlayerGroundChecker>();
            InputManager.instance.OnInputBegin += OnInputBegin;
            InputManager.instance.OnInputDragging += OnInputDragging;
            InputManager.instance.OnInputEnd += OnInputEnd;
            _playerGroundChecker.OnGrounded += OnGrounded;
            _colDedector.OnGameOver += OnGameOver;
        }

        private void OnGrounded() {
            _jumpTimes++;
        }

        private void Update() {

            if (_playerGroundChecker.IsGrounded) {
                InputManager.instance.enabled = true;
            } else {
                InputManager.instance.enabled = HasDoubleJump;
            }


        }

        private void OnGameOver() {
            _playerAnimation.OnGameOver();
        }

        private void OnInputBegin(Vector2 startPosition) {
            _playerAnimation.OnInputBegin(startPosition);
        }

        private void OnInputDragging(Vector2 draggingPosition, Vector2 direction) {
            _playerAnimation.OnInputDragging(draggingPosition, direction);
        }

        private void OnInputEnd(Vector2 endPosition, Vector2 direction, float selectedInputPower) {

            if (selectedInputPower <= .5f) {
                _playerAnimation.OnInputCancel();
                return;
            }
            if (_playerGroundChecker.IsGrounded) {
                _playerMotor.Jump(direction, selectedInputPower);
            } else if (HasDoubleJump) {
                _playerMotor.Jump(direction, selectedInputPower);
                HasDoubleJump = false;
                _uIManager.CloseDoubleJump();
            }
        }
    }

}
