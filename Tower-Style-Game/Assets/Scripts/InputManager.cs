using System;
using UnityEngine;

namespace GK {

	public class InputManager : MonoBehaviour {

		public Action<Vector2> OnInputBegin;
		public Action<Vector2, Vector2> OnInputDragging;
		public Action<Vector2, Vector2> OnInputEnd;

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
				_startPosition = Camera.main.ScreenToWorldPoint(mousePos);
				_currentPosition = _startPosition;

				OnInputBegin?.Invoke(_startPosition);
			}

			if (Input.GetMouseButton(0)) {
				Vector2 mousePos = Input.mousePosition;
				_currentPosition = Camera.main.ScreenToWorldPoint(mousePos);
				_direction = _startPosition - _currentPosition;

				OnInputDragging?.Invoke(_currentPosition, _direction.normalized);
			}

			if (Input.GetMouseButtonUp(0)) {
				Vector2 mousePos = Input.mousePosition;
				_endPosition = Camera.main.ScreenToWorldPoint(mousePos);

				_direction = _startPosition -_currentPosition;
				OnInputEnd?.Invoke(_endPosition, _direction.normalized);

				ResetInputs();
			}
		}

		private void ResetInputs() {
			_startPosition = Vector2.zero;
			_endPosition = Vector2.zero;
			_currentPosition = Vector2.zero;
			_direction = Vector2.zero;
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.white;
			Gizmos.DrawLine(_startPosition, _currentPosition);
		}
	}

}
