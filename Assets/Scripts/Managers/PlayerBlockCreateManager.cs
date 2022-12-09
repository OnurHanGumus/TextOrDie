using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using DG.Tweening;
using Enums;

public class PlayerBlockCreateManager : MonoBehaviour
{

	#region Self Variables
	#region Public Variables
	#endregion

	#region SerializeField Variables
	[SerializeField] private int blockIndeks = 1, initializeBlockCounts = 5;

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
		PlayerSignals.Instance.onPlayerAnsweredRight += OnPlayerAnsweredRight;
    }

	private void UnsubscribeEvents()
	{
		PlayerSignals.Instance.onPlayerAnsweredRight -= OnPlayerAnsweredRight;
	}

	private void OnDisable()
	{
		UnsubscribeEvents();
	}

	#endregion

	private void Start()
	{
		InitializeBlocks();
	}

	private void InitializeBlocks()
    {
		StartCoroutine(CreateBlocks(5));
    }

	private IEnumerator CreateBlocks(int charCount)
    {
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < charCount; i++)
		{
			GameObject block = PoolSignals.Instance.onGetObject(PoolEnums.Block);
			block.transform.localScale = Vector3.zero;
			block.transform.position = new Vector3(0, blockIndeks++, 5);
			block.SetActive(true);

			transform.DOMoveY(blockIndeks - 0.5f, 0.2f);

			block.transform.DOScale(new Vector3(2, 1, 2), 0.5f);
			yield return new WaitForSeconds(0.2f);

		}
	}
	private void OnPlayerAnsweredRight(int charCount)
    {
		StartCoroutine(CreateBlocks(charCount));
    }



}