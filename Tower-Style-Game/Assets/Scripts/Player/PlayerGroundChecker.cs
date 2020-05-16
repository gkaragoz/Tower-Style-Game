using System;
using UnityEngine;

namespace GK {

	public class PlayerGroundChecker : MonoBehaviour {

		public Action OnGrounded;
		public Action OnPeeked;

		[SerializeField]
		private float _groundCheckThreshold;
		[SerializeField]
		private Transform _groundCheckPivotTransform = null;
		[SerializeField]
		private float _groundCheckRayDistance = 0.1f;
		[SerializeField]
		private LayerMask _groundCheckLayerMask = 0;

		[Header("Debug")]
		[SerializeField]
		[Utils.ReadOnly]
		private bool _isGrounded;
		[SerializeField]
		[Utils.ReadOnly]
		private bool _isPeeked = false;

		private Rigidbody2D _rb2D;
		private PlayerMotor _playerMotor;

		public bool IsGrounded {
			get {
				return _isGrounded;
			} set {
				_isGrounded = value;
			}
		}


		private void Awake() {
			_rb2D = GetComponent<Rigidbody2D>();
			_playerMotor = GetComponent<PlayerMotor>();

			_playerMotor.OnJumped += OnJumped;
		}

		private void OnJumped() {
			_isPeeked = false;
			_isGrounded = false;
		}

		private void FixedUpdate() {
			CheckIsGrounded();
		}

		private void CheckIsGrounded() {
			if (_rb2D.velocity.y < 0 && _isPeeked == false) {
				_isPeeked = true;

				OnPeeked?.Invoke();
			}

			if (_rb2D.velocity.y <= 0 && _isGrounded == false) {
				if (DoubleCheckIsGroundedViaRaycast()) {
					IsGrounded = true;
					OnGrounded?.Invoke();
				}
			}
		}

		private bool DoubleCheckIsGroundedViaRaycast() {
			if (Physics2D.Raycast(_groundCheckPivotTransform.position, Vector2.down, _groundCheckRayDistance, _groundCheckLayerMask)) {
				return true;
			} else {
				return false;
			}
		}

		private void OnDrawGizmos() {
			if (_groundCheckPivotTransform == null) {
				return;
			}

			if (IsGrounded) {
				Gizmos.color = Color.yellow;
			} else {
				Gizmos.color = Color.red;
			}

			Gizmos.DrawLine(_groundCheckPivotTransform.position, _groundCheckPivotTransform.position + (Vector3.down * _groundCheckRayDistance));
		}

	}

}
