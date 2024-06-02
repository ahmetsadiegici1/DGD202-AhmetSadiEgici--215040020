using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<LevelModel> Levels;

    private SaveManager saveManager;

    private void Awake() => DontDestroyOnLoad(this);

    private void Start()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
    }

    public int GetCurrentLevelNumber() => Levels.Find(x => x.LevelName.Equals(SceneManager.GetActiveScene().name)).LevelNumber;

    public void LoadNextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void LoadCurrentLevel() => SceneManager.LoadScene(Levels[saveManager.GetCurrentLevel() - 1].LevelName);

    public void LoadLevel(int levelNumber) => SceneManager.LoadScene(Levels.Find(x => x.LevelNumber.Equals(levelNumber)).LevelName);

    public void ReloadLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    public void LoadMainMenu() => SceneManager.LoadScene(0);

    [Serializable]
    public class LevelModel
    {
        public string LevelName;
        public int LevelNumber;
    }
}