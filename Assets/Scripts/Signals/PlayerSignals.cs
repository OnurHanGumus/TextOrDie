using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<int> onPlayerAnsweredRight = delegate { };
        public UnityAction onPlayerAnsweredWrong = delegate { };
        public UnityAction onWaterRising = delegate { };
        public UnityAction<float> onBlockRisingEnd = delegate { };
        public UnityAction<Vector3> onInteractedWithWater = delegate { };
    }
}