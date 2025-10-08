using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Part : MonoBehaviour
{
    [SerializeField] string name;
    List<GameObject> listOfVisualComponents = new List<GameObject>();
    //[SerializeField]
    private void Start()
    {

    }
    public string GetName()
    {
        return name;
    }


}
