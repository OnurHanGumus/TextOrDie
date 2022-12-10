using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using Enums;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
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
		CoreGameSignals.Instance.onPlay += OnPlay;
		CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
    }

	private void UnsubscribeEvents()
	{
		CoreGameSignals.Instance.onPlay -= OnPlay;
		CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
	}

	private void OnDisable()
	{
		UnsubscribeEvents();
	}

    #endregion
    private void OnPlay()
    {
		gameObject.SetActive(true);
    }
    private void OnRestartLevel()
    {

    }
}