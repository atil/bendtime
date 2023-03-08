using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUnbendVolume : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            for (int i = 0; i < 10; i++)
            {
                Timeline.Unbend();
            }
        }
    }
}
