using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Transform MainCanvas;
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject SettingsPrefab;
    [SerializeField] GameObject LevelsPanel;

    public void PlayGame() => FindAnyObjectByType<LevelManager>().LoadCurrentLevel();
    public void OpenSettings() => Instantiate(SettingsPrefab, MainCanvas);

    public void SetActivityLevelsPanel(bool activity)
    {
        LevelsPanel.SetActive(activity);

    }
}
