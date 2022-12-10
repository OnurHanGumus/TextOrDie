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

	#endregion

	#region Private Variables
	private Tween _patrollingTween;
	private bool _isOnHunt = false;
	private bool _isBusy = false;
	private float _waterLevel = 0;
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
		PlayerSignals.Instance.onWaterRising += OnWaterRisig;
		QuestionSignals.Instance.onAskQuestion += OnAskQuestion;
    }

	private void UnsubscribeEvents()
	{
		PlayerSignals.Instance.onInteractedWithWater -= OnInteractedWithWater;
		PlayerSignals.Instance.onWaterRising -= OnWaterRisig;
		QuestionSignals.Instance.onAskQuestion -= OnAskQuestion;

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
		).SetLookAt(0.05f);
    }
	private void Start()
    {
	}

	private void OnWaterRisig(float value)
    {
		_patrollingTween.Kill();
		_waterLevel = value;
	}

	private void OnAskQuestion(int value)
    {
		Patrolling();
    }



	private void OnInteractedWithWater(Transform target)
    {
		transform.position = new Vector3(transform.position.x, _waterLevel, transform.position.z);
        if (_isBusy)
        {
			target.parent.gameObject.SetActive(false);
			return;
        }
		_isBusy = true;
		_patrollingTween.Kill();
		transform.DOMove(new Vector3(target.position.x, target.transform.position.y, target.transform.position.z), 5).SetSpeedBased(true).OnComplete(()=>
		{
			_isBusy = false;

		});
        transform.DOLookAt(target.position, 1f);

    }

    



}