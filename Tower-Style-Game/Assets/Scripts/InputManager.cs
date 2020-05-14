﻿using System;
using UnityEngine;

namespace GK {

	public class InputManager : MonoBehaviour {

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

		public Action<Vector2> OnInputBegin;
		public Action<Vector2, Vector2> OnInputDragging;
		public Action<Vector2, Vector2, float> OnInputEnd;

		[SerializeField]
		private float _clampedInputMagnitude = 2.5f;

		private Vector2 _startPosition = Vector2.zero;
		private Vector2 _endPosition = Vector2.zero;
		private Vector2 _currentPosition = Vector2.zero;

		private Vector2 _direction = Vector2.zero;

		public Vector2 Direction {
			get {
				return _direction;
			}
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				Vector2 mousePos = Input.mousePosition;
				_startPosition = _camera.ScreenToWorldPoint(mousePos);

				_currentPosition = _startPosition;

				OnInputBegin?.Invoke(_startPosition);
			}

			if (Input.GetMouseButton(0)) {
				Vector2 mousePos = Input.mousePosition;
				_currentPosition = _camera.ScreenToWorldPoint(mousePos);

				_direction = _startPosition - _currentPosition;

				Vector2 clampedVector = _direction.normalized * Mathf.Clamp(_direction.magnitude, 0f, 2.5f);

				OnInputDragging?.Invoke(_currentPosition,_direction.normalized);
			}

			if (Input.GetMouseButtonUp(0)) {
				Vector2 mousePos = Input.mousePosition;
				_endPosition = _camera.ScreenToWorldPoint(mousePos);

				_direction = _startPosition -_currentPosition;

				OnInputEnd?.Invoke(
					_endPosition, 
					InputDirectionModifier.UserDirectionVector(_direction).normalized,
					Mathf.Clamp(_direction.magnitude, 0f, _clampedInputMagnitude));

				ResetInputs();
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
