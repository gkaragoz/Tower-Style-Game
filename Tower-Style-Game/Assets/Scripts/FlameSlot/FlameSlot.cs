using UnityEngine;

namespace GK {

	public class FlameSlot : MonoBehaviour {

		[SerializeField]
		private float _colliderSpeed = 1f;
		[SerializeField]
		private float _colliderFrequency = 1f;
		[SerializeField]
		private BoxCollider2D _collider = null;

		private float _scaleTo = 1f;

		private void Awake() {
			_scaleTo = _collider.transform.localScale.x;
			_collider.transform.localScale = new Vector3(0, _collider.transform.localScale.y, _collider.transform.localScale.z);

			LeanTween.delayedCall(_colliderFrequency, () => {
				LeanTween.scaleX(_collider.gameObject, _scaleTo, _colliderSpeed).setFrom(0).setLoopPingPong().setRepeat(2);
			}).setRepeat(-1);
		}

	}

}
