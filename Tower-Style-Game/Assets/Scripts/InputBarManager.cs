using UnityEngine;

namespace GK {

	public class InputBarManager : MonoBehaviour {

		[SerializeField]
		private InputBarSlot[] _slots = null;
		[SerializeField]
		private RectTransform _parentTransform = null;
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

		private void Awake() {
			Init();
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

		private int currentindex;

		private void CheckCursorIndex() {
			currentindex = (int)(_cursorTransform.anchoredPosition.x / _slotPerDistance);
		}

		private void Play() {
			_cursorTransform.anchoredPosition = _cursorStartPosition;

			LeanTween.moveLocalX(_cursorTransform.gameObject, _cursorEndPosition.x, _cursorSpeed).setLoopPingPong();
		}

		private void Stop() {
			LeanTween.cancel(_cursorTransform.gameObject);
			Debug.Log(currentindex);
		}


	}

}
