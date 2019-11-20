using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

public class CameraController : MonoBehaviour
{
    [SerializeField] int _oscPort = 3333;
    [SerializeField] Vector3 _positionScale = Vector3.one;
    [SerializeField] Vector3 _rotationScale = Vector3.one;
    [SerializeField, Range(0, 0.99f)] float _smoothing = 0.3f;
    OscServer _server;
    Vector3 _originalPos;
    Quaternion _originalRot;
    Vector3 _pos;
    Quaternion _rot;

    void Start()
    {
        _originalPos = transform.position;
        _originalRot = transform.rotation;

        _server = new OscServer(_oscPort);

        _server.MessageDispatcher.AddCallback("", (string address, OscDataHandle data) =>
        {
            if (address.Contains("arkitposition"))
            {
                SetPosition(new Vector3(
                    data.GetElementAsFloat(0),
                    data.GetElementAsFloat(1),
                    data.GetElementAsFloat(2)
                ));
            }
            if (address.Contains("arkitrotation"))
            {
                SetRotation(new Vector4(
                    data.GetElementAsFloat(0),
                    data.GetElementAsFloat(1),
                    data.GetElementAsFloat(2),
                    data.GetElementAsFloat(3)
                ));
            }
        });
    }

    void Update()
    {
        var t = 1 - _smoothing;
        transform.position = Vector3.Lerp(transform.position, _pos, t);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rot, t);
    }

    void OnDestroy()
    {
        _server.Dispose();
    }

    void SetPosition(Vector3 pos)
    {
        _pos = _originalPos + new Vector3(
            pos.x * _positionScale.x,
            pos.y * _positionScale.y,
            pos.z * _positionScale.z
        );
    }

    void SetRotation(Vector4 quat)
    {
        var rot = new Quaternion(quat.x, quat.y, quat.z, quat.w).eulerAngles;
        _rot = Quaternion.Euler(
            rot.x * _rotationScale.x,
            rot.y * _rotationScale.y,
            rot.z * _rotationScale.z
        ) * _originalRot;
    }
}
