using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Enums;
using TMPro;

public class PoolManager : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject enemyBlock0, enemyBlock1, enemyBlock2, enemyBlock3;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private GameObject cubePrefab;

    [SerializeField] private Dictionary<PoolEnums, List<GameObject>> poolDictionary;


    [SerializeField] private int amountBlocks = 50;
    [SerializeField] private int amountParticle = 5;
    [SerializeField] private int amountEnemies = 5;
    [SerializeField] private int amountTexts = 200;
    [SerializeField] private int amountCube = 10;



    #endregion
    #region Private Variables
    private int _levelId = 0;
    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _levelId = LevelSignals.Instance.onGetCurrentModdedLevel();
        poolDictionary = new Dictionary<PoolEnums, List<GameObject>>();
        InitializePool(PoolEnums.Block, blockPrefab, amountBlocks);
        InitializePool(PoolEnums.Enemy, enemyPrefab, amountEnemies);
        InitializePool(PoolEnums.Cube, cubePrefab, amountCube);

        InitializePool(PoolEnums.EnemyBlock0, enemyBlock0, amountBlocks);
        InitializePool(PoolEnums.EnemyBlock1, enemyBlock1, amountBlocks);
        InitializePool(PoolEnums.EnemyBlock2, enemyBlock2, amountBlocks);
        InitializePool(PoolEnums.EnemyBlock3, enemyBlock3, amountBlocks);
        //InitializePool(PoolEnums.Particle, particlePrefab, amountParticle);
    }



    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.Instance.onGetPoolManagerObj += OnGetPoolManagerObj;
        PoolSignals.Instance.onGetObject += OnGetObject;
        CoreGameSignals.Instance.onRestartLevel += OnReset;

    }

    private void UnsubscribeEvents()
    {
        PoolSignals.Instance.onGetPoolManagerObj -= OnGetPoolManagerObj;
        PoolSignals.Instance.onGetObject -= OnGetObject;
        CoreGameSignals.Instance.onRestartLevel -= OnReset;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void InitializePool(PoolEnums type, GameObject prefab, int size)
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < size; i++)
        {
            tmp = Instantiate(prefab, transform);
            tmp.SetActive(false);
            tempList.Add(tmp);
        }
        poolDictionary.Add(type, tempList);
    }

    public GameObject OnGetObject(PoolEnums type)
    {
        for (int i = 0; i < poolDictionary[type].Count; i++)
        {
            if (!poolDictionary[type][i].activeInHierarchy)
            {
                return poolDictionary[type][i];
            }
        }
        return null;
    }

    public Transform OnGetPoolManagerObj()
    {
        return transform;
    }


    private void OnReset()
    {
        //reset
        ResetPool(PoolEnums.Block);
        ResetPool(PoolEnums.Enemy);
        ResetPool(PoolEnums.EnemyBlock0);
        ResetPool(PoolEnums.EnemyBlock1);
        ResetPool(PoolEnums.EnemyBlock2);
        ResetPool(PoolEnums.EnemyBlock3);
        ResetPool(PoolEnums.Cube);
        //ResetPool(PoolEnums.Particle);
    }

    private void ResetPool(PoolEnums type)
    {
        foreach (var i in poolDictionary[type])
        {
            i.SetActive(false);
        }
    }
}
