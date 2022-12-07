using Enums;
using Extentions;
using System;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public Func<int> onGetMoney = delegate { return 0; };
        public Func<int> onGetGem = delegate { return 0; };
        public Func<int> onGetScore = delegate { return 0; };

        public UnityAction<ScoreTypeEnums, int> onScoreIncrease = delegate { };
        public UnityAction<ScoreTypeEnums, int> onScoreDecrease = delegate { };
        public UnityAction<int> onHighScoreChanged = delegate { };



    }
}