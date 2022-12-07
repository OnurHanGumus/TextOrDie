using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    public void ClosePausePanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.PausePanel);
        Time.timeScale = 1f;
    }
    public void MainMenuBtn()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.PausePanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
        Time.timeScale = 1f;

    }
    public void ExitBtn()
    {
        Application.Quit();
    }


}
