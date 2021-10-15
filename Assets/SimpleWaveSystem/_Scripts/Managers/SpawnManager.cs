using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RehtseStudio.SimpleWaveSystem.Managers
{

    public class SpawnManager : MonoBehaviour
    {

        [Header("Waves properties")]
        private int _waveNumber;
        private int _currentObjectToSpawnOnScene;
        private int _objectDestroyed = 0;

        [Header("Wait seconds between object spawn")]
        private WaitForSeconds _objectSpawnWaitForSeconds;
        [SerializeField] private float _waitForSecondsBetweenObjectSpawn = 3f;
        
        [Header("Wait second between waves")]
        private WaitForSeconds _nextWaveRoutineWaitForSeconds;
        [Range(0f,10f)]
        [SerializeField] private float _waitForSecondBetweenWaves = 3f;

        [Header("Wave Text reference")]
        [SerializeField] private Text _waveSystemText;

        [Header("Position / destination where the objects are going to spawn")]
        [SerializeField] private Vector3 _objectPosition;

        //Getting Reference to the WaveSystemManager Script and PoolManager Script
        private WaveSystemManager _waveSystemManager;
        private PoolManager _poolManager;
        private UIManager _uiManager;

        private void Start()
        {

            _waveSystemManager = GetComponent<WaveSystemManager>();
            _poolManager = GetComponent<PoolManager>();
            _uiManager = GameObject.Find("UI").GetComponent<UIManager>();

            _objectSpawnWaitForSeconds = new WaitForSeconds(_waitForSecondsBetweenObjectSpawn);
            _nextWaveRoutineWaitForSeconds = new WaitForSeconds(_waitForSecondBetweenWaves);

        }

        public void StartWave()
        {
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

                //You can change the direcction of the object you want to spawn
                //You can change this part of the code
                newObject.transform.position = _objectPosition;
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
                    _uiManager.ActivateRestartSpawnButton();
                }
                else
                {
                    _waveSystemText.text = "Wave: " + (_waveNumber + 1) + " / " + _waveSystemManager.WavesReferenceLenght();
                    StartCoroutine(NextWaveRoutine());
                }
            }
        }

        //You can access this Public Method if you like to change the positon of the object
        public void ObjectPositoinToSpawn(Vector3 _objPos)
        {
            _objectPosition = _objPos;
        }

    }

}

