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

        [ShowInInspector] private int _levelId, _moddedLevelId;
        private LevelData _data;


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
        }

        private void Start()
        {
            GetValuesFromSave();
            InitializeLevel();

        }

        private void GetValuesFromSave()
        {
            _levelId = SaveSignals.Instance.onGetScore(SaveLoadStates.Level, SaveFiles.SaveFile);
            Debug.Log(_levelId);
        }


        private int OnGetModdedLevelId()
        {
            return _moddedLevelId;
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
            CoreGameSignals.Instance.onLevelInitialize += OnInitializeLevel;

            LevelSignals.Instance.onGetCurrentModdedLevel += OnGetModdedLevelId;
            LevelSignals.Instance.onGetLevel += OnGetLevelID;

        }



        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
            CoreGameSignals.Instance.onLevelInitialize -= OnInitializeLevel;

            LevelSignals.Instance.onGetCurrentModdedLevel -= OnGetModdedLevelId;
            LevelSignals.Instance.onGetLevel -= OnGetLevelID;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private void OnNextLevel()
        {
            _levelId++;
            SaveSignals.Instance.onSaveScore?.Invoke(_levelId, SaveLoadStates.Level, SaveFiles.SaveFile);
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
        }

        private int OnGetLevelID()
        {
            return _levelId;
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
            int newLevelId = _levelId % Levels.Length;
            _moddedLevelId = newLevelId;
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