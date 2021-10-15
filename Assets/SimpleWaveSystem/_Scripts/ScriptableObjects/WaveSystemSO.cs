using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RehtseStudio.SimpleWaveSystem.SO
{

    [System.Serializable]
    public class WaveSetUp
    {

        public int objectID;
        public GameObject gameObjectToSpawn;

    }

    [CreateAssetMenu(menuName = "Rehtse Studio Simple Wave System/Create New Wave")]
    public class WaveSystemSO : ScriptableObject
    {

        [Header("Type of object on the wave")]
        public WaveSetUp[] objectToSpawnOnThisWave;

        [Header("Amount of objects to spawn on this wave")]
        public int amountToSpawnOnThisWave;

        public int ReturnObjectType()
        {

            var nextObjectType = objectToSpawnOnThisWave[UnityEngine.Random.Range(0, objectToSpawnOnThisWave.Length)].objectID;
            return nextObjectType;

        }

    }

}

