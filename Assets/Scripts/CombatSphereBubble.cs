using FomeCharacters;
using System.Collections.Generic;
using UnityEngine;

public class CombatSphereBubble : MonoBehaviour
{
    private UnitController player;

    private float sphereMaxSize;
    private float sphereMinSize = 1;
    private int resizeDirection = 1;
    private float sphereSingleVector;

    [SerializeField] GameObject combatSphere;
    [SerializeField] float sphereSpeedScaler = 1;

    private int doubleRadiusForDiameter = 2;
    private bool subscribedToUpdate = false;

    List<Armament> armaments;
    private Armament armament;
    private void Awake()
    {
        //Debug.Log("CombatController awake");
        Event.OnPlayerSpawn += GetPlayerRef;
    }
    private void Start()
    {
        sphereSingleVector = sphereMinSize;
        SetCombatSphereSmall();

        Event.OnArmamentSelectionChange += OpenSphere;
        Event.OnCombatSphereClose += CloseSphere;
    }

    private void GetPlayerRef(UnitController _player)
    {
        player = _player;
        armaments = player.playerArmaments;
    }

    private float GetRange(int _selected)
    {
        return armaments[_selected].GetRange();
    }

    private void OpenSphere(int _selected)
    {
        SetCombatSphereSmall();
        sphereMaxSize = GetRange(_selected) * doubleRadiusForDiameter;
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
