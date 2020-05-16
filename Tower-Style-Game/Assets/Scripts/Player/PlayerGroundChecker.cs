using System;
using UnityEngine;

namespace GK {

	public class PlayerGroundChecker : MonoBehaviour {

		public Action OnGrounded;
		public Action OnPeeked;
		public Action OnIsFalling;

		[SerializeField]
		private float _groundCheckThreshold;
		[SerializeField]
		private Transform _groundCheckPivotTransform = null;
		[SerializeField]
		private float _groundCheckRayDistance = 0.1f;
		[SerializeField]
		private LayerMask _groundCheckLayerMask = 0;
		[SerializeField]
		private LayerMask _platformCheckLayerMask = 0;

		[Header("Debug")]
		[SerializeField]
		[Utils.ReadOnly]
		private bool _isGrounded;
		[SerializeField]
		[Utils.ReadOnly]
		private bool _isFalling;
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

		public bool IsFalling {
			get {
				return _isFalling;
			} set {
				_isFalling = value;
			}
		}

		private void Awake() {
			_rb2D = GetComponent<Rigidbody2D>();
			_playerMotor = GetComponent<PlayerMotor>();

			_playerMotor.OnJumped += OnJumped;
		}

		private void OnJumped() {
			ResetValues();
		}

		private void FixedUpdate() {
			CheckIsFalling();
			CheckIsGrounded();
		}

		private void CheckIsFalling() {
			if (_rb2D.velocity.y < 0 && _isFalling == false) {
				IsFalling = true;

				ResetValues();

				OnIsFalling?.Invoke();
			}
		}

		private void CheckIsGrounded() {
			if (_rb2D.velocity.y < 0 && _isPeeked == false) {
				_isPeeked = true;

				OnPeeked?.Invoke();
			}

			if (_rb2D.velocity.y <= 0 && _isGrounded == false) {
				if (DoubleCheckIsGroundedViaRaycast()) {
					IsGrounded = true;

					// TODO
					// REFACTOR
					RaycastHit2D hit = Physics2D.Raycast(_groundCheckPivotTransform.position, Vector2.down, _groundCheckRayDistance, _platformCheckLayerMask);
					if (hit) {
						hit.transform.gameObject.GetComponent<IPlatform>().DestroyPlatform(OnPlatformDestroyed);
					}

					OnGrounded?.Invoke();
				}
			}
		}

		private void OnPlatformDestroyed() {
			IsFalling = false;
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

		public void ResetValues() {
			_isPeeked = false;
			_isGrounded = false;
		}

	}

}
