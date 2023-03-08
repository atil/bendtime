using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<FpsController>().ResetAt(_target);
        }
    }
}
