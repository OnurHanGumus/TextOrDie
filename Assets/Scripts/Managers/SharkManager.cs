using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using Enums;
using DG.Tweening;

public class SharkManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables
	[SerializeField] private SharkStateEnums currentState;
	[SerializeField] private List<Vector3> targetList;

	#endregion

	#region Private Variables
	private Tween _patrollingTween;
	private Tween _huntingTween;
	private bool _isOnHunt = false;
	private bool _isBusy = false;
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
		PlayerSignals.Instance.onInteractedWithWater += OnInteractedWithWater;
		LevelSignals.Instance.onWaterRising += OnWaterRisig;
		LevelSignals.Instance.onWaterRised += OnWaterRised;
		QuestionSignals.Instance.onAskQuestion += OnAskQuestion;
		CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
    }

	private void UnsubscribeEvents()
	{
		PlayerSignals.Instance.onInteractedWithWater -= OnInteractedWithWater;
		LevelSignals.Instance.onWaterRising -= OnWaterRisig;
		LevelSignals.Instance.onWaterRised -= OnWaterRised;
		QuestionSignals.Instance.onAskQuestion -= OnAskQuestion;
		CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
	}

	private void OnDisable()
	{
		UnsubscribeEvents();
	}

	#endregion

	private void Patrolling()
	{
		_patrollingTween = transform.DOPath(new Vector3[2]
		{
			new Vector3(-20, transform.parent.localPosition.y, 10), new Vector3(20, transform.parent.localPosition.y, 10),
		}, 5f).SetSpeedBased(true).OnComplete(() =>
			{
                transform.eulerAngles += new Vector3(0, -90, 0);
                Patrolling();
			}
		).SetLookAt(0.05f).SetEase(Ease.Linear);
    }
	private void Start()
    {
	}

	private void OnWaterRisig()
    {
		_patrollingTween.Kill();
	}

	private void OnAskQuestion(int value)
    {
		Patrolling();
		targetList.Clear();
    }



	private void OnInteractedWithWater(Transform target)
    {
		targetList.Add(target.position);

    }

	private void OnWaterRised(float waterLevel)
	{ 
		_huntingTween = transform.DOPath(targetList.ToArray(), 5f).SetSpeedBased(true).SetLookAt(0.05f).OnComplete(()=>
			{
				transform.eulerAngles = Vector3.zero;
				LevelSignals.Instance.onTargetsAreCleared?.Invoke();
			}
		).SetEase(Ease.Linear);
	}

	private void OnLevelSuccessful()
    {
		_huntingTween.Kill();
	}

    



}