using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePlatform : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem VFXEndStar01;
    [SerializeField]
    private ParticleSystem VFXEndStar02;

    public void EndGameVFX()
    {
        var seq = LeanTween.sequence();
        seq.append(LeanTween.delayedCall(0f, () => {
            if (VFXEndStar01 != null)
            {
                VFXEndStar01.Play();
            }
        }));
        seq.append(LeanTween.delayedCall(2f, () => {
            if (VFXEndStar02 != null)
            {
                VFXEndStar02.Play();
            }
        }));
        seq.append(LeanTween.delayedCall(0f, () => {
            if (VFXEndStar01 != null)
            {
                VFXEndStar01.Play();
            }
        }));
        seq.append(LeanTween.delayedCall(2f, () => {
            if (VFXEndStar02 != null)
            {
                VFXEndStar02.Play();
            }
        }));
    }
}
