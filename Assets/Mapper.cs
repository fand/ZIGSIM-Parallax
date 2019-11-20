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
        Debug.Log(GetUV(_mapperTopLeft));
        Debug.Log(GetUV(_mapperTopRight));

        _material.SetVector("_uvTopLeft", GetUV(_mapperTopLeft));
        _material.SetVector("_uvTopRight", GetUV(_mapperTopRight));
        _material.SetVector("_uvBottomLeft", GetUV(_mapperBottomLeft));
        _material.SetVector("_uvBottomRight", GetUV(_mapperBottomRight));
    }

    Vector2 GetUV(Transform target)
    {
        var p = _sceneCamera.WorldToScreenPoint(target.position);
        return new Vector2(p.x / Screen.width,  p.y / Screen.height);
    }
}
