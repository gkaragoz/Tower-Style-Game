using UnityEngine;

namespace GK {

	public class PlayerMotor : MonoBehaviour {

		[SerializeField]
		private float _baseJumpForce = 1f;
		[SerializeField]
		private float _clampedInputForce = 1f;
		[SerializeField]
		private ForceMode2D _forceMode2D = ForceMode2D.Impulse;

		private Rigidbody2D _rb2D = null;

		private void Start() {
			_rb2D = GetComponent<Rigidbody2D>();

			InputManager.instance.OnInputBegin += OnInputBegin;
			InputManager.instance.OnInputDragging += OnInputDragging;
			InputManager.instance.OnInputEnd += OnInputEnd;
		}

		private void OnInputBegin(Vector2 startPosition) {
			//throw new NotImplementedException();
		}

		private void OnInputDragging(Vector2 draggingPosition, Vector2 direction) {
			//throw new NotImplementedException();
		}

		private void OnInputEnd(Vector2 endPosition, Vector2 direction, float selectedInputPower) {
			_rb2D.AddForce(direction * _baseJumpForce * Mathf.Clamp(selectedInputPower, 0f, _clampedInputForce), _forceMode2D);
		}

	}

}
