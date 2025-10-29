using FomeCharacters;
using System.Collections.Generic;
using UnityEngine;

public class CombatSphereDetector : MonoBehaviour
{
    public UnitController player;
    private List<UnitController> unitControllersInStage;
    private List<UnitController> targetsInStage = new List<UnitController>();
    private List<UnitController> targetsInCombatSphere = new List<UnitController>();

    private Armament armament;
    private float armamentRange;

    private void Awake()
    {
        Event.OnPlayerSpawn += GetPlayerRef;
        Debug.Log("Detector Awakening");
    }
    private void Start()
    {
        unitControllersInStage = GameManager.Instance.unitControllersInStage;

        Event.OnArmamentSelectionChange += getAllTargetsInRangeOfArmament;
    }

    private void GetPlayerRef(UnitController _player)
    {
        Debug.Log("Detector GETPLAYERREF");
        player = _player;
    }

    private void getAllTargets()
    {
        targetsInStage.Clear();
        foreach (UnitController _target in unitControllersInStage)
        {
            if(_target.getCharacterType() != UnitController.CharacterType.Player &&
                _target.getCharacterType() != UnitController.CharacterType.Part)
            {
                targetsInStage.Add(_target);
            }
        }

        //foreach (UnitController _target in targetsInStage)
        //{
        //    Debug.Log("==============");
        //    Debug.Log(_target.GetName());
        //    Debug.Log("==============");
        //}
    }
    private void getArmamentInfo(int _selectedArmament)
    {
        armament = player.playerArmaments[_selectedArmament];
        armamentRange = armament.GetRange();
    }

    public void getAllTargetsInRangeOfArmament(int _selectedArmament)
    {
        getAllTargets();
        getArmamentInfo(_selectedArmament);
        targetsInCombatSphere.Clear();

        foreach (UnitController _target in targetsInStage)
        {
            float distance = Vector3.Distance(_target.GetUnitPos(), player.GetUnitPos());
            
            if (distance <= armamentRange)
            {
                targetsInCombatSphere.Add(_target.GetComponent<UnitController>());
            }
        }

        if (targetsInCombatSphere.Count == 0)
        {
            Event.ToggleNoTargetsInSphere?.Invoke(true);
        }
        else
        {
            Event.ToggleNoTargetsInSphere?.Invoke(false);
        }

        Event.OnTargetDetectionFinish?.Invoke(targetsInCombatSphere);
        //Event.OnDetectionFinish?.Invoke();
    }
}
