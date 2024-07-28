using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    public static PoolSpawner Instance;

    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int amountToSpawn;

    private List<GameObject> poolList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolList = new List<GameObject>();
        PoolObjects();
    }

    private void PoolObjects()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject _pooledObj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            _pooledObj.SetActive(false);
            poolList.Add(_pooledObj);
        }
    }

    public GameObject SpawnPooledObjects()
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                Debug.Log(poolList[i]);
                return poolList[i];
            }
        }
        return null;
    }
}
