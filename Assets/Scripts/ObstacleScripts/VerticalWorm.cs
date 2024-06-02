using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWorm : ObstacleController
{
    [SerializeField] float _targetIncrease;
    [SerializeField] float _delay;
    [SerializeField] float _speed;

    private void Start() => transform.DOMoveY((transform.position.y + _targetIncrease), Mathf.Abs(_targetIncrease) / _speed)
                             .SetLoops(-1, LoopType.Yoyo)
                             .SetEase(Ease.Linear)
                             .SetDelay(_delay);
}
