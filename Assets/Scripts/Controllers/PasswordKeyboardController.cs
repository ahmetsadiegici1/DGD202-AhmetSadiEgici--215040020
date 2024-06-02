using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordKeyboardController : MonoBehaviour
{
    private string currentPassword = "";
    [SerializeField] private TextMeshProUGUI[] passwordTexts;
    [HideInInspector] public LockController locker;
    public void AddNumber(int number)
    {
        if (currentPassword.Length >= 4)
            return;

        passwordTexts[currentPassword.Length].text = number.ToString();
        currentPassword += number;
    }

    public void RemoveNumber()
    {
        passwordTexts[currentPassword.Length - 1].text = "";
        currentPassword = currentPassword.Remove(currentPassword.Length - 1);
    }

    public void ApprovePassword()
    {
        if (locker.Password.Equals(currentPassword))
        {
            locker.Unlock();
            ClosePanel();
        }
    }

    public void ClosePanel() { Time.timeScale = 1; Destroy(gameObject); }
}
