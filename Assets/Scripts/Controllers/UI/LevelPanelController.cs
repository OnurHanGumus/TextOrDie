using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using DG.Tweening;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI questionText, answerText;
    #endregion
    #region Private Variables


    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {

    }
    private void Start()
    {
        AskQuestion();
    }
    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        if (type.Equals(ScoreTypeEnums.Score))
        {
            scoreTxt.text = score.ToString();
        }
    }

    public void OnRestartLevel()
    {
        scoreTxt.text = 0.ToString();
    }

    public void AskQuestion()
    {
        questionText.text = QuestionSignals.Instance.onGetQuestion(0);
    }

    public void PlayerHitEnterButton()
    {
        QuestionSignals.Instance.onPlayerHitEnterButton?.Invoke(answerText.text);
    }
}
