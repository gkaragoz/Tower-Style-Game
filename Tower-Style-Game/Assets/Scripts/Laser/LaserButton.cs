﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GY{

public class LaserButton : MonoBehaviour
 {
        [SerializeField]
        private LaserHolder _laserHolder=null;

        public void CloseLaser() {
            _laserHolder.CloseLaser();
        }
 }
}