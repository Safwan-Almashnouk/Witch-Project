using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    public List<Pool> pools;

    private Dictionary<PoolTag, Dictionary<string, Queue<GameObject>>> poolDictionary;

    void Awake()
    {
        if (SharedInstance == null)
            SharedInstance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        poolDictionary = new Dictionary<PoolTag, Dictionary<string, Queue<GameObject>>>();

        foreach (Pool pool in pools)
        {
            if (!poolDictionary.ContainsKey(pool.tag))
            {
                poolDictionary[pool.tag] = new Dictionary<string, Queue<GameObject>>();
            }

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary[pool.tag][pool.prefabKey] = objectPool;
        }
    }

    public GameObject SpawnFromPool(PoolTag tag, string prefabKey, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag) || !poolDictionary[tag].ContainsKey(prefabKey))
        {
           
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag][prefabKey].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag][prefabKey].Enqueue(objectToSpawn); // Requeue for reuse

        return objectToSpawn;
    }
}
