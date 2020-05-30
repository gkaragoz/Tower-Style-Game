using UnityEngine;

namespace GK {

	public class Scaler : MonoBehaviour {
		
		[SerializeField]
		private float _scaleAmount = 1.1f;
		[SerializeField]
		private float _scaleSpeed = 0.1f;

		private void Awake() {
			LeanTween.scale(this.gameObject, Vector3.one * _scaleAmount, _scaleSpeed).setEase(LeanTweenType.linear).setLoopPingPong().setIgnoreTimeScale(true);
		}

	}

}
