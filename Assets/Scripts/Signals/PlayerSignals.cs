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
        public UnityAction<int,string> onPlayerAnsweredRight = delegate { };
        public UnityAction onPlayerAnsweredWrong = delegate { };
        public UnityAction<Transform> onInteractedWithWater = delegate { };
    }
}