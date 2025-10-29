using FomeCharacters;
using System.Collections.Generic;
using UnityEngine;
public class PartManager : MonoBehaviour 
{
    public List<UnitController> parts = new List<UnitController>();

    public void AddPart(UnitController part)
    {
        parts.Add(part);
    }
    public List<UnitController> GetParts()
    {
        return parts;
    }
}
