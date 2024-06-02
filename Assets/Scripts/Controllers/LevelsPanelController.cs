using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelsPanelController : MonoBehaviour
{
    [SerializeField] GameObject levelCirclePrefab;
    [SerializeField] Vector2 spawnPoint;

    private GameObject levelsParent;
    private LevelManager levelManager;
    private SaveManager saveManager;
    private Coroutine generateLevelButton;

    private void Start()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
        //ShowLevels();
    }

    private void OnDisable()
    {
        if (generateLevelButton != null)
            StopCoroutine(generateLevelButton);

    }

    public void ShowLevels()
    {
        if (generateLevelButton != null)
            StopCoroutine(generateLevelButton);

        if (levelsParent)
            Destroy(levelsParent);

        levelsParent = new GameObject();
        generateLevelButton = StartCoroutine(GenerateLevelButton());
    }

    IEnumerator GenerateLevelButton()
    {

        var currentLevel = FindAnyObjectByType<SaveManager>().GetCurrentLevel();
        Debug.Log(currentLevel);
        for (int i = 1; i <= currentLevel; i++)
        {
            Debug.Log(i);
            var levelCircle = Instantiate(levelCirclePrefab, levelsParent.transform);
            levelCircle.transform.position = spawnPoint+Vector2.right*Random.Range(-1f,1f);
            levelCircle.transform.GetChild(0).GetComponent<TextMeshPro>().text = i.ToString();
            levelCircle.GetComponent<LevelButtonController>().TargetLevel = int.Parse(levelCircle.GetComponentInChildren<TextMeshPro>().text);
            yield return new WaitForSeconds(.5f);
        }
    }
}
