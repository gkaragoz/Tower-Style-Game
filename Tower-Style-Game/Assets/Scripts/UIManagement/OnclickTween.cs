using UnityEngine;
using UnityEngine.UI;

namespace GK {

	[RequireComponent(typeof(Button))]
	public class OnclickTween : MonoBehaviour {

		[SerializeField]
		private RectTransform _targetTween = null;
		[SerializeField]
		private float _scaleAmount = 1.1f;
		[SerializeField]
		private float _scaleSpeed = 0.1f;

		private Button _button;

		private void Awake() {
			_button = GetComponent<Button>();

			_button.onClick.AddListener(() => {
				if (LeanTween.isTweening(_targetTween)) {
					return;
				}

				// Pop size of button
				LeanTween.size(_targetTween, _targetTween.sizeDelta * _scaleAmount, _scaleSpeed).setEaseInOutCirc().setRepeat(2).setLoopPingPong().setIgnoreTimeScale(true);
			});
		}

	}

}
