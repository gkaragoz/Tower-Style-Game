using UnityEngine;

namespace GK {

	public class PlayerFlyChecker : MonoBehaviour {

		[SerializeField]
		private float _groundCheckThreshold = 0.1f;
		[SerializeField]
		private Transform _groundCheckPivotTransform = null;
		[SerializeField]
		private float _groundCheckRayDistance = 0.1f;
		[SerializeField]
		private LayerMask _groundCheckLayerMask = 0;

		[Header("Debug")]
		[SerializeField]
		[Utils.ReadOnly]
		private bool _isFalling;
		[SerializeField]
		[Utils.ReadOnly]
		private bool _isGrounded;

		private Rigidbody2D _rb2D;

		public bool IsFalling {
			get {
				return _isFalling;
			}
			set {
				_isFalling = value;
				//Debug.Log("_isFalling Set as: " + _isFalling);
			}
		}

		public bool IsGrounded {
			get {
				return _isGrounded;
			} set {
				_isGrounded = value;
				Debug.Log("_isGrounded Set as: " + _isGrounded);
			}
		}

		private void Awake() {
			_rb2D = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate() {
			CheckIsFalling();
			CheckIsGrounded();
		}

		private void CheckIsFalling() {
			if (_rb2D.velocity.y < 0) {
				IsFalling = true;
			} else {
				IsFalling = false;
			}
		}

		private void CheckIsGrounded() {
			// Threshold value because of latency of sit on ground changing.
			if (_rb2D.velocity.magnitude <= _groundCheckThreshold) {
				// Checking via raycast because on peek position of my velocity is 0 at single frame.
				if (DoubleCheckIsGroundedViaRaycast()) {
					IsGrounded = true;
				} else {
					IsGrounded = false;
				}
			} else {
				IsGrounded = false;
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
