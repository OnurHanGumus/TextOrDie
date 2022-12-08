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
        public Func<int> onGetQuestionId = delegate { return 0; };
        public UnityAction<string> onPlayerHitEnterButton = delegate { };
        public UnityAction<int> onAskQuestion = delegate { };

    }
}