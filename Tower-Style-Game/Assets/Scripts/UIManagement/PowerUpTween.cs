using UnityEngine;
using UnityEngine.UI;

namespace GK {

	public class PowerUpTween : MonoBehaviour {

		[SerializeField]
		private Image _image = null;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Space)) {
				GetPowerUp();
			}
		}

		public void GetPowerUp() {
			Vector3 screenPos = Vector3.zero;

			_image.rectTransform.anchoredPosition = screenPos;
			_image.gameObject.SetActive(true);
		}

	}

}
