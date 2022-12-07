using Enums;
using Extentions;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Signals
{
    public class QuestionSignals : MonoSingleton<QuestionSignals>
    {
        public Func<int,string> onGetQuestion = delegate { return ""; };

    }
}