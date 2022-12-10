using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using Signals;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour
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
                transform.parent.gameObject.SetActive(false);
                LevelSignals.Instance.onEnemyDie?.Invoke();
            }
        }
    }
}