using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHorizontalLoop : MonoBehaviour
{
    [SerializeField] float _targetIncrease;
    [SerializeField] float _speed;
    private List<Transform> onPlatformObjects = new List<Transform>();
    private float oldX = 0;

    void Start() => transform.DOMoveX((transform.position.x + _targetIncrease), Mathf.Abs(_targetIncrease) / _speed)
                             .SetLoops(-1, LoopType.Yoyo)
                             .SetEase(Ease.Linear)
                             .OnUpdate(() =>
                             {

                                 foreach (var onPlatformObject in onPlatformObjects)
                                 {
                                     onPlatformObject.position += Vector3.right * (transform.position.x - oldX);
                                 }
                                 oldX = transform.position.x;
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
