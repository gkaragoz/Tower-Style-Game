using UnityEngine;

namespace GK {

	public class Rotator : MonoBehaviour {

		[SerializeField]
		private float _rotationSpeedInSeconds = 0.3f;

		private void Awake() {
			DoRotate();
		}

		public void DoRotate() {
			LeanTween.rotateAroundLocal(this.gameObject, Vector3.up, 360, _rotationSpeedInSeconds).setLoopClamp();
		}

		public void StopRotate() {
			LeanTween.cancel(this.gameObject);
		}
		
	}

}
