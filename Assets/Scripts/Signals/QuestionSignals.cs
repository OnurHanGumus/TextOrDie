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
        public Func<string> onGetRandomAnswer = delegate { return ""; };

        public UnityAction<string> onPlayerHitEnterButton = delegate { };
        public UnityAction<int> onAskQuestion = delegate { };
        public UnityAction<string> onSendAnswerToPanel = delegate { };
        public UnityAction onShowAnswerPanel = delegate { };

    }
}