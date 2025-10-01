using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ArmamentController : MonoBehaviour
{
    public List<Armament> armaments = new List <Armament>();

    public void AddArmament(Armament _armamaent)
    { 
        armaments.Add(_armamaent);
    }

    //public Armament GetItemsInList(int num)
    //{
    //    return armaments[num];
    //}



}
