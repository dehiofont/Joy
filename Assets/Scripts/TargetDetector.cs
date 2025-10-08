using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using FomeCharacters;

public class TargetDetector
{
    public event Action<Armament, List<Armament>> OnDetectionFinish;

    private float armamentRange;

    public List<UnitController> targetsInCombatSphere;
    public List<UnitController> listOfAllPotentialTargets;

    public TargetDetector(List<UnitController> _listOfAllPotentialTargets, List<UnitController> _targetsInCombatSphere)
    {
        listOfAllPotentialTargets = _listOfAllPotentialTargets;
        targetsInCombatSphere = _targetsInCombatSphere;
    }
    public void getAllTargetsInRangeOfArmament(float _armamentRange)
    {
        targetsInCombatSphere.Clear();
        foreach (UnitController target in listOfAllPotentialTargets)
        {
            float distance = Vector3.Distance(target.GetUnitPos(), GameManager.Instance.PlayerUnitController.GetUnitPos());
            
            Debug.Log("--------");
            Debug.Log($"{target} is {distance} from the player and the current range is {_armamentRange}");
            
            if (distance <= _armamentRange)
            {
                targetsInCombatSphere.Add(target.GetComponent<UnitController>());
            }
        }
        Debug.Log("--------");
        for (int i = 0; i < targetsInCombatSphere.Count; i++)
        {
            Debug.Log(targetsInCombatSphere[i]);
        }
        Debug.Log("--------");

        if (targetsInCombatSphere.Count == 0)
        {
            Event.OnNoTargetsInSphere();
            //Event.OnUnitControllersFound();
        }
    }
}
