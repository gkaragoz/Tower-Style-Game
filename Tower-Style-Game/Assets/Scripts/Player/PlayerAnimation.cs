using UnityEngine;

namespace GK {

    public class PlayerAnimation : MonoBehaviour {

        private Animator _animator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerMotor _playerMotor;

        public const string JUMP_BEGIN = "JumpBegin";
        public const string JUMP_START = "JumpStart";
        public const string JUMP_PEEK = "JumpPeek";
        public const string JUMP_END = "JumpEnd";
        public const string JUMP_WALL = "JumpWall";
        public const string INPUT_CANCEL = "InputCancel";
        public const string PLATFORM_FAIL = "PlatformFail";
        private void Awake() {
            _animator = GetComponentInChildren<Animator>();
            _playerGroundChecker = GetComponent<PlayerGroundChecker>();
            _playerMotor = GetComponent<PlayerMotor>();
        }
        private void Update() {
            _animator.SetBool("isFalling", _playerGroundChecker.IsFalling);
        }
        private void Start() {
            _playerGroundChecker.OnGrounded += OnGrounded;
            _playerGroundChecker.OnPeeked += OnPeek;
            _playerGroundChecker.OnHitWall += OnHitWall;

            _playerMotor.OnJumped += OnJumped;
        }
        private void OnHitWall(bool isLeft) {
            if (isLeft) {
                _animator.SetTrigger(JUMP_WALL);
            } else {
                _animator.SetTrigger(JUMP_WALL);
            }
        }
        public void OnInputCancel() {
            _animator.SetTrigger(INPUT_CANCEL);
        }
        public void OnInputBegin(Vector2 startPosition) {
            _animator.SetTrigger(JUMP_BEGIN);
        }
        public void OnInputDragging(Vector2 draggingPosition, Vector2 direction) {
            //_animator.SetFloat(INPUT, ExtensionMethods.Map(direction.magnitude, 0, InputManager.instance.ClampedInputMagnitude, 0, 1));
        }
        private void OnJumped() {
            ResetTriggers();
            _animator.SetTrigger(JUMP_START);
        }
        private void OnPeek() {
            _animator.SetTrigger(JUMP_PEEK);
        }
        private void OnGrounded() {
            _animator.SetTrigger(JUMP_END);
        }

        private void ResetTriggers() {
            _animator.ResetTrigger(JUMP_BEGIN);
            _animator.ResetTrigger(JUMP_END);
            _animator.ResetTrigger(JUMP_PEEK);
            _animator.ResetTrigger(PLATFORM_FAIL);
        }

    }

}
