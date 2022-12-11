using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using DG.Tweening;

public class QuestionManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables
	[SerializeField] private int questionId = 0;
	[SerializeField] private bool isPlayerOnWater = false;
	#endregion

	#region Private Variables
	private int _remainEnemy = 4;
	#endregion
	#endregion
	private void Awake()
	{
	}
	#region Event Subscriptions

	private void OnEnable()
	{
		SubscribeEvents();
	}

	private void SubscribeEvents()
	{
		CoreGameSignals.Instance.onPlay += OnPlay;
		CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        QuestionSignals.Instance.onGetQuestionId += OnGetQuestionId;
		LevelSignals.Instance.onBlockRisingEnd += OnBlockRisingEnd;
		LevelSignals.Instance.onTargetsAreCleared += OnTargetsAreCleared;
		LevelSignals.Instance.onPlayerInWater += OnPlayerInWater;
		LevelSignals.Instance.onEnemyDie += OnEnemyDie;
    }

	private void UnsubscribeEvents()
	{
		CoreGameSignals.Instance.onPlay -= OnPlay;
		CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        QuestionSignals.Instance.onGetQuestionId -= OnGetQuestionId;
		LevelSignals.Instance.onBlockRisingEnd -= OnBlockRisingEnd;
		LevelSignals.Instance.onTargetsAreCleared -= OnTargetsAreCleared;
		LevelSignals.Instance.onPlayerInWater -= OnPlayerInWater;
		LevelSignals.Instance.onEnemyDie -= OnEnemyDie;
	}

	private void OnDisable()
	{
		UnsubscribeEvents();
	}

    #endregion

    private void Start()
    {
		
	}

    private void OnPlay()
    {
		AskQuestion();
	}
    public void AskQuestion()
	{
		isPlayerOnWater = false;
		QuestionSignals.Instance.onAskQuestion?.Invoke(questionId);

	}


	private void OnBlockRisingEnd(float delay)
    {
		StartCoroutine(WaterRising(delay));

	}
	private IEnumerator WaterRising(float delay)
    {
		yield return new WaitForSeconds(delay);
		LevelSignals.Instance.onWaterRising?.Invoke();

		yield return new WaitForSeconds(2f);
        questionId++;

        if (questionId == 4)
        {
            questionId = 0;
        }

        if (isPlayerOnWater)
        {
        }
        else
        {
            AskQuestion();
        }

    }

	private void OnPlayerInWater()
    {
		isPlayerOnWater = true;
    }

	private void OnEnemyDie()
    {
		--_remainEnemy;
        if (_remainEnemy <= 0)
        {
			CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
        }
    }

	private void OnTargetsAreCleared()
    {
		AskQuestion();
    }

	public int OnGetQuestionId()
	{
		return questionId;
	}

	private void OnRestartLevel()
    {
		_remainEnemy = 4;
    }

}