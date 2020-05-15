using UnityEngine;

namespace GK {
	
	[RequireComponent(typeof(PlayerMotor))]
	public class PlayerController : MonoBehaviour {

		private PlayerMotor _playerMotor;

		private void Start() {
			_playerMotor = GetComponent<PlayerMotor>();

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
			_playerMotor.Jump(direction, selectedInputPower);
		}

	}

}
