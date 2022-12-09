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
    [SerializeField] private TextMeshProUGUI charCounterText;
    [SerializeField] private TextMeshProUGUI questionText, answerText;
    [SerializeField] private int questionId = 0;
    [SerializeField] private List<string> enemyAnswerList;
    [SerializeField] private Transform enemyAnswerPanel;
    [SerializeField] private List<TextMeshProUGUI> enemyAnswerTextList;
    [SerializeField] private Transform questionPanel, answerPanel;

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
    }
    
    public void PlayerHitEnterButton()
    {
        QuestionSignals.Instance.onPlayerHitEnterButton?.Invoke(answerText.text);
    }

    public void IncreaseCharCounterText()
    {
        charCounterText.text = answerText.text.Length.ToString();
    }

    public void ShowQuestionAnswerPanel()
    {
        questionPanel.transform.DOLocalMoveY(668f, 0.5f);
        answerPanel.transform.DOLocalMoveX(0, 0.5f);
    }
    private void CloseQuestionAnswerPanel()
    {
        questionPanel.transform.DOLocalMoveY(1250f, 0.5f);
        answerPanel.transform.DOLocalMoveX(-1100f, 0.5f);
        enemyAnswerPanel.DOLocalMoveX(1100f, 0.5f);
    }

    private void SelectLongestWord()
    {
        int longestWordCharCount = 0;
        for (int i = 0; i < enemyAnswerList.Count; i++)
        {
            if (enemyAnswerList[i].Length > longestWordCharCount)
            {
                longestWordCharCount = enemyAnswerList[i].Length;
            }
        }
        if (answerText.text.Length > longestWordCharCount)
        {
            longestWordCharCount = answerText.text.Length;
        }
        PlayerSignals.Instance.onBlockRisingEnd?.Invoke(longestWordCharCount * 0.5f);
    }

    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        if (type.Equals(ScoreTypeEnums.Score))
        {
            scoreTxt.text = score.ToString();
        }
    }

    public void OnAskQuestion(int id)
    {
        questionText.text = QuestionSignals.Instance.onGetQuestion(id);
        ShowQuestionAnswerPanel();
        enemyAnswerList.Clear();
    }
    public void OnSendAnswerToPanel(string enemyAnswer)
    {
        enemyAnswerList.Add(enemyAnswer);
    }
    public void OnShowEnemyAnswerInPanel()
    {
        enemyAnswerPanel.DOLocalMoveX(0f, 0.5f);
        for (int i = 0; i < enemyAnswerList.Count; i++)
        {
            enemyAnswerTextList[i].text = enemyAnswerList[i];
            enemyAnswerTextList[i].transform.parent.gameObject.SetActive(true);
        }
    }
    public void OnPlayerHitEnterButton(string value)
    {
        SelectLongestWord();
    }

    public void OnWaterRising()
    {
        CloseQuestionAnswerPanel();
    }
    public void OnRestartLevel()
    {
        scoreTxt.text = 0.ToString();
    }
}
