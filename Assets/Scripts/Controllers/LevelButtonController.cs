using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtonController : MonoBehaviour
{
    public int TargetLevel;

    private void OnMouseDown()
    {
        print(TargetLevel);
        FindAnyObjectByType<LevelManager>().LoadLevel(TargetLevel);
        
    }
}
