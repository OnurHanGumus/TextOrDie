using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using DG.Tweening;
using Enums;
using System;
using TMPro;
using Data.ValueObject;
using Data.UnityObject;

public class EnemyBlockCreateManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables
	[SerializeField] private int blockIndeks = 1;
	[SerializeField] private int enemyId = 0;

	#endregion

	#region Private Variables
	private int _initializeBlockCounts = 5;
	private LevelData _data;

	#endregion
	#endregion
	private void Awake()
	{
		Init();
	}

	private void Init()
	{
		_data = GetData();
		_initializeBlockCounts = _data.InitializeBlockCounts;
	}
	private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;

	#region Event Subscriptions

	private void OnEnable()
	{
		SubscribeEvents();
		blockIndeks = 1;
		InitializeBlocks();


	}

	private void SubscribeEvents()
	{
		QuestionSignals.Instance.onPlayerHitEnterButton += OnPlayerHitEnterButton;
		CoreGameSignals.Instance.onPlay += OnPlay;
		CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;

	}

	private void UnsubscribeEvents()
	{
		QuestionSignals.Instance.onPlayerHitEnterButton -= OnPlayerHitEnterButton;
		CoreGameSignals.Instance.onPlay -= OnPlay;
		CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
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
		StartCoroutine(CreateBlocks(_initializeBlockCounts, ""));
    }

	private IEnumerator CreateBlocks(int charCount, string word)
    {
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < charCount; i++)
		{
			GameObject block = PoolSignals.Instance.onGetObject(PoolEnums.Block);
			block.transform.localScale = Vector3.zero;
			block.transform.position = new Vector3(transform.position.x, blockIndeks++, 5);
            if (word != "")
            {
                block.transform.GetChild(0).GetComponent<TextMeshPro>().text = word[word.Length - i - 1].ToString();
            }
            block.SetActive(true);
			block.transform.DOScale(Vector3.one, 0.5f);



			transform.DOMoveY(blockIndeks - 0.5f, 0.2f);

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
		QuestionSignals.Instance.onSendAnswerToPanel?.Invoke(randomAnswer);
		QuestionSignals.Instance.onShowAnswerPanel?.Invoke();
        StartCoroutine(CreateBlocks(randomAnswer.Length, randomAnswer));
    }

	private void OnRestartLevel()
    {
		blockIndeks = 1;
	}

}