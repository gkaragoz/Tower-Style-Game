using UnityEngine;

namespace GK {

	public class GhostPlatformOpenTween : MonoBehaviour {

		[SerializeField]
		private float _openSpeed = 0.5f;
		[SerializeField]
		private byte _targetAlpha = 107;

		private Renderer _renderer;
		private bool _isCompleted = false;

		private void Awake() {
			_renderer = GetComponent<Renderer>();
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			if (LeanTween.isTweening(this.gameObject)) {
				return;
			}
			if (_isCompleted) {
				return;
			}

			// You can re-use this block between calls rather than constructing a new one each time.
			var block = new MaterialPropertyBlock();

			// You can look up the property by ID instead of the string to be more efficient.
			LeanTween.value(this.gameObject, 0, _targetAlpha, _openSpeed).setOnUpdate((float newValue) => {
				Color32 color = Color.white;
				color.a = (byte)(newValue);
				block.SetColor("_BaseColor", color);

				// You can cache a reference to the renderer to avoid searching for it.
				_renderer.SetPropertyBlock(block);
			}).setOnComplete(() => {
				_isCompleted = true;
			});
		}

	}

}
