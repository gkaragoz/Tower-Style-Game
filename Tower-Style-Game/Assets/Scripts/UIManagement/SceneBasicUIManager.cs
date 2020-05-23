using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GY {
    public class SceneBasicUIManager : MonoBehaviour {
        private int _goldCount=0;
        [SerializeField]
        private Text _coinText=null;
        public void UpdateGold(int goldCount) {
            _coinText.text = "Gold : " + goldCount;
        }
    }
}