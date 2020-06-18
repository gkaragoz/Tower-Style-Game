using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckRestart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(transform.gameObject, new Vector3(.9f, .9f, .9f), .5f).setLoopPingPong();
    }


}
