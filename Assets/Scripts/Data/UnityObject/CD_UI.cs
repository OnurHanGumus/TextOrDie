using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_UI", menuName = "Picker3D/CD_UI", order = 0)]
    public class CD_UI : ScriptableObject
    {
        public UIData Data;
    }
}