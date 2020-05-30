using UnityEngine;

namespace GK {

	public class Rotator : MonoBehaviour {

		[SerializeField]
		private float _rotationSpeedInSeconds = 0.3f;
		[SerializeField]
		private bool _setRandomDelay = true;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				DoRotate();
			}
			if (Input.GetKeyDown(KeyCode.A)) {
				StopRotate();
			}
		}

		public void DoRotate() {
			if (_setRandomDelay) {
				LeanTween.rotateAroundLocal(this.gameObject, Vector3.up, 360, _rotationSpeedInSeconds).setLoopClamp();
			}
		}

		public void StopRotate() {
			LeanTween.cancel(this.gameObject);
		}
		
	}

}
