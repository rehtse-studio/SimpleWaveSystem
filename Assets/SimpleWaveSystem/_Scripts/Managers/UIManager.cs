using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RehtseStudio.SimpleWaveSystem.Managers
{
    public class UIManager : MonoBehaviour
    {

        private SpawnManager _spawnManager;

        [SerializeField] private GameObject _restartButton;
        private void Start()
        {
            _spawnManager = GameObject.Find("Managers").GetComponent<SpawnManager>();
        }

        public void StartSpawnButton()
        {
            _spawnManager.StartWave();
        }

        public void ActivateRestartSpawnButton()
        {
            _restartButton.SetActive(true);
        }


    }

}

