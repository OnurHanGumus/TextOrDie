using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class UIData
    {
        public float QuestionPanelScreenPosY = 668f, AnswerPanelScreenPosX = 0f, EnemyAnswerPanelScreenPosX = 0;
        public float QuestionPanelClosePosY = 1250f, AnswerPanelClosePosX = -1100f, EnemyAnswerPanelClosePosX = 1100f;
        public float UIAnimationDelay = 0.5f;
    }
}