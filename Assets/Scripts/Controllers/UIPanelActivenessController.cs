using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class UIPanelActivenessController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<CanvasGroup> panels;

        #endregion

        #endregion

        public void OpenMenu(UIPanels storeMenu)
        {
            panels[(int)storeMenu].DOFade(1f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
            //panels[(int)storeMenu].transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
            panels[(int)storeMenu].blocksRaycasts = true;
        }
        public void CloseMenu(UIPanels storeMenu)
        {
            panels[(int)storeMenu].DOFade(0f, 0.5f).SetUpdate(true);
            //panels[(int)storeMenu].transform.DOScale(0,0.5f).SetEase(Ease.Linear);
            panels[(int)storeMenu].blocksRaycasts = false;
        }
    }
}