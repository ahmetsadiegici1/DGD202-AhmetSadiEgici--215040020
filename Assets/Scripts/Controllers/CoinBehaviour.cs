using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [HideInInspector] public Transform CoinCollectPoint;
    bool isCollected;
    public void Collect()
    {
        if (!isCollected)
        {
            isCollected = true;
            GetComponent<CircleCollider2D>().enabled = false;
            var pos = Camera.main.ScreenToWorldPoint(CoinCollectPoint.position);
            transform.DOMove(pos, .5f).SetEase(Ease.InOutBack);
            FindAnyObjectByType<LevelUIController>().IncreaseCoin();
            Destroy(gameObject, .55f);
        }
    }
}
