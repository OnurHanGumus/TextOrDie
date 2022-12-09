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
	private float _waterPosY = 0f;
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
	private void SetState(SharkStateEnums state)
    {
        if (state.Equals(SharkStateEnums.Patrolling))
        {
			Patrolling();
        }
        else if (state.Equals(SharkStateEnums.LockToTarget))
        {

        }
    }

	private void Patrolling()
	{
		_patrollingTween = transform.DOPath(new Vector3[2]
		{
			new Vector3(-20, _waterPosY, 10), new Vector3(20, _waterPosY, 10),
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

	private void OnWaterRisig()
    {
		_patrollingTween.Kill();
		_waterPosY += 3;
	}

	private void OnAskQuestion(int value)
    {
		Debug.Log("tetikledni");
		Patrolling();
    }



	private void OnInteractedWithWater(Vector3 pos)
    {
		_patrollingTween.Kill();
		transform.DOMove(pos, 5).SetSpeedBased(true).OnComplete(()=>
		{
			Patrolling();
		});
        transform.DOLookAt(pos, 1f);

    }

    



}