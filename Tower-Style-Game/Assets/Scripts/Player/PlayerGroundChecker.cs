using System;
using UnityEngine;

namespace GK {

	public class PlayerGroundChecker : MonoBehaviour {

		public Action OnGrounded;

		[SerializeField]
		private float _groundCheckThreshold = 0.1f;
		[SerializeField]
		private Transform _groundCheckPivotTransform = null;
		[SerializeField]
		private float _groundCheckRayDistance = 0.1f;
		[SerializeField]
		private LayerMask _groundCheckLayerMask = 0;

		[SerializeField]
		private float _slideCheckThreshold = 0.1f;

		[Header("Debug")]
		[SerializeField]
		[Utils.ReadOnly]
		private bool _isGrounded;
		[SerializeField]
		[Utils.ReadOnly]
		private bool _isSliding;

		private Rigidbody2D _rb2D;

		public bool IsGrounded {
			get {
				return _isGrounded;
			} set {
				_isGrounded = value;
			}
		}

		public bool IsSliding {
			get {
				return _isSliding;
			} set {
				_isSliding = value;
			}
		}

		private void Awake() {
			_rb2D = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate() {
			CheckIsSliding();

			CheckIsGrounded();
		}

		private void CheckIsSliding() {
			if (Mathf.Abs(_rb2D.velocity.x) >= _slideCheckThreshold) {
				IsSliding = true;
			} else {
				IsSliding = false;
			}
		}

		private void CheckIsGrounded() {
			if (Mathf.Abs(_rb2D.velocity.y) <= _groundCheckThreshold && IsSliding == false) {
				IsGrounded = true;

				OnGrounded?.Invoke();
			} else {
				IsGrounded = false;
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
