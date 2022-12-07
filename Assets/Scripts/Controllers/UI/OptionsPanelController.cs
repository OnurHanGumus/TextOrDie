using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private AudioSource audioSource;
    #endregion
    #region Private Variables
    private bool _audioSourceActiveness;
    #endregion
    #endregion

    private void Start()
    {
        _audioSourceActiveness = SaveSignals.Instance.onGetSoundState(SaveLoadStates.SoundState, SaveFiles.GameOptions) == 1;
        soundToggle.isOn = _audioSourceActiveness;
        EnableAudioSource();
    }
    public void OnValueChanged()
    {
        SaveSignals.Instance.onChangeSoundState?.Invoke(soundToggle.isOn ? 1 : 0, SaveLoadStates.SoundState, SaveFiles.GameOptions);
        _audioSourceActiveness = !_audioSourceActiveness;
        EnableAudioSource();
    }
    public void CloseOptionsPanel()
    {
        UISignals.Instance.onClosePanel?.Invoke(UIPanels.OptionsPanel);
        UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }
    private void EnableAudioSource()
    {
        audioSource.enabled = _audioSourceActiveness;
    }


}
