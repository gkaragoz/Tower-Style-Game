using UnityEngine;

namespace GK {

	public class RandomColor : MonoBehaviour {

		private void Start() {
			// You can re-use this block between calls rather than constructing a new one each time.
			var block = new MaterialPropertyBlock();

			// You can look up the property by ID instead of the string to be more efficient.
			block.SetColor("_BaseColor", ColorPalette.instance.GetRandomColor());

			// You can cache a reference to the renderer to avoid searching for it.
			GetComponent<Renderer>().SetPropertyBlock(block);
		}

	}

}
