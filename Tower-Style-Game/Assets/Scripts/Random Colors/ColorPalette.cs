using UnityEngine;

namespace GK {

	public class ColorPalette : MonoBehaviour {

		#region Singleton

		public static ColorPalette instance;
		private void Awake() {
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy(gameObject);
		}

		#endregion

		[SerializeField]
		private Color[] _colors = new Color[0];

		public Color GetRandomColor() {
			return _colors[Random.Range(0, _colors.Length)];
		}

	}

}
