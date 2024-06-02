using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FinishController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.GetComponent<PlayerController>());
            Destroy(collision.GetComponent<Rigidbody2D>());
            Destroy(collision.GetComponent<CircleCollider2D>());
            collision.transform.SetParent(transform.GetChild(0));
            transform.GetChild(0).DORotate(Vector3.forward * -90, .25f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            collision.transform.DOScale(Vector3.zero, 3f).OnComplete(() => StartCoroutine(GetNextLevel()));
            collision.transform.DOLocalMove(Vector3.zero, 3f).OnComplete(() => StartCoroutine(GetNextLevel()));
            Debug.Log("Level Finished");
        }
    }

    IEnumerator GetNextLevel()
    {
        yield return new WaitForSeconds(1f);
        var levelManager = FindAnyObjectByType<LevelManager>();
        FindAnyObjectByType<SaveManager>().SaveLevel(levelManager.GetCurrentLevelNumber());
        levelManager.LoadNextLevel();
    }
}
