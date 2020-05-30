using System.Collections;
using UnityEngine;

namespace GK {

	public class PlatformDestroyer : MonoBehaviour {

		[SerializeField]
		private float _destroyTime = 3f;
		[SerializeField]
		private float _scaleYShrinkAmount = 0.75f;
		[SerializeField]
		private float _completeShrinkSpeed = 0.3f;
		[SerializeField]
		private float _scaleYShrinkSpeed = 0.5f;

		private Shaker[] _shakers = null;

		private void Awake() {
			_shakers = GetComponentsInChildren<Shaker>();
		}

		private IEnumerator IStartShake() {
			foreach (Shaker shaker in _shakers) {
				shaker.DoShake();
			}

			yield return new WaitForSeconds(_destroyTime);

			DoShrink();
		}

		private void DoShrink() {
			var seq = LeanTween.sequence();

			seq.append(LeanTween.scaleY(this.gameObject, _scaleYShrinkAmount, _scaleYShrinkSpeed).setEaseOutQuad());

			foreach (Shaker shaker in _shakers) {
				shaker.StopShake();
			}

			seq.append(LeanTween.scaleX(this.gameObject, 1.1f, _scaleYShrinkSpeed * 0.2f).setEaseOutQuad());

			seq.append(LeanTween.scale(this.gameObject, Vector3.zero, _completeShrinkSpeed).setEaseOutQuad());
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				StartShake();
			}
		}

		public void StartShake() {
			StartCoroutine(IStartShake());
		}
		
	}

}
