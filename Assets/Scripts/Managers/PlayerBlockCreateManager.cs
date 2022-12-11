using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Signals;
using DG.Tweening;
using Enums;
using TMPro;

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
		CoreGameSignals.Instance.onPlay += OnPlay;
		CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
    }

	private void UnsubscribeEvents()
	{
		PlayerSignals.Instance.onPlayerAnsweredRight -= OnPlayerAnsweredRight;
		CoreGameSignals.Instance.onPlay -= OnPlay;
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

	private void InitializeBlocks()
    {
		StartCoroutine(CreateBlocks(5, ""));
    }

	private IEnumerator CreateBlocks(int charCount, string word)
    {
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < charCount; i++)
		{
			GameObject block = PoolSignals.Instance.onGetObject(PoolEnums.Block);
			block.transform.localScale = Vector3.zero;
			block.transform.position = new Vector3(0, blockIndeks++, 5);
			if (word != "")
			{
				block.transform.GetChild(0).GetComponent<TextMeshPro>().text = word[word.Length - i - 1].ToString();
			}
			block.SetActive(true);
			block.transform.DOScale(new Vector3(1, 1, 1), 0.5f);

			transform.DOMoveY(blockIndeks - 0.5f, 0.2f);

			yield return new WaitForSeconds(0.2f);

		}
	}

	private void OnPlay()
    {
		InitializeBlocks();

	}
	private void OnPlayerAnsweredRight(int charCount, string word)
    {
		word = word.Remove(word.Length - 1);
		charCount = word.Length;
		StartCoroutine(CreateBlocks(charCount, word));
    }

	private void OnRestartLevel()
    {
		blockIndeks = 1;
    }

}