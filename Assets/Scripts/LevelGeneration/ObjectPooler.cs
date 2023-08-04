using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private ObjectPoolScriptableObject pool;

    private List<GameObject> poolObjects;
    private int poolAmount;

    private List<GameObject> poolList;

    private void Awake()
    {
        poolObjects = pool.poolObject;
        poolAmount = pool.poolAmount;

        poolList = new List<GameObject>();

        for(int i = 0; i < poolObjects.Count - 1; i++)
        {
            GameObject obj = Instantiate(poolObjects[i]);

            obj.transform.SetParent(transform, true);
            obj.SetActive(false);
            poolList.Add(obj);
        }

        ActivateFirstRoads();
    }

    private void ActivateFirstRoads()
    {
        RoadMover.Instance.SpawnNextRoadTile(poolAmount);
    }
    public void ActivatePoolObjects(int amountToActivate)
    {
        for(int i = 0; i < amountToActivate; i++)
        {
            poolList[i].SetActive(true);
        }
    }

    public GameObject GetPooledObject()
    {
        var notActiveObjects = poolList.Where(obj => obj.activeInHierarchy == false);

        if (notActiveObjects.Count() > 0)
        {
            int index = Random.Range(0, notActiveObjects.Count());
            return notActiveObjects.ElementAt(index);
        }

        return null;
    }
}
