using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };
        public Func<int> onGetLevel = delegate { return 0; };
        public UnityAction onEnemyDie = delegate { };
        public UnityAction onPlayerInWater = delegate { };
        public UnityAction onWaterRising = delegate { };
        public UnityAction<float> onWaterRised = delegate { };
        public UnityAction<float> onBlockRisingEnd = delegate { };
        public UnityAction onTargetsAreCleared = delegate { };
    }
}