using UnityEngine;

namespace GK {

	public class PlayerMotor : MonoBehaviour {

		[SerializeField]
		private float _jumpForce = 1f;
		[SerializeField]
		private ForceMode2D _forceMode2D = ForceMode2D.Impulse;

		private Rigidbody2D _rb2D = null;
		private InputManager _inputManager = null;

		private void Awake() {
			_rb2D = GetComponent<Rigidbody2D>();
			_inputManager = GetComponent<InputManager>();

			_inputManager.OnInputBegin += OnInputBegin;
			_inputManager.OnInputDragging += OnInputDragging;
			_inputManager.OnInputEnd += OnInputEnd;
		}

		private void OnInputBegin(Vector2 startPosition) {
			//throw new NotImplementedException();
		}

		private void OnInputDragging(Vector2 draggingPosition, Vector2 direction) {
			//throw new NotImplementedException();
		}

		private void OnInputEnd(Vector2 endPosition, Vector2 direction) {
			_rb2D.AddForce(direction * _jumpForce, _forceMode2D);
		}

	}

}
