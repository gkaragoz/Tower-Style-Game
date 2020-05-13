using UnityEngine;

namespace GK {

	public class InputBarSlot : MonoBehaviour {

		[SerializeField]
		private float _value = 1f;

		public float Value { get { return _value; } }

	}

}
