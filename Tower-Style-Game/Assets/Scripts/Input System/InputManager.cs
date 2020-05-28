using System;
using UnityEngine;
using GY;
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


		///TO DO Delete UI MANAGER
		///
		[SerializeField]
		private SceneBasicUIManager sceneManager;

		[SerializeField]
		[Utils.ReadOnly]
		private int _jumpTimes;

		[SerializeField]
		private bool _doubleJump;
		[SerializeField]

		private bool _hasArmor;


		[SerializeField]
		private Camera _camera = null;
		[SerializeField]
		private bool _clampInputActive = false;

		public Action<Vector2> OnInputBegin;
		public Action<Vector2, Vector2> OnInputDragging;
		public Action<Vector2, Vector2, float> OnInputEnd;

		[SerializeField]
		private float _clampedInputMagnitude = 2.5f;

		private Vector2 _startPosition = Vector2.zero;
		private Vector2 _endPosition = Vector2.zero;
		private Vector2 _currentPosition = Vector2.zero;

		private Vector2 _direction = Vector2.zero;

		private float _endMagnitude;

		public float ClampedInputMagnitude {
			get {
				return _clampedInputMagnitude;
			}
		}


		public bool HasArmor {
			get { return _hasArmor; }
			set { _hasArmor = value; }
		}
		public int JumpTimes {
			get { return _jumpTimes; }
			set { _jumpTimes = value; }
		}

		public bool DoubleJump {
			get { return _doubleJump; }
			set { _doubleJump = value; }
		}


		public Vector2 Direction {
			get {
				return _direction;
			}
		}

		private void Update() {
			if (JumpTimes>0) {

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

					if (_clampInputActive) {
						_direction = _direction.normalized * Mathf.Clamp(_direction.magnitude, 0f, _clampedInputMagnitude);
					}

					OnInputDragging?.Invoke(_currentPosition, InputDirectionModifier.UserDirectionVector(_direction));
				}

				if (Input.GetMouseButtonUp(0)) {
					Vector2 mousePos = Input.mousePosition;
					_endPosition = _camera.ScreenToWorldPoint(mousePos);

					_direction = _startPosition - _currentPosition;
					_endMagnitude = _direction.magnitude;

					if (_clampInputActive) {
						_endMagnitude = Mathf.Clamp(_direction.magnitude, 0f, _clampedInputMagnitude);
					}

					OnInputEnd?.Invoke(
						_endPosition,
						InputDirectionModifier.UserDirectionVector(_direction).normalized,
						_endMagnitude);

					ResetInputs();

					if (JumpTimes==1) {
						DoubleJump = false;
						JumpTimes--;
						sceneManager.UpdateJump(DoubleJump);


					} else {
						DoubleJump = true;
						JumpTimes--;
					}
				}
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
