using UnityEngine;
using UnityEngine.UI;

namespace GK {

	public class UISettings : MonoBehaviour {

		[SerializeField]
		private Button _button = null;

		[SerializeField]
		private RectTransform _content = null;

		[SerializeField]
		private float _scaleSpeed = 0.1f;

		[SerializeField]
		[Utils.ReadOnly]
		private bool _hasOpened = false;

		private void Awake() {
			_button = GetComponent<Button>();

			_content.localScale = Vector3.zero;

			_button.onClick.AddListener(() => {
				if (LeanTween.isTweening(_content)) {
					return;
				}

				// For settings only.
				if (_content != null) {
					_hasOpened = !_hasOpened;

					if (_hasOpened) {
						LeanTween.scale(_content, Vector2.one, _scaleSpeed).setEaseInBack().setIgnoreTimeScale(true);
					} else {
						LeanTween.scale(_content, Vector2.zero, _scaleSpeed).setEaseOutBack().setIgnoreTimeScale(true);
					}
				}
			});

			
		}

	}

}
