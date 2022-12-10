using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using DG.Tweening;

public class WaterManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables

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
		LevelSignals.Instance.onWaterRising += OnWaterRising;
    }

	private void UnsubscribeEvents()
	{
		LevelSignals.Instance.onWaterRising -= OnWaterRising;

	}

	private void OnDisable()
	{
		UnsubscribeEvents();
	}

    #endregion

    private void Start()
    {
	}

	private void OnWaterRising(float value)
    {
		transform.DOMoveY(transform.position.y + 8, 1.5f).OnComplete(()=> 
		{
			LevelSignals.Instance.onWaterRised?.Invoke(transform.position.y);
		});

	}



}