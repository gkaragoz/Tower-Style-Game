using UnityEngine;

namespace GK {

	public class PanelInTween : MonoBehaviour {

		[SerializeField]
		private float openingDelay = 0f;
		[SerializeField]
		private float closingDelay = 0f;
		[SerializeField]
		private RectTransform _targetTween = null;
		[SerializeField]
		private float _scaleSpeed = 0.3f;

		public void Open() {
			if (LeanTween.isTweening(_targetTween)) {
				return;
			}

			// *********** Main Window **********
			// Scale the whole window in
			_targetTween.localScale = Vector3.zero;
			LeanTween.scale(_targetTween, Vector3.one, _scaleSpeed).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true).setDelay(openingDelay);
		}

		public void Close() {
			if (LeanTween.isTweening(_targetTween)) {
				return;
			}

			LeanTween.scale(_targetTween, Vector3.zero, _scaleSpeed).setEase(LeanTweenType.easeInBack).setIgnoreTimeScale(true).setDelay(closingDelay);
		}

	}

}
