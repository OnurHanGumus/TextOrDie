using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Extentions;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables


        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private ClearActiveLevelCommand levelClearer;

        #endregion

        #region Private Variables

        [ShowInInspector] private int _levelID;
        private LevelData _data;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
            InitializeLevel();
        }

        private void Init()
        {
            _levelID = GetActiveLevel();
            _data = GetData();
        }

        private void Start()
        {
            
        }

        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }

        private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;

        }



        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onGetLevelID -= OnGetLevelID;
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void OnNextLevel()
        {
            _levelID++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            //CoreGameSignals.Instance.onSaveAndResetGameData?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            //CoreGameSignals.Instance.onSaveAndResetGameData?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private int OnGetLevelID()
        {
            return _levelID;
        }


        private void OnPlay()
        {
            InitializeOtherPlayers();
        }
        private void OnInitializeLevel()
        {
            InitializeLevel();
        }
        private void InitializeLevel()
        {
            UnityEngine.Object[] Levels = Resources.LoadAll("Levels");
            int newLevelId = _levelID % Levels.Length;
            levelLoader.InitializeLevel((GameObject)Levels[newLevelId], levelHolder.transform);

        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }

        private void InitializeOtherPlayers()
        {
            for (int i = 0; i < _data.OtherPlayerPositions.Length; i++)
            {
                GameObject block = PoolSignals.Instance.onGetObject(PoolEnums.Block);
                block.transform.position = new Vector3(_data.OtherPlayerPositions[i].x, _data.OtherPlayerPositions[i].y - 0.5f, _data.OtherPlayerPositions[i].z);
                block.SetActive(true);
                GameObject otherPlayer = PoolSignals.Instance.onGetObject(PoolEnums.Enemy);
                otherPlayer.transform.position = _data.OtherPlayerPositions[i];
                otherPlayer.SetActive(true);
            }
        }
    }
}