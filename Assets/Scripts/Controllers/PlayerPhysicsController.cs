using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using Signals;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables



        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Water"))
            {
                LevelSignals.Instance.onPlayerInWater?.Invoke();
            }
            else if (other.CompareTag("Shark"))
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
            }
        }
    }
}