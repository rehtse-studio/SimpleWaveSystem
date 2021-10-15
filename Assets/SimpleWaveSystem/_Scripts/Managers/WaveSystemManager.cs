using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using RehtseStudio.SimpleWaveSystem.SO;
//

namespace RehtseStudio.SimpleWaveSystem.Managers
{

    public class WaveSystemManager : MonoBehaviour
    {

        [SerializeField] private WaveSystemSO[] _wavesReference;

        public int WavesReferenceLenght()
        {
            return _wavesReference.Length;
        }

        public int ObjectOnTheWaveLenght(int waveId)
        {
            return _wavesReference[waveId].objectToSpawnOnThisWave.Length;
        }

        public int IdOfTheObject(int waveId, int objectId)
        {
            return _wavesReference[waveId].objectToSpawnOnThisWave[objectId].objectID;
        }

        public GameObject GetGameObjectFromWave(int waveId, int objectId)
        {
            return _wavesReference[waveId].objectToSpawnOnThisWave[objectId].gameObjectToSpawn;
        }
        //
        public int AmountOfObjectToSpawnInThisWave(int waveId)
        {
            return _wavesReference[waveId].amountToSpawnOnThisWave;
        }

        public int ReturnObjectTypeId(int waveId)
        {
            return _wavesReference[waveId].ReturnObjectType();
        }

    }

}

