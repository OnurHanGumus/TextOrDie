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
    [SerializeField] private int questionId = 0;
    [SerializeField] private List<string> enemyAnswerList;
    [SerializeField] private Transform enemyAnswerPanel;
    [SerializeField] private List<TextMeshProUGUI> enemyAnswerTextList;

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
        enemyAnswerList.Clear();
    }
    public void OnSendAnswerToPanel(string enemyAnswer)
    {
        enemyAnswerList.Add(enemyAnswer);
    }
    public void OnShowAnswerToPanel()
    {
        enemyAnswerPanel.transform.localPosition = Vector3.zero;
        for (int i = 0; i < enemyAnswerList.Count; i++)
        {
            enemyAnswerTextList[i].text = enemyAnswerList[i];
            enemyAnswerTextList[i].transform.parent.gameObject.SetActive(true);
        }
    }
    public void OnRestartLevel()
    {
        scoreTxt.text = 0.ToString();
    }
}
