using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using DG.Tweening;
using Enums;
using System;

public class EnemyBlockCreateManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables
	[SerializeField] private int blockIndeks = 1, initializeBlockCounts = 5;
	[SerializeField] private int enemyId = 0;

	#endregion

	#region Private Variables
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
		QuestionSignals.Instance.onPlayerHitEnterButton += OnPlayerHitEnterButton;
		CoreGameSignals.Instance.onPlay += OnPlay;

	}

	private void UnsubscribeEvents()
	{
		QuestionSignals.Instance.onPlayerHitEnterButton -= OnPlayerHitEnterButton;
		CoreGameSignals.Instance.onPlay -= OnPlay;
	}



	private void OnDisable()
	{
		UnsubscribeEvents();
	}

	#endregion

	private void Start()
	{
	}

	private void InitializeBlocks()
    {
		StartCoroutine(CreateBlocks(5));
    }

	private IEnumerator CreateBlocks(int charCount)
    {
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < charCount; i++)
		{
			GameObject block = PoolSignals.Instance.onGetObject(PoolEnums.Block);
			block.transform.localScale = Vector3.zero;
			block.transform.position = new Vector3(transform.position.x, blockIndeks++, 5);
			block.SetActive(true);

			transform.DOMoveY(blockIndeks - 0.5f, 0.2f);

			block.transform.DOScale(new Vector3(2, 1, 2), 0.5f);
			yield return new WaitForSeconds(0.2f);

		}
	}
	private void OnPlay()
	{
		InitializeBlocks();

	}

	private void OnPlayerHitEnterButton(string arg0)
	{
		string randomAnswer = QuestionSignals.Instance.onGetRandomAnswer();
		Debug.Log(randomAnswer);
		QuestionSignals.Instance.onSendAnswerToPanel?.Invoke(randomAnswer);
		QuestionSignals.Instance.onShowAnswerPanel?.Invoke();
        StartCoroutine(CreateBlocks(randomAnswer.Length));
    }



}