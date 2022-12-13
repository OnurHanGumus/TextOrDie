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
        public Func<int> onGetTotalQuestionCount = delegate { return 100; };
        public Func<int> onGetEnemtId = delegate { return 0; };
        public Func<int,string> onGetWord = delegate { return ""; };
        public Func<string> onPlayerGetWord = delegate { return ""; };

        public UnityAction<string> onPlayerHitEnterButton = delegate { };
        public UnityAction<int> onAskQuestion = delegate { };
        public UnityAction<string> onSendAnswerToPanel = delegate { };
        public UnityAction onShowAnswerPanel = delegate { };
        public UnityAction<int,string> onEnemyChoosedWord = delegate { };
        public UnityAction<string> onPlayerChoosedWord = delegate { };
        public UnityAction onBlocksFirstMove = delegate { };

    }
}