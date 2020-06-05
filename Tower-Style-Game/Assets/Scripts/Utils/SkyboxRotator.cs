using UnityEngine;

namespace GK {

	public class SkyboxRotator : MonoBehaviour {

		[SerializeField]
		private float _rotationSpeed = 13f;

		private void Update() {
			RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotationSpeed);
		}

	}

}
