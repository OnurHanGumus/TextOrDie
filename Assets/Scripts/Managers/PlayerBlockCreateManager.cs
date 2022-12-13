using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using DG.Tweening;
using Enums;
using TMPro;
using Data.ValueObject;
using Data.UnityObject;
using System;
using System.Linq;
using System.Threading.Tasks;

public class PlayerBlockCreateManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables
	[SerializeField] private int blockIndeks = 1;

	#endregion

	#region Private Variables
	private LevelData _data;
	private int _initializeBlockCounts = 5;
	private bool _canBlocksInitialize = false;
	private bool _isNewCreated = true;
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

	}

	private void SubscribeEvents()
	{
		PlayerSignals.Instance.onPlayerAnsweredRight += OnPlayerAnsweredRight;
		CoreGameSignals.Instance.onPlay += OnPlay;
		CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
		QuestionSignals.Instance.onBlocksFirstMove += OnBlocksFirstMove;
    }

	private void UnsubscribeEvents()
	{
		PlayerSignals.Instance.onPlayerAnsweredRight -= OnPlayerAnsweredRight;
		CoreGameSignals.Instance.onPlay -= OnPlay;
		CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
		QuestionSignals.Instance.onBlocksFirstMove -= OnBlocksFirstMove;
	}

	private void OnDisable()
	{
		UnsubscribeEvents();
	}

	#endregion

	private void Start()
	{
		_canBlocksInitialize = true;
	}
	public async void BlocksFirstInitialize()//task yazarak kullan.
	{
		while (!_canBlocksInitialize)
		{
			await Task.Yield();
		}
		InitializeBlocks();
	}
	private void InitializeBlocks()
	{
		StartCoroutine(CreateBlocks(_initializeBlockCounts, String.Concat(Enumerable.Repeat(" ", _data.InitializeBlockCounts))));
	}

	private IEnumerator CreateBlocks(int charCount, string word)
    {
		QuestionSignals.Instance.onPlayerChoosedWord?.Invoke(word);

		yield return new WaitForSeconds(1f);
		for (int i = 0; i < charCount; i++)
		{
			GameObject block = PoolSignals.Instance.onGetObject(PoolEnums.Block);
			block.transform.localScale = Vector3.zero;
			block.transform.position = new Vector3(0, blockIndeks++, 5);
			
			block.SetActive(true);
			block.transform.DOScale(Vector3.one, 0.5f);

			transform.DOMoveY(blockIndeks - 0.5f, 0.2f);

			yield return new WaitForSeconds(0.2f);

		}
	}

	private void OnPlay()
    {
		//InitializeBlocks();
		//BlocksFirstInitialize();


	}
	private void OnPlayerAnsweredRight(int charCount, string word)
    {
		word = word.Remove(word.Length - 1);
		charCount = word.Length;
		StartCoroutine(CreateBlocks(charCount, word));
    }
	private void OnBlocksFirstMove()
	{
		InitializeBlocks();
	}

	private void OnRestartLevel()
    {
		blockIndeks = 1;
    }

}