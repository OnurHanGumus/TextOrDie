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
	[SerializeField] private Transform waterTransform;
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
        QuestionSignals.Instance.onGetQuestionId += OnGetQuestionId;
		PlayerSignals.Instance.onBlockRisingEnd += OnBlockRisingEnd;
    }

	private void UnsubscribeEvents()
	{
        QuestionSignals.Instance.onGetQuestionId -= OnGetQuestionId;
		PlayerSignals.Instance.onBlockRisingEnd -= OnBlockRisingEnd;
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
		QuestionSignals.Instance.onAskQuestion?.Invoke(questionId);
	}


	private void OnBlockRisingEnd(float delay)
    {
		StartCoroutine(WaterRising(delay));

	}
	private IEnumerator WaterRising(float delay)
    {
		yield return new WaitForSeconds(delay);
		PlayerSignals.Instance.onWaterRising?.Invoke();
		waterTransform.DOMoveY(waterTransform.position.y + 3, 1.5f).SetDelay(0.5f).OnComplete(() =>
			{
				questionId++;
				AskQuestion();

			}
		);

	}

	public int OnGetQuestionId()
	{
		return questionId;
	}

}