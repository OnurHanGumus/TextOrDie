using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelData
    {
        public Vector3[] OtherPlayerPositions = { new Vector3(-6f, 0.5f, 5f), new Vector3(-3f, 0.5f, 5f), new Vector3(3f, 0.5f, 5f), new Vector3(6f, 0.5f, 5f) };
        public int InitializeBlockCounts = 5;

    }
}