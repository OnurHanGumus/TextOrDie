using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Managers;

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
    [SerializeField] private UIManager manager;

    #endregion
    #region Private Variables
    private UIData _data;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _data = manager.GetData();
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
        questionPanel.transform.DOLocalMoveY(_data.QuestionPanelScreenPosY, _data.UIAnimationDelay);
        answerPanel.transform.DOLocalMoveX(_data.AnswerPanelScreenPosX, _data.UIAnimationDelay);
    }
    private void CloseQuestionAnswerPanel()
    {
        questionPanel.transform.DOLocalMoveY(_data.QuestionPanelClosePosY, _data.UIAnimationDelay);
        answerPanel.transform.DOLocalMoveX(_data.AnswerPanelClosePosX, _data.UIAnimationDelay);
        enemyAnswerPanel.DOLocalMoveX(_data.EnemyAnswerPanelClosePosX, _data.UIAnimationDelay).OnComplete(()=> 
        {
            foreach (var i in enemyAnswerTextList)
            {
                i.transform.parent.gameObject.SetActive(false);
            }
        });
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
        LevelSignals.Instance.onBlockRisingEnd?.Invoke(longestWordCharCount * _data.UIAnimationDelay);
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
        enemyAnswerPanel.DOLocalMoveX(_data.EnemyAnswerPanelScreenPosX, _data.UIAnimationDelay);
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
