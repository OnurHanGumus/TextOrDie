using Cinemachine;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public CameraStates CameraStateController
        {
            get => _cameraStateValue;
            set
            {
                _cameraStateValue = value;
                SetCameraStates();
            }
        }

        #endregion
        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera startCam;
        [SerializeField] private CinemachineVirtualCamera gameCam;

        #endregion

        #region Private Variables

        private Vector3 _initialPosition;
        private CameraStates _cameraStateValue = CameraStates.Start;
        private Animator _camAnimator;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _camAnimator = GetComponent<Animator>();
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


        private void Start()
        {

        }
        private void SetCameraStates()
        {
            if (CameraStateController == CameraStates.Start)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
            else if (CameraStateController == CameraStates.Game)
            {
                _camAnimator.Play(CameraStateController.ToString());
            }
        }


        private void ChangeGameState(CameraStates cameraState)
        {
            CameraStateController = cameraState;
            SetCameraStates();
        }
        private void OnPlay()
        {
            ChangeGameState(CameraStates.Game);
        }
        private void OnRestartLevel()
        {
            ChangeGameState(CameraStates.Start);

        }

    }
}