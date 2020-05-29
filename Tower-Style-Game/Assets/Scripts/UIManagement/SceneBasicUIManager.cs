using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK;
using System;
using TMPro;

namespace GY {
    public class SceneBasicUIManager : MonoBehaviour {
        [SerializeField]
        private TextMeshProUGUI _coinText =null;
        [SerializeField]

        private TextMeshProUGUI _doubleJumpText = null;
        [SerializeField]
        private TextMeshProUGUI _armorText = null;



        public void UpdateJump(bool input) {
            _doubleJumpText.text = "Double Jump : " +input ;

        }
        public void UpdateGold(int goldCount) {       
            _coinText.text = "Gold : " + goldCount;

        }

        public void UpdateArmor() {
            _armorText.text = "Has Armor : " + PlayerController.instance.HasArmor;
        }
    }
}