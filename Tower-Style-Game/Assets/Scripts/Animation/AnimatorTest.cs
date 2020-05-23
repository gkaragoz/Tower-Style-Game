using UnityEngine;
using UnityEngine.UI;

namespace GY{

public class AnimatorTest : MonoBehaviour
 {
        public Slider slider;
        public Animator anim;

        private void Update() {
            anim.SetFloat("Velocity",slider.value);
        }
    }
}