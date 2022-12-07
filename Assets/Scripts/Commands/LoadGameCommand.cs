using Enums;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Commands
{
    public class LoadGameCommand
    {
        public int OnLoadGameData(SaveLoadStates saveLoadStates, string fileName = "SaveFile")
        {

            if (!ES3.FileExists(fileName + ".es3")) { return 0; }
            return ES3.Load<int>(saveLoadStates.ToString(), fileName + ".es3", 0);
        }
    }
}
