using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;
using Signals;

namespace Controllers
{
    public class WaterPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables



        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                PlayerSignals.Instance.onInteractedWithWater?.Invoke(other.transform.position);
            }
        }
    }
}