using System;
using UnityEngine;

namespace GK {

	public class PlayerMotor : MonoBehaviour {

		[SerializeField]
		private float _baseJumpForce = 1f;
		[SerializeField]
		private ForceMode2D _forceMode2D = ForceMode2D.Impulse;

		private Rigidbody2D _rb2D = null;

		private void Start() {
			_rb2D = GetComponent<Rigidbody2D>();
		}

		public void Jump(Vector2 direction, float selectedInputPower) {
			_rb2D.AddForce(direction * _baseJumpForce * selectedInputPower, _forceMode2D);
		}

	}

}
