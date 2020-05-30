using UnityEngine;

namespace GK {

	public class Shaker : MonoBehaviour {

		[SerializeField]
		private Vector3 _shakeDirection = Vector3.one;

		[SerializeField]
		private float _jumpIter = 9.5f;
		[SerializeField]
		private float _heightMultiplier = 0.3f;
		[SerializeField]
		private float _heightDegreeMultiplier = 0.2f;
		[SerializeField]
		private float _shakePeriodTime = 0.42f;
		[SerializeField]
		private float _dropOffTime = 1.6f;

		public void DoShake() {
			float height = Mathf.PerlinNoise(_jumpIter, 0f) * 10f;
			height = height * height * _heightMultiplier;
			float shakeAmt = height * _heightDegreeMultiplier; // the degrees to shake the camera
			float shakePeriodTime = _shakePeriodTime; // The period of each shake
			float dropOffTime = _dropOffTime; // How long it takes the shaking to settle down to nothing

			LTDescr shakeTweenRight = LeanTween.rotateAroundLocal(gameObject, _shakeDirection, shakeAmt, shakePeriodTime)
			.setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
			.setLoopClamp()
			.setRepeat(-1);

			//// Slow the camera shake down to zero
			//LeanTween.value(gameObject, shakeAmt, 0f, dropOffTime).setOnUpdate(
			//	(float val) => {
			//		shakeTween.setTo(direction * val);
			//	}
			//).setEase(LeanTweenType.easeOutQuad);
		}

		public void StopShake() {
			LeanTween.cancel(this.gameObject);
		}

	}

}
