using System;
using System.Collections;
using UnityEngine;

namespace GK {

	public class Platform : MonoBehaviour, IPlatform {

		[SerializeField]
		private float _destroyTime = 3f;

		public IEnumerator IDestroy(Action onDestroyed) {
			while (true) {
				yield return new WaitForSeconds(_destroyTime);

				onDestroyed();
				this.gameObject.SetActive(false);

				break;
			}
		}

		public void DestroyPlatform(Action onDestroyed) {
			StartCoroutine(IDestroy(onDestroyed));
		}

	}

}
