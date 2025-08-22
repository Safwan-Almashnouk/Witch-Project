using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalUIManager : MonoBehaviour
{
   public List<GameObject> ElementTypes = new List<GameObject>();
   private Dictionary<string, GameObject> objectsDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        
        foreach (var obj in ElementTypes)
        {
            if (obj != null && !objectsDict.ContainsKey(obj.name))
            {
                objectsDict.Add(obj.name, obj);
            }
        }
    }

    public void SetActiveObject(string name)
    {
        foreach (var kvp in objectsDict)
        {
            kvp.Value.SetActive(kvp.Key == name);
        }
    }
}


