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
		private Transform _groundCheckPivotRightTransform = null;
		[SerializeField]
		private Transform _groundCheckPivotLeftTransform = null;
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
					RaycastHit2D lefthit = Physics2D.Raycast(_groundCheckPivotLeftTransform.position, Vector2.down, _groundCheckRayDistance, _platformCheckLayerMask);
					RaycastHit2D rightHit = Physics2D.Raycast(_groundCheckPivotRightTransform.position, Vector2.down, _groundCheckRayDistance, _platformCheckLayerMask);
					if (rightHit) {
						rightHit.transform.gameObject.GetComponent<IPlatform>().DestroyPlatform(OnPlatformDestroyed);
					}else if (lefthit) {
						lefthit.transform.gameObject.GetComponent<IPlatform>().DestroyPlatform(OnPlatformDestroyed);
					}

					OnGrounded?.Invoke();
				}
			}
		}

		private void OnPlatformDestroyed() {
			IsFalling = false;
		}

		private bool DoubleCheckIsGroundedViaRaycast() {
			if (Physics2D.Raycast(_groundCheckPivotLeftTransform.position, Vector2.down, _groundCheckRayDistance, _groundCheckLayerMask)) {
				return true;
			} else if(Physics2D.Raycast(_groundCheckPivotRightTransform.position, Vector2.down, _groundCheckRayDistance, _groundCheckLayerMask)) {
				return true;
			} else {
				return false;
			}
		}

		private void OnDrawGizmos() {
			if (_groundCheckPivotLeftTransform == null) {
				return;
			}
			if (_groundCheckPivotRightTransform == null) {
				return;
			}


			if (IsGrounded) {
				Gizmos.color = Color.yellow;
			} else {
				Gizmos.color = Color.red;
			}

			Gizmos.DrawLine(_groundCheckPivotLeftTransform.position, _groundCheckPivotLeftTransform.position + (Vector3.down * _groundCheckRayDistance));
			Gizmos.DrawLine(_groundCheckPivotRightTransform.position, _groundCheckPivotRightTransform.position + (Vector3.down * _groundCheckRayDistance));

		}

		public void ResetValues() {
			_isPeeked = false;
			_isGrounded = false;
		}

	}

}
