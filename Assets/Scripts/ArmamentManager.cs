using System.Collections.Generic;
using UnityEngine;

public class ArmamentManager : MonoBehaviour
{
    private List<Armament> armaments = new List <Armament>();

    private void Start()
    {
        Armament Cannon = new Armament("Cannon", 10);
        Armament Archer = new Armament("Archer", 15);
        armaments.Add(Cannon);
        armaments.Add(Archer);
    }
    public void addArmament(Armament _armamaent)
    { 
        armaments.Add(_armamaent);
    }
    public void removeArmament(Armament _armamaent)
    {
        armaments.Remove(_armamaent);
    }

    public List<Armament> getArmaments()
    {
        return armaments;
    }

    //public Armament GetItemsInList(int num)
    //{
    //    return armaments[num];
    //}



}
