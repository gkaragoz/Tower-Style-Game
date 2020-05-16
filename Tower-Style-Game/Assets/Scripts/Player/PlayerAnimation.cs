using System;
using UnityEngine;

namespace GK {

	public class PlayerAnimation : MonoBehaviour {

		private Animator _animator;
		private PlayerGroundChecker _playerGroundChecker;
		private PlayerMotor _playerMotor;

		private const string JUMP_BEGIN = "JumpBegin";
		private const string JUMP_START = "JumpStart";
		private const string JUMP_PEEK = "JumpPeek";
		private const string JUMP_END = "JumpEnd";
		private const string JUMP_WALL = "JumpWall";
		private const string INPUT = "Input";

		private void Awake() {
			_animator = GetComponentInChildren<Animator>();
			_playerGroundChecker = GetComponent<PlayerGroundChecker>();
			_playerMotor = GetComponent<PlayerMotor>();
		}

		private void Start() {
			_playerGroundChecker.OnGrounded += OnGrounded;
			_playerGroundChecker.OnSliding += OnSliding;
			_playerMotor.OnJumped += OnJumped;
			_playerMotor.OnPeek += OnPeek;

			InputManager.instance.OnInputBegin += OnInputBegin;
			InputManager.instance.OnInputDragging += OnInputDragging;
		}

		private void OnInputBegin(Vector2 startPosition) {
			_animator.SetTrigger(JUMP_BEGIN);
		}

		private void OnInputDragging(Vector2 draggingPosition, Vector2 direction) {
			
		}

		private void OnJumped() {

		}
		private void OnPeek() {

		}

		private void OnGrounded() {

		}

		private void OnSliding(PlayerGroundChecker.Direction direction) {
			
		}
	}

}
