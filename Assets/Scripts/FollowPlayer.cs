using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float offsetZ = -3;
    [SerializeField] private float offsetY = 4;
    [SerializeField] private GameObject toFollow;

    private void LateUpdate()
    {
        Vector3 newPosition = toFollow.transform.position;
        newPosition.z += offsetZ;
        newPosition.y = offsetY;
        transform.position = newPosition;
    }
}
