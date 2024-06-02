using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SettingsController : MonoBehaviour
{
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SoundFxSlider;
    [SerializeField] TMP_Dropdown LanguageDropdown;

    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindAnyObjectByType<SaveManager>();
        InitializeSettings();
    }

    private void OnEnable()
    {
        LanguageDropdown.onValueChanged.AddListener(ApplyLanguage);
    }

    private void OnDisable()
    {
        LanguageDropdown.onValueChanged.RemoveListener(ApplyLanguage);
    }

    public void CloseSettings()
    {
        SaveSettings();
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void ReturnMainMenu() => FindAnyObjectByType<LevelManager>().LoadMainMenu();

    private void InitializeSettings()
    {
        MusicSlider.value = saveManager.GetMusicLevel();
        SoundFxSlider.value = saveManager.GetSoundFxLevel();
        LanguageDropdown.value = saveManager.GetLanguage();
    }

    private void SaveSettings() => saveManager.SaveSettings(MusicSlider.value, SoundFxSlider.value, LanguageDropdown.value);

    private void ApplyLanguage(int languageId) => saveManager.StartCoroutine(saveManager.SetLanguage(languageId));

}
