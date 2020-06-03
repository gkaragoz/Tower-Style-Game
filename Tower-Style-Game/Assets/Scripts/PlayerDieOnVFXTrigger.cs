using GY;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieOnVFXTrigger : MonoBehaviour
{
    bool isDead;
    private void OnParticleTrigger()
    {
        PlayerController.instance.Die();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (!isDead)
        {
            isDead = true;
            PlayerController.instance.Die();
        }

    }
}
