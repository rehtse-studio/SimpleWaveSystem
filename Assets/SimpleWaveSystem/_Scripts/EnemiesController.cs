using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using RehtseStudio.Managers;


namespace RehtseStudio.Enemy
{

    public class EnemiesController : MonoBehaviour
    {

        private SpawnManager _spawnManager;

        [SerializeField] private Vector2 _destination;

        [SerializeField] private float _speed = 0.5f;

        private void OnEnable()
        {
            _spawnManager = GameObject.Find("Managers").GetComponent<SpawnManager>();
        }

        private void Update()
        {
            transform.Translate(_destination * _speed * Time.deltaTime);
        }
        private void OnMouseDown()
        {
            _spawnManager.ObjectWaveCheck();
            this.gameObject.SetActive(false);
        }

    }

}

