using FomeCharacters;
using System.Collections.Generic;
using UnityEngine;

public class CombatSphereBubble
{
    private float sphereMaxSize;
    private float sphereMinSize = 1;
    private float sphereSpeedScaler = 1;
    private int resizeDirection = 1;
    private float sphereSingleVector;

    private GameObject combatSphere;
    private bool combatSphereOn = false;
    private int selectedArmament;

    private int doubleRadiusForDiameter = 2;
    private bool subscribedToUpdate = false;

    List<Armament> listOfArmaments;
    public CombatSphereBubble(
        List<Armament> _listOfArmaments,
        int _selectorPositionInList,
        float _sphereSpeedScaler,
        GameObject _combatSphere)
    {
        listOfArmaments = _listOfArmaments;
        sphereSpeedScaler = _sphereSpeedScaler;
        combatSphere = _combatSphere;
        selectedArmament = _selectorPositionInList;

        sphereMaxSize = GetRange(); 

        sphereSingleVector = sphereMinSize;
        SetCombatSphereSmall();

        Event.OnArmamentSelectionChange += OpenSphere;
        Event.OnCombatSphereClose += CloseSphere;
    }

    private float GetRange()
    {
        return listOfArmaments[selectedArmament].GetRange();
    }

    private void OpenSphere(int _selected)
    {
        SetCombatSphereSmall();
        selectedArmament = _selected;
        sphereMaxSize = GetRange() * doubleRadiusForDiameter;
        resizeDirection = 1;
        if(subscribedToUpdate == false)
        {
            Event.OnUpdate += ResizeCombatSphere;
        }
        subscribedToUpdate = true;
    }

    private void CloseSphere()
    {
        resizeDirection = -1;
        Event.OnUpdate += ResizeCombatSphere;
    }

    private void UnsubscribeToUpdate()
    {
        Event.OnUpdate -= ResizeCombatSphere;
        subscribedToUpdate = false;
    }

    public void StartResizingCombatSphere(int _direction)
    {
        resizeDirection = _direction;
    }
    private void MaintainCombatSphereBounds()
    {
        if (sphereSingleVector > sphereMaxSize)
        {
            sphereSingleVector = sphereMaxSize;
        }
        else
        {
            sphereSingleVector = sphereMinSize;
        }

        SetCombatSphereSize();
    }

    private void SetCombatSphereLarge()
    {
        sphereSingleVector = sphereMaxSize;
        SetCombatSphereSize();
    }
    private void SetCombatSphereSmall()
    {
        sphereSingleVector = sphereMinSize;
        SetCombatSphereSize();
    }

    private void SetCombatSphereSize()
    {
        combatSphere.transform.localScale = new Vector3(sphereSingleVector, sphereSingleVector, sphereSingleVector);
    }
    private void ResizeCombatSphere()
    {
        sphereSingleVector += Time.deltaTime * (sphereSpeedScaler * resizeDirection);

        if (sphereSingleVector < sphereMinSize || sphereSingleVector > sphereMaxSize)
        {
            MaintainCombatSphereBounds();
            UnsubscribeToUpdate();
        }
        else
        {
            SetCombatSphereSize();
        }
    }
}
