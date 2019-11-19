using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapper : MonoBehaviour
{
    [SerializeField] Camera _sceneCamera;
    [SerializeField] Transform _mapperTopLeft;
    [SerializeField] Transform _mapperTopRight;
    [SerializeField] Transform _mapperBottomLeft;
    [SerializeField] Transform _mapperBottomRight;
    [SerializeField] Material _material;

    void Update()
    {

        Vector3 screenPos = _sceneCamera.WorldToScreenPoint(_mapperTopLeft.position);
        _material.SetVector(0, screenPos);
    }
}
