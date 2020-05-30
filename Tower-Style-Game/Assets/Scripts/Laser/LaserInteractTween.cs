using UnityEngine;

namespace GK {

	public class LaserInteractTween : MonoBehaviour {

		[SerializeField]
		private GameObject _target = null;
		[SerializeField]
		private float _rotateTo = 120;
		[SerializeField]
		private float _rotationSpeed = 1f;

		public void Interact() {
			if (LeanTween.isTweening(_target)) {
				return;
			}

			LeanTween.rotateAroundLocal(_target, Vector3.forward, _rotateTo, _rotationSpeed);
		}
		
	}

}
