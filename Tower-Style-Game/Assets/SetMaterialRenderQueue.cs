using UnityEngine;

namespace GK {

	public class SetMaterialRenderQueue : MonoBehaviour {

		[System.Serializable]
		public class RQMaterial {
			public Material material;
			[Range(-1, 5000)]
			public int renderOrder = 2000;
		}

		[SerializeField]
		private RQMaterial[] _rqMaterials = null;

		private void Start() {
			foreach (RQMaterial rqMaterial in _rqMaterials) {
				rqMaterial.material.renderQueue = rqMaterial.renderOrder;
			}
		}

	}

}
