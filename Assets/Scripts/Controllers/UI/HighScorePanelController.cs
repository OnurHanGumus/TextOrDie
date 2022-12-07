using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HighScorePanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI highScoreTxt;
    #endregion
    #endregion
    private void Start()
    {
        InitializeText();
    }

    private void InitializeText()
    {
        int score = SaveSignals.Instance.onGetScore(SaveLoadStates.Score, SaveFiles.SaveFile);
        highScoreTxt.text = score.ToString();
    }

    public void CloseScorePanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.HighScorePanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }

    public void OnUpdateText(int newValue)
    {
        highScoreTxt.text = newValue.ToString();
    }
}
