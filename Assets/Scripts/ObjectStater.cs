using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stater", menuName = "Custom/Stater")]
public class ObjectStater : ScriptableObject
{
    public List<ObjectState> objects = new List<ObjectState>();
}

[System.Serializable]
public class ObjectState
{
    [SerializeField]public string id = System.Guid.NewGuid().ToString();
    [SerializeField]public string name;
    [SerializeField]public GameObject state_Normal;
    [SerializeField]public GameObject state_Fantasy;
    [SerializeField]public GameObject state_Cyber;
}