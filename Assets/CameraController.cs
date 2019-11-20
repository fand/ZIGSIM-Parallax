using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _positionScale = Vector3.one;
    [SerializeField] Vector3 _rotationScale = Vector3.one;
    Vector3 _originalPos;
    Quaternion _originalRot;
    OscServer _server;
    [SerializeField] int _oscPort = 3333;

    void Start()
    {
        _originalPos = transform.position;
        _originalRot = transform.rotation;

        _server = new OscServer(_oscPort);
        Debug.Log("inti!");
        _server.MessageDispatcher.AddCallback(
            "",
            (string address, OscDataHandle data) =>
            {
                Debug.Log("receive!'");
                Debug.Log(address);
            }
        );
    }

    void OnDestroy()
    {
        _server.Dispose();
        Debug.Log("disposed!");
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = _originalPos + new Vector3(
            pos.x * _positionScale.x,
            pos.y * _positionScale.y,
            pos.z * _positionScale.z
        );
    }

    public void SetRotation(Vector4 quat)
    {
        var rot = new Quaternion(quat.x, quat.y, quat.z, quat.w).eulerAngles;
        transform.rotation = Quaternion.Euler(
            rot.x * _rotationScale.x,
            rot.y * _rotationScale.y,
            rot.z * _rotationScale.z
        ) * _originalRot;
    }
}
