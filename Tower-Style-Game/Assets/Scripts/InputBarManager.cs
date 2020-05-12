using System;
using UnityEngine;

namespace GK {

	public class InputBarManager : MonoBehaviour {

		[SerializeField]
		private InputManager _inputManager = null;
		[SerializeField]
		private InputBarSlot[] _slots = null;
		[SerializeField]
		private RectTransform _parentTransform = null;
		[SerializeField]
		private CanvasGroup _parentCanvasGroup = null;
		[SerializeField]
		private RectTransform _cursorTransform = null;
		[SerializeField]
		private float _cursorSpeed = 1f;

		[Header("Debug")]
		[SerializeField]
		private float _slotPerDistance;
		[SerializeField]
		private float _containerWidth;
		[SerializeField]
		private Vector2 _cursorStartPosition;
		[SerializeField]
		private Vector2 _cursorEndPosition;
		[SerializeField]
		private int _selectedIndex;

		private void Awake() {
			Init();
			Hide();

			_inputManager.OnInputBegin += OnInputBegin;
			_inputManager.OnInputDragging += OnInputDragging;
			_inputManager.OnInputEnd += OnInputEnd;
		}

		private void OnInputBegin(Vector2 startPosition) {
			Show();
		}

		private void OnInputDragging(Vector2 draggingPosition, Vector2 direction) {
		}

		private void OnInputEnd(Vector2 endPosition, Vector2 direction) {
			Hide();
		}

		private void Init() {
			if (_slots.Length <= 0) {
				Debug.LogError("Bug is here!");
			}

			_containerWidth = _parentTransform.rect.width;

			_cursorStartPosition = new Vector2(0, _cursorTransform.anchoredPosition.y);
			_cursorEndPosition = new Vector2(_containerWidth * 0.5f, _cursorTransform.anchoredPosition.y);

			_slotPerDistance = _containerWidth / _slots.Length;
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.N)) {
				Play();
			}
			if (Input.GetKeyDown(KeyCode.M)) {
				Stop();
			}

			CheckCursorIndex();
		}

		private void Hide() {
			_parentCanvasGroup.alpha = 0;
		}

		private void Show() {
			_parentCanvasGroup.alpha = 1;
		}

		private void CheckCursorIndex() {
			_selectedIndex = (int)(_cursorTransform.anchoredPosition.x / _slotPerDistance);
		}

		public void Play() {
			_cursorTransform.anchoredPosition = _cursorStartPosition;

			LeanTween.moveLocalX(_cursorTransform.gameObject, _cursorEndPosition.x, _cursorSpeed).setLoopPingPong();
		}

		public void Stop() {
			LeanTween.cancel(_cursorTransform.gameObject);
		}


	}

}
