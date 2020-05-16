using System;
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

		public const string PLATFORM_FAIL = "PlatformFail";

		public const string INPUT = "Input";

		private void Awake() {
			_animator = GetComponentInChildren<Animator>();
			_playerGroundChecker = GetComponent<PlayerGroundChecker>();
			_playerMotor = GetComponent<PlayerMotor>();
		}

		private void Start() {
			_playerGroundChecker.OnGrounded += OnGrounded;
			_playerGroundChecker.OnPeeked += OnPeek;
			_playerGroundChecker.OnIsFalling += OnFalling;

			_playerMotor.OnJumped += OnJumped;

			InputManager.instance.OnInputBegin += OnInputBegin;
			InputManager.instance.OnInputDragging += OnInputDragging;
		}

		private void OnInputBegin(Vector2 startPosition) {
			_animator.SetTrigger(JUMP_BEGIN);
		}

		private void OnInputDragging(Vector2 draggingPosition, Vector2 direction) {
			_animator.SetFloat(INPUT, ExtensionMethods.Map(direction.magnitude, 0, InputManager.instance.ClampedInputMagnitude, 0, 1));
		}

		private void OnJumped() {
			ResetTriggers();
			_animator.SetTrigger(JUMP_START);
		}

		private void OnFalling() {
			ResetTriggers();

			_animator.SetTrigger(PLATFORM_FAIL);
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
