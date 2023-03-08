using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    private Material _material;

    [SerializeField]
    private Color _color1;
    [SerializeField]
    private Color _color2;

    void Start()
    {
        _material = new Material(GetComponent<Skybox>().material);
        GetComponent<Skybox>().material = _material;
    }

    void Update()
    {
        _material.SetColor("_Color2", Color.Lerp(_color1, _color2, Timeline.Current));
    }

}
