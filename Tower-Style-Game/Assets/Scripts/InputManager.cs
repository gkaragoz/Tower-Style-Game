using System;
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

		private Vector2 _startPosition = Vector2.zero;
		private Vector2 _endPosition = Vector2.zero;
		private Vector2 _currentPosition = Vector2.zero;

		private Vector2 _direction = Vector2.zero;

		public Vector2 Direction {
			get {
				return _direction;
			}
		}

		private InputBarManager _inputBarManager = null;

		private void Start() {
			_inputBarManager = GetComponent<InputBarManager>();
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				Vector2 mousePos = Input.mousePosition;
				_startPosition = _camera.ScreenToWorldPoint(mousePos);

				_currentPosition = _startPosition;

				_inputBarManager.Play();

				OnInputBegin?.Invoke(_startPosition);
			}

			if (Input.GetMouseButton(0)) {
				Vector2 mousePos = Input.mousePosition;
				_currentPosition = _camera.ScreenToWorldPoint(mousePos);

				_direction = _startPosition - _currentPosition;

				OnInputDragging?.Invoke(_currentPosition,_direction.normalized);
			}

			if (Input.GetMouseButtonUp(0)) {
				Vector2 mousePos = Input.mousePosition;
				_endPosition = _camera.ScreenToWorldPoint(mousePos);

				_direction = _startPosition -_currentPosition;

				_inputBarManager.Stop();

				OnInputEnd?.Invoke(
					_endPosition, 
					InputDirectionModifier.InputDirectionVector(_direction).normalized, 
					_inputBarManager.GetSelectedPowerValue());

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
			Gizmos.DrawLine(_startPosition, InputDirectionModifier.UserDirectionVector(_direction)+_startPosition);
		}
	}

}
