﻿using System;
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
		public const string INPUT = "Input";

		private void Awake() {
			_animator = GetComponentInChildren<Animator>();
			_playerGroundChecker = GetComponent<PlayerGroundChecker>();
			_playerMotor = GetComponent<PlayerMotor>();
		}

		private void Start() {
			_playerGroundChecker.OnGrounded += OnGrounded;
			_playerGroundChecker.OnPeeked += OnPeek;
			_playerMotor.OnJumped += OnJumped;

			InputManager.instance.OnInputBegin += OnInputBegin;
			InputManager.instance.OnInputDragging += OnInputDragging;
		}

		private void OnInputBegin(Vector2 startPosition) {
			Debug.Log("JUMP_BEGIN");
			_animator.SetTrigger(JUMP_BEGIN);
		}

		private void OnInputDragging(Vector2 draggingPosition, Vector2 direction) {
			_animator.SetFloat(INPUT, ExtensionMethods.Map(direction.magnitude, 0, InputManager.instance.ClampedInputMagnitude, 0, 1));
		}

		private void OnJumped() {
			Debug.Log("JUMP_START");
			_animator.ResetTrigger(JUMP_BEGIN);
			_animator.ResetTrigger(JUMP_END);
			_animator.ResetTrigger(JUMP_PEEK);

			_animator.SetTrigger(JUMP_START);
		}

		private void OnPeek() {
			Debug.Log("JUMP_PEEK");
			_animator.SetTrigger(JUMP_PEEK);
		}

		private void OnGrounded() {
			Debug.Log("JUMP_END");
			_animator.SetTrigger(JUMP_END);
		}

	}

}
