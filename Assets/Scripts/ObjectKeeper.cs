using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ObjectKeeper : MonoBehaviour
{
    public ObjectStater objectStater;

    public GameObject objPanelPrefab;

    public GameObject parentObj;

    public List<CurrentCustomObject> allChangableObjects = new List<CurrentCustomObject>();
    [SerializeField] public List<GameObject> objStatePanels = new List<GameObject>();

    
    private void Start()
    {
        for (int i = 0; i < allChangableObjects.Count; i++)
        {
            GameObject objStatePanel = Instantiate(objPanelPrefab, parentObj.transform);
            ObjectChanger objectChanger = objStatePanel.GetComponent<ObjectChanger>();

            objectChanger.objectKeeper = this;

            string objNametmp = allChangableObjects[i].curObject.name;
            objectChanger.name = objNametmp.Substring(0, objNametmp.Length - 6);
            objectChanger.objName.text = objNametmp.Substring(0, objNametmp.Length - 6);
            //            print(objNametmp.Substring(0, objNametmp.Length - 6));
            objectChanger.currentCustomObject = allChangableObjects[i];

            objStatePanels.Add(objStatePanel);
        }
    }

    public CurrentCustomObject GetObjectByName(string name)
    {
        for (int i = 0; i < allChangableObjects.Count; i++)
        {
            //string objNametmp = allChangableObjects[i].curObject.name;
            if (allChangableObjects[i].curObject.name.StartsWith(name))
            {
                //print("Object with name " + name + " found in ObjectKeeper.");
                return allChangableObjects[i];
            }
        }
        //print("Object with name " + name + " not found in ObjectKeeper.");
        return null;
    }

    public static bool FindFullWord(string search, string word)
    {
        if (search == word || search.StartsWith(word + " "))
        {
            return true;
        }
        else if (search.EndsWith(" " + word))
        {
            return true;
        }
        else if (search.Contains(" " + word + " "))
        {
            return true;
        }
        else {
            return false;
        }
    }
}


[System.Serializable]
public class CurrentCustomObject
{
    [SerializeField]public GameObject curObject;
    [SerializeField]public string state = "Normal";
}