using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 HorizontalLimits;

    private Transform _playerTransform;
    private Vector3 _playerOffset;

    void Awake()
    {
        _playerTransform = FindAnyObjectByType<PlayerController>().transform;
        _playerOffset = _playerTransform.position - transform.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(Mathf.Clamp(_playerTransform.position.x - _playerOffset.x, HorizontalLimits.x, HorizontalLimits.y),
                                                         _playerTransform.position.y - _playerOffset.y,
                                                         -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition, .75f);
    }
}
