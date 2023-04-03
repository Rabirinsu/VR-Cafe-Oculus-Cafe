using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject[] _prefabs;
    private float _lastSpawnTime;
    public float spawnInterval = 2f;
    public Transform target;
    private bool _spawning = false;

    void Start()
    {
        _prefabs = Resources.LoadAll<GameObject>("CollectablePrefabs");
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawning && Time.time > _lastSpawnTime)
        {
            _lastSpawnTime = Time.time + spawnInterval;
            SpawnCollectable();
        }
    }

    void SpawnCollectable()
    {
        var randomIndex = Random.Range(0, _prefabs.Length);
        var collectable = Instantiate(_prefabs[randomIndex], transform.position, Quaternion.identity);
        collectable.GetComponent<Collectable>().SetTarget(target);
    }

    public void StartSpawn()
    {
        _spawning = true;
    }

    public void StopSpawn()
    {
        _spawning = false;
    }
}