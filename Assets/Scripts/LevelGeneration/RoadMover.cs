using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMover : MonoBehaviour
{
    [SerializeField] private ObjectPooler roadPool;

    [SerializeField] private Transform nextSpawnTransform;
    [SerializeField] private Vector3 pos;

    private static RoadMover instance;
    public static RoadMover Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.Log("Road mover is null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetNextSpawnPointTransform(Transform transform)
    {
        nextSpawnTransform = transform;
        pos = transform.position;
    }

    public void ActivatePoolObjects(int amount)
    {
        roadPool.ActivatePoolObjects(amount);
    }

    public void SpawnNextRoadTile(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var roadTile = roadPool.GetPooledObject();
            roadTile.transform.position = nextSpawnTransform.position;
            roadTile.transform.rotation = nextSpawnTransform.rotation;
            roadTile.SetActive(true);
        }
    }
}
