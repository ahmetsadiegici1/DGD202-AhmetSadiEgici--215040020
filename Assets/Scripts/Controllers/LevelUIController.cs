using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    public UnityEvent<float> MoveInputEvent;
    public UnityEvent JumpInputEvent;

    [SerializeField] Transform LevelMainCanvas;
    [SerializeField] GameObject LevelSettingPanelPrefab;
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] RectTransform PauseButton;
    [SerializeField] TextMeshProUGUI CoinText;
   // [SerializeField] Text debug;
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = FindAnyObjectByType<LevelManager>();
        LevelText.text = $"Level {_levelManager?.GetCurrentLevelNumber()}";
        PauseButton.anchoredPosition = new Vector2(0, PauseButton.sizeDelta.y / 2);
        LevelText.rectTransform.DOAnchorPosY(LevelText.rectTransform.sizeDelta.y / 2, 1)
                               .SetDelay(2f)
                               .OnComplete(() =>
                               {
                                   PauseButton.DOAnchorPosY(-PauseButton.anchoredPosition.y, .5f);
                               });

        var coins = FindObjectsByType<CoinBehaviour>(FindObjectsSortMode.None);
        foreach (var coin in coins)
            coin.CoinCollectPoint = CoinText.transform.parent;

    }

 

    public void OnMoveInputCalled(float direction) => MoveInputEvent.Invoke(direction);
    public void OnJumpInputCalled() => JumpInputEvent.Invoke();
    public void OpenSettingsPanel() => Instantiate(LevelSettingPanelPrefab, LevelMainCanvas);

    public void IncreaseCoin() => CoinText.text = (int.Parse(CoinText.text) + 1).ToString();
}
