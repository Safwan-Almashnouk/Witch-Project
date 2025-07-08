using UnityEngine;

public enum PoolTag
{
    EnemyBullet,
    FireBullet,
    IceBullet,
    PlayerProjectile,
    Explosion,
   
}

[System.Serializable]
public class Pool
{
    public PoolTag tag;
    public string prefabKey;
    public GameObject prefab;
    public int size;
}