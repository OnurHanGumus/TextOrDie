using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData
    {
        public float Speed = 5;
        public Vector3 InitialPos = new Vector3(0, 0.5f, 5);
    }
}