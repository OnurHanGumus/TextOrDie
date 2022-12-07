using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class GameOverPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private GameObject successPanel, failPanel;
    [SerializeField] private TextMeshProUGUI scoreTxt;

    #endregion
    #region Private Variables
    private int _highScore;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _highScore = InitializeHighScore();
    }

    private void Start()
    {

    }

    public void CloseGameOverPanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }
    private int InitializeHighScore()
    {
        return SaveSignals.Instance.onGetScore(SaveLoadStates.Score, SaveFiles.SaveFile);
    }
    public void ShowThePanel()
    {
        int temp = ScoreSignals.Instance.onGetScore();

        if(temp > _highScore)
        {
            successPanel.SetActive(true);
            failPanel.SetActive(false);
            scoreTxt.text = "High Score: " + temp;
            _highScore = temp;
            SaveSignals.Instance.onSaveScore?.Invoke(temp,SaveLoadStates.Score,SaveFiles.SaveFile);
        }
        else
        {
            successPanel.SetActive(false);
            failPanel.SetActive(true);
            scoreTxt.text = "Score: " + temp;
        }
    }

    public void TryAgainBtn()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        CoreGameSignals.Instance.onPlay?.Invoke();
    }
    public void MenuBtn()
    {
        CoreGameSignals.Instance.onRestartLevel?.Invoke();
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }

    [Button]
    public void Open()
    {
        CoreGameSignals.Instance.onLevelFailed?.Invoke();
    }
}
