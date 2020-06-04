using GY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieOnVFXTrigger : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        PlayerController.instance.Die();
    }
}
