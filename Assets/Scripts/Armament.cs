using UnityEngine;

public class Armament
{
    private string name;
    private float range;
    public Armament(string _name, float _range)
    {
        name = _name; 
        range = _range;        
    }

    public string GetName()
    {
        return name;
    }

    public float GetRange()
    {
        return range;
    }
}
