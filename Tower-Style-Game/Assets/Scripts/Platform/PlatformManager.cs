using GK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GY{

public class PlatformManager : MonoBehaviour
 {
        [SerializeField]
        [Utils.ReadOnly]
        private Platform[] _platformsArray;

        private void Start() {
            _platformsArray = GameObject.FindObjectsOfType<Platform>();
        }
    }
}