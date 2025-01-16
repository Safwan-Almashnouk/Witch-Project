using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float damage;
   

    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 7);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }


}
