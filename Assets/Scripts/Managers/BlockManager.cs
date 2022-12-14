using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using TMPro;

public class BlockManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables
	[SerializeField] private TextMeshPro charText;
	[SerializeField] private int blockId = 0;
	[SerializeField] private bool isFirstTimeInitialized = true;
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
		UpdateText();
	}

	private void SubscribeEvents()
	{
		CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
    }

	private void UnsubscribeEvents()
	{
		CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;

	}

	private void OnDisable()
	{
		UnsubscribeEvents();
	}

    #endregion

    private void Start()
    {
		AskQuestion();
	}
	public void AskQuestion()
	{

	}

	private void UpdateText()
    {
        if (isFirstTimeInitialized)
        {
			isFirstTimeInitialized = false;
			return;
        }
		charText.text = QuestionSignals.Instance.onGetWord(blockId);
    }

	private void OnRestartLevel()
    {
		charText.text = "";
    }
}