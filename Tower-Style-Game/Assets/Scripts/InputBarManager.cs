using System;
using UnityEngine;

namespace GK {

	public class InputBarManager : MonoBehaviour {

		[SerializeField]
		private InputBarSlot[] _slots = null;
		[SerializeField]
		private RectTransform _parentTransform = null;
		[SerializeField]
		private RectTransform _inputSelectorTransform = null;
		[SerializeField]
		private float _cursorSpeedByTime = 1f;

		[Header("Debug")]
		[SerializeField]
		private float _slotPerDistance;
		[SerializeField]
		private float _containerWidth;
		[SerializeField]
		private Vector2 _inputSelectorStartPosition;
		[SerializeField]
		private Vector2 _inputSelectorEndPosition;
		[SerializeField]
		private int _selectedInputIndex;

		private void Awake() {
			Init();
		}

		private void Init() {
			if (_slots.Length <= 0) {
				Debug.LogError("Bug is here!");
			}

			_containerWidth = _parentTransform.rect.width;

			_inputSelectorStartPosition = new Vector2(0, _inputSelectorTransform.anchoredPosition.y);
			_inputSelectorEndPosition = new Vector2(_containerWidth * 0.5f, _inputSelectorTransform.anchoredPosition.y);

			_slotPerDistance = _containerWidth / _slots.Length;
		}

		private void SetInputIndex() {
			_selectedInputIndex = (int)(_inputSelectorTransform.anchoredPosition.x / _slotPerDistance);
		}

		public float GetSelectedPowerValue() {
			return _slots[_selectedInputIndex].Value;
		}

		public void Play() {
			_inputSelectorTransform.anchoredPosition = _inputSelectorStartPosition;

			LeanTween.moveLocalX(_inputSelectorTransform.gameObject, _inputSelectorEndPosition.x, _cursorSpeedByTime).setLoopPingPong();
		}

		public void Stop() {
			LeanTween.cancel(_inputSelectorTransform.gameObject);
			SetInputIndex();
		}

	}

}
