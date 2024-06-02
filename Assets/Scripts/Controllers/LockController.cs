using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{

    public string Password;
    [SerializeField] GameObject PasswordScreen;
    [SerializeField] Transform LockedDoor;
    [SerializeField] Vector3 UnlockMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!FindAnyObjectByType<PasswordKeyboardController>())
            {
                var PassKeyboard = Instantiate(PasswordScreen, FindAnyObjectByType<Canvas>().transform);
                PassKeyboard.GetComponent<PasswordKeyboardController>().locker = this;
                Time.timeScale = 0;
            }
        }
    }

    public void Unlock()
    {
        LockedDoor.DOMove(LockedDoor.position + UnlockMove, 1);
        Destroy(GetComponent<Collider2D>());
    }
}
