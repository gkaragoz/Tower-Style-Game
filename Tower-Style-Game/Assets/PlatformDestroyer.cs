using System;
using System.Collections;
using UnityEngine;

namespace GK {

	public class PlatformDestroyer : MonoBehaviour,IPlatform {

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

		private IEnumerator IStartShake(Action onPlatformDestroyed) {
			foreach (Shaker shaker in _shakers) {
				shaker.DoShake();
			}

			yield return new WaitForSeconds(_destroyTime);

			DoShrink(onPlatformDestroyed);
		}

		private void DoShrink(Action onPlatformDestroyed) {
			var seq = LeanTween.sequence();

			seq.append(LeanTween.scaleY(this.gameObject, _scaleYShrinkAmount, _scaleYShrinkSpeed).setEaseOutQuad());

			foreach (Shaker shaker in _shakers) {
				shaker.StopShake();
			}

			seq.append(LeanTween.scaleX(this.gameObject, 1.1f, _scaleYShrinkSpeed * 0.2f).setEaseOutQuad());

			seq.append(LeanTween.scale(this.gameObject, Vector3.zero, _completeShrinkSpeed).setEaseOutQuad().setOnComplete(() => {
				onPlatformDestroyed();
				this.gameObject.SetActive(false);
			}));
		}

		public void DestroyPlatform(Action onPlatformDestroyed) {
			StartCoroutine(IStartShake(onPlatformDestroyed));
		}

	}

}
