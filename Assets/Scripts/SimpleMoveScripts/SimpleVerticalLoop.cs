using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVerticalLoop : MonoBehaviour
{
    [SerializeField] float _targetIncrease;
    [SerializeField] float _speed;
    private List<Transform> onPlatformObjects = new List<Transform>();
    private float oldY = 0;

    void Start() => transform.DOMoveY((transform.position.y + _targetIncrease), Mathf.Abs(_targetIncrease) / _speed)
                             .SetLoops(-1, LoopType.Yoyo)
                             .SetEase(Ease.Linear)
                             .OnUpdate(() =>
                             {
                                 foreach (var onPlatformObject in onPlatformObjects)
                                 {
                                     onPlatformObject.position += Vector3.up * (transform.position.y - oldY);
                                 }
                                 oldY = transform.position.y;
                             });

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!onPlatformObjects.Contains(collision.collider.transform))
            onPlatformObjects.Add(collision.collider.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (onPlatformObjects.Contains(collision.collider.transform))
            onPlatformObjects.Remove(collision.collider.transform);
    }
}
