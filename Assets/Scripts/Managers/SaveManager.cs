using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SaveManager : MonoBehaviour
{
    private const string MUSIC_LEVEL_KEY = "music_level_key";
    private const string SOUND_FX_LEVEL_KEY = "sound_fx_level_key";
    private const string LANGUAGE_KEY = "language_key";
    private const string CURRENT_LEVEL_KEY = "current_level_key";

    void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(SetLanguage(GetLanguage()));
    }

    public void SaveSettings(float musicLevel, float soundFxLevel, int language)
    {
        PlayerPrefs.SetFloat(MUSIC_LEVEL_KEY, musicLevel);
        PlayerPrefs.SetFloat(SOUND_FX_LEVEL_KEY, soundFxLevel);
        PlayerPrefs.SetInt(LANGUAGE_KEY, language);
    }

    public void SaveLevel(int levelNumber)
    {
        if (levelNumber > GetCurrentLevel())
            PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, levelNumber);
    }

    public float GetMusicLevel() => PlayerPrefs.GetFloat(MUSIC_LEVEL_KEY, 1);
    public float GetSoundFxLevel() => PlayerPrefs.GetFloat(SOUND_FX_LEVEL_KEY, 1);
    public int GetLanguage() => PlayerPrefs.GetInt(LANGUAGE_KEY, 1);
    public int GetCurrentLevel() => PlayerPrefs.GetInt(CURRENT_LEVEL_KEY, 1);

    public IEnumerator SetLanguage(int languageId)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageId];
        Debug.Log(LocalizationSettings.AvailableLocales.Locales[languageId].LocaleName);
    }
}