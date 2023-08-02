using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private ObjectPoolScriptableObject pool;

    private List<GameObject> poolObjects;
    private int poolAmount;
    private float roadZLength = 10;

    private List<GameObject> poolList;

    private void Awake()
    {
        poolObjects = pool.poolObject;
        poolAmount = pool.poolAmount;

        poolList = new List<GameObject>();

        Transform lastPosition = null;

        for(int i = 0; i < poolObjects.Count - 1; i++)
        {
            GameObject obj = Instantiate(poolObjects[i]);

            if (i > poolAmount)
            {
                obj.transform.SetParent(transform, true);
                obj.SetActive(false);
                poolList.Add(obj);
            }
            else
            {
                obj.transform.SetParent(transform, true);
                obj.SetActive(true);
                Vector3 position = obj.transform.position;
                position.z = roadZLength * i;
                obj.transform.position = position;
                poolList.Add(obj);
                if(i == poolAmount)
                {
                    lastPosition = obj.transform;
                }
            }
        }

        if (lastPosition != null)
        {
            RoadMover.Instance.SetNextSpawnPointTransform(lastPosition);
        }
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
