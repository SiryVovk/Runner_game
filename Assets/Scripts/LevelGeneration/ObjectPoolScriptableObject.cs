using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectPoolScriptableObject", order = 1)]
public class ObjectPoolScriptableObject : ScriptableObject
{
    public List<GameObject> poolObject;
    public int poolAmount;
}
