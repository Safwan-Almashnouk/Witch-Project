using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    [SerializeField] private float health, lifetime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lifetime)
        {
           Destroy(gameObject);
        }
    }
}
