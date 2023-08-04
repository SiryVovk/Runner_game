using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    [SerializeField] private Transform nextSpawnPointTransform;

    private void OnEnable()
    {
        RoadMover.Instance.SetNextSpawnPointTransform(nextSpawnPointTransform);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<PlayerPosition>(out _))
        {
            RoadMover.Instance.SpawnNextRoadTile(1);
            DisableRoad();
        }
    }

    private void DisableRoad()
    {
        this.gameObject.SetActive(false);
    }
}
