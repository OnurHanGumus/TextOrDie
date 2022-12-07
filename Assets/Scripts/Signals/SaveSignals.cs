using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction<int, SaveLoadStates, SaveFiles> onSaveScore = delegate { };
        public UnityAction<int, SaveLoadStates, SaveFiles> onChangeSoundState = delegate { };

        public Func<SaveLoadStates, SaveFiles, int> onGetScore = delegate { return 0; };
        public Func<SaveLoadStates, SaveFiles, int> onGetSoundState = delegate { return 0; };




    }
}