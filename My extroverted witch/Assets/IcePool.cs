using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePool : MonoBehaviour
{
   public static IcePool IceInstance;
    [SerializeField] List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 5;

    [SerializeField] private GameObject icebullet;

    private void Awake()
    {
        if (IceInstance == null) 
        {
            IceInstance = this;
        }  
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)   
        {
            GameObject obj = Instantiate(icebullet);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }


    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
