using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _targetFrameRate = 30;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
        DontDestroyOnLoad(this);
    }
}
