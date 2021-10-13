using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RehtseStudio.Managers
{

    public class SpawnManager : MonoBehaviour
    {

        [Header("Waves properties")]
        [SerializeField] private int _waveNumber;
        [SerializeField] private int _currentObjectToSpawnOnScene;
        [SerializeField] private int _objectDestroyed = 0;

        [Header("Wait seconds between object spawn")]
        private WaitForSeconds _objectSpawnWaitForSeconds;
        [SerializeField] private float _waitForSecondsBetweenObjectSpawn = 3f;
        
        [Header("Wait second between waves")]
        private WaitForSeconds _nextWaveRoutineWaitForSeconds;
        [Range(0f,10f)]
        [SerializeField] private float _waitForSecondBetweenWaves = 3f;

        [Header("Wave Text reference")]
        [SerializeField] private Text _waveSystemText;

        //Getting Reference to the WaveSystemManager Script and PoolManager Script
        private WaveSystemManager _waveSystemManager;
        private PoolManager _poolManager;

        private void Start()
        {

            _waveSystemManager = GetComponent<WaveSystemManager>();
            _poolManager = GetComponent<PoolManager>();

            _objectSpawnWaitForSeconds = new WaitForSeconds(_waitForSecondsBetweenObjectSpawn);
            _nextWaveRoutineWaitForSeconds = new WaitForSeconds(_waitForSecondBetweenWaves);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartObjectWave();
        }

        private void StartObjectWave()
        {
            _waveSystemText.text = "Wave: " + (_waveNumber + 1) + " / " + _waveSystemManager.WavesReferenceLenght();
            StartCoroutine(NextWaveRoutine());
        }

        private IEnumerator NextWaveRoutine()
        {
            yield return _nextWaveRoutineWaitForSeconds;
            StartCoroutine(SpawnObjectRoutine());
        }

        private IEnumerator SpawnObjectRoutine()
        {
            _currentObjectToSpawnOnScene = _waveSystemManager.AmountOfObjectToSpawnInThisWave(_waveNumber);

            for (int i = 0; i < _currentObjectToSpawnOnScene; i++)
            {
                //Get the object to pass to the PoolManager
                var selectedObject = _waveSystemManager.ReturnObjectTypeId(_waveNumber);
                GameObject newObject = _poolManager.RequestObjectToSpawn(selectedObject);

                newObject.transform.position = Vector3.zero;
                newObject.SetActive(true);

                yield return _objectSpawnWaitForSeconds;
            }

        }

        public void ObjectWaveCheck()
        {
            _objectDestroyed++;

            if(_objectDestroyed == _currentObjectToSpawnOnScene)
            {
                _objectDestroyed = 0;
                _waveNumber++;

                if((_waveNumber + 1) > _waveSystemManager.WavesReferenceLenght())
                {
                    _waveSystemText.text = "No more Object to spawn";
                    _waveNumber = 0;
                    _objectDestroyed = 0;
                }
                else
                {
                    _waveSystemText.text = "Wave: " + (_waveNumber + 1) + " / " + _waveSystemManager.WavesReferenceLenght();
                    StartCoroutine(NextWaveRoutine());
                }
            }
        }

    }

}

