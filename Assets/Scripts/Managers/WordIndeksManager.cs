using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using DG.Tweening;

public class WordIndeksManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables
	[SerializeField] private string[] enemyWordArray;
	[SerializeField] private int[] enemyIndeksArray;

	[SerializeField] private string playerWord;
	[SerializeField] private int playerIndeks;

	#endregion

	#region Private Variables
	private int _enemyId = 0; 
	#endregion
	#endregion
	private void Awake()
	{
		Init();
	}
	private void Init()
    {
		enemyWordArray = new string[4];
		enemyIndeksArray = new int[4];
    }
	#region Event Subscriptions

	private void OnEnable()
	{
		SubscribeEvents();
	}

	private void SubscribeEvents()
	{
		QuestionSignals.Instance.onEnemyChoosedWord += OnEnemyChoosedWord;
		QuestionSignals.Instance.onGetEnemtId += OnGetEnemyId;
		QuestionSignals.Instance.onGetWord += OnGetWord;
		QuestionSignals.Instance.onAskQuestion += OnAskQuestion;
		QuestionSignals.Instance.onPlayerChoosedWord += OnPlayerChoosedWord;
		CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
	}

	private void UnsubscribeEvents()
	{
		QuestionSignals.Instance.onEnemyChoosedWord -= OnEnemyChoosedWord;
		QuestionSignals.Instance.onGetEnemtId -= OnGetEnemyId;
		QuestionSignals.Instance.onGetWord -= OnGetWord;
		QuestionSignals.Instance.onAskQuestion -= OnAskQuestion;
		QuestionSignals.Instance.onPlayerChoosedWord -= OnPlayerChoosedWord;
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

	private int OnGetEnemyId()
    {
		return _enemyId++;
    }

	private void OnEnemyChoosedWord(int id, string word)
    {
		enemyWordArray[id] = word;
    }

	private void OnPlayerChoosedWord(string word)
	{
		playerWord = word;
	}



	private string OnGetWord(int id)
    {
        if (id == 5)
        {
			return playerWord[playerWord.Length - playerIndeks++ - 1].ToString();
		}
		string word = enemyWordArray[id];
		Debug.Log(word.Length);
		return word[word.Length - enemyIndeksArray[id]++ - 1].ToString();
	}

	private void OnAskQuestion(int value)
    {
		for (int i = 0; i < enemyIndeksArray.Length; i++)
		{
			enemyIndeksArray[i] = 0;
		}
		playerIndeks = 0;
	}
	private void OnRestartLevel()
	{
		_enemyId = 0;
		for (int i = 0; i < enemyIndeksArray.Length; i++)
		{
			enemyIndeksArray[i] = 0;
		}
		playerIndeks = 0;
	}

}