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

		private PlayerMotor _playerMotor;
		private PlayerAnimation _playerAnimation;
		private CollisionDedector _colDedector;
		private PlayerGroundChecker _playerGroundChecker;

		private void Start() {
			_playerMotor = GetComponent<PlayerMotor>();
			_playerAnimation = GetComponent<PlayerAnimation>();
			_colDedector = GetComponentInChildren<CollisionDedector>();
			_playerGroundChecker = GetComponent<PlayerGroundChecker>();
			InputManager.instance.OnInputBegin += OnInputBegin;
			InputManager.instance.OnInputDragging += OnInputDragging;
			InputManager.instance.OnInputEnd += OnInputEnd;
			_colDedector.OnGameOver += OnGameOver;
		}

		private void Update() {
			InputManager.instance.enabled=_playerGroundChecker.IsGrounded;
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
			if (selectedInputPower<=0.1f) {
				_playerAnimation.OnInputCancel();
				return;
			}
			_playerMotor.Jump(direction, selectedInputPower);
		}


		
	}

}
