using UnityEngine;

namespace GK {

	public class MainMenuCloserTween : MonoBehaviour {

		[SerializeField]
		private RectTransform _market = null;
		[SerializeField]
		private RectTransform _ranking = null;
		[SerializeField]
		private RectTransform _watchAds = null;
		[SerializeField]
		private RectTransform _grpLevel = null;
		[SerializeField]
		private float _movementSpeed = 0.3f;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.A)) {
				Open();
			}
			if (Input.GetKeyDown(KeyCode.S)) {
				Close();
			}
		}

		public void Open() {
			LeanTween.move(_market, new Vector2(-470, -158), _movementSpeed).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true).setDelay(0.3f);
			LeanTween.move(_ranking, new Vector2(466, -154), _movementSpeed).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true).setDelay(0.3f);
			LeanTween.move(_watchAds, new Vector2(462, 292), _movementSpeed).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true).setDelay(0.3f);
			LeanTween.move(_grpLevel, new Vector2(0, 639), _movementSpeed).setEase(LeanTweenType.easeOutBack).setIgnoreTimeScale(true).setDelay(0.3f);
		}

		public void Close() {
			LeanTween.move(_market, new Vector2(-1000, -158), _movementSpeed).setEase(LeanTweenType.easeInBack).setIgnoreTimeScale(true);
			LeanTween.move(_ranking, new Vector2(1000, -154), _movementSpeed).setEase(LeanTweenType.easeInBack).setIgnoreTimeScale(true);
			LeanTween.move(_watchAds, new Vector2(1000, 292), _movementSpeed).setEase(LeanTweenType.easeInBack).setIgnoreTimeScale(true);
			LeanTween.move(_grpLevel, new Vector2(0, 1500), _movementSpeed).setEase(LeanTweenType.easeInBack).setIgnoreTimeScale(true);
		}

	}

}
