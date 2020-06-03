using UnityEngine;

namespace GK {

	public class Rotator : MonoBehaviour {

		[SerializeField]
		private float _rotationSpeedInSeconds = 0.3f;

		private void Awake() {
			DoRotate();
		}

		public void DoRotate() {
			this.transform.rotation = Quaternion.Euler(Vector3.up * -45f);
			LeanTween.rotate(this.gameObject, Vector3.up * 45f, _rotationSpeedInSeconds).setLoopPingPong();
		}

		public void StopRotate() {
			LeanTween.cancel(this.gameObject);
		}
		
	}

}
