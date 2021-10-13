using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace RehtseStudio.Managers
{

    public class PoolManager : MonoBehaviour
    {

        private GameObject _objectContainer;
        private int _poolObjectSize = 20;

        private Dictionary<int, List<GameObject>> _objectDictionary = new Dictionary<int, List<GameObject>>();     

        //Getting Reference to the WaveSystemManager Script
        private WaveSystemManager _waveSystemManager;

        private void Start()
        {
            _objectContainer = GameObject.Find("[--Container--]");
            _waveSystemManager = GetComponent<WaveSystemManager>();
            CreateListOfObjects();
            GeneratePool();
        }

        //Creating the list in order to create the pool for each objec(enemies, item, etc)
        private void CreateListOfObjects()
        {
            //Getting the Lenght of how many wave we are working with
            for (int i = 0; i < _waveSystemManager.WavesReferenceLenght(); i++)
            {
                //Getting the lenght to know how many object are in the wave to create the list
                for (int w = 0; w < _waveSystemManager.ObjectOnTheWaveLenght(i); w++)
                {
                    //Check if the object id is already in use is the Dictionary
                    //If the object id is not on the Dictionary create a new list for that object
                    var isObjectIDonTheDictionary = _objectDictionary.ContainsKey(_waveSystemManager.IdOfTheObject(i,w));
                    if (isObjectIDonTheDictionary == false)
                        _objectDictionary.Add(_waveSystemManager.IdOfTheObject(i, w), new List<GameObject>());
                }
            }

        }

        private void GeneratePool()
        {
            for (int i = 0; i < _waveSystemManager.WavesReferenceLenght(); i++)
            {
                for (int w = 0; w < _waveSystemManager.ObjectOnTheWaveLenght(i); w++)
                {
                    for (int p = 0; p < _poolObjectSize; p++)
                    {
                        if (_objectDictionary[_waveSystemManager.IdOfTheObject(i, w)].Count != _poolObjectSize)
                        {
                            GameObject newObject = Instantiate(_waveSystemManager.GetGameObjectFromWave(i, w));
                            newObject.transform.SetParent(_objectContainer.transform);
                            newObject.SetActive(false);
                            _objectDictionary[_waveSystemManager.IdOfTheObject(i, w)].Add(newObject);
                        }
                    }
                }
            }
        }

        //The SpawnManager will comunicate with the PoolManager in order to call the object to spawn
        public GameObject RequestObjectToSpawn(int objectId)
        {
            var getObject = _objectDictionary[objectId].FirstOrDefault((obj) => obj.activeInHierarchy == false);

            if(getObject != null)
            {
                return getObject;
            }

            return null;
        }

    }

}

