using Assets.Scripts;
using FomeCharacters;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class WeaponLineController : MonoBehaviour
{
    private List<UnitController> targetsInCombatSphere = new List<UnitController>();

    bool lineRenderersActive = false;

    private int selectedArmamentSlotNum;
    private Armament selectedArmament;
    private UnitController selectedTarget;
    private UnitController selectedPart;
    private UnitController selectedUnit;

    [SerializeField] List<LineRenderer> weaponLineRenderers = new List<LineRenderer>();
    [SerializeField] List<WeaponLine> weaponLines = new List<WeaponLine>();

    [SerializeField] float maxWeaponDistance;
    UnitController player;
    List<Armament> armaments;

    private void Awake()
    {
        Event.OnPlayerSpawn += GetPlayerRef;
    }
    private void Start()
    {
        KillLineRenderers();

        Event.OnArmamentSelectionChange += SetSelectedArmament; 
        Event.OnTargetSelectionChange += SetSelectedTarget;
        Event.OnPartSelectionChange += SetSelectedPart; 
        Event.OnTargetDetectionFinish += GetTargetsInCombatSphere;
        Event.OnProjectileFire += SetWeaponLineActive;
        Event.OnCombatSphereClose += KillLineRenderers;
    }
    private void SetWeaponLineActive()
    {
        weaponLines[selectedArmamentSlotNum].ToggleFieldActive(1);
    }
    private void KillLineRenderers()
    {
        foreach (WeaponLine _weaponLine in weaponLines)
        {
            if(_weaponLine.GetFieldActiveStatus() == false)
            {
                _weaponLine.ToggleFieldActive(0);
            }
        }
    }
    private void SetSelectedArmament(int _selected)
    {
        selectedArmament = armaments[_selected];
        selectedArmamentSlotNum = _selected;
    }
    private void SetSelectedTarget(int _selected)
    {
        selectedTarget = targetsInCombatSphere[_selected];
        AttachToTarget(selectedTarget);
    }
    private void SetSelectedPart(int _selected)
    {
        selectedPart = selectedTarget.parts[_selected];
        AttachToTarget(selectedPart);
    }
    private void GetPlayerRef(UnitController _player)
    {
        player = _player;
        armaments = player.playerArmaments;

        for (int i = 0; i < 4; i++)
        {
            weaponLines.Add(new WeaponLine(weaponLineRenderers[i], i, player));
            Debug.Log("created object " + weaponLines[i]);
        }
    }
    private void GetTargetsInCombatSphere(List<UnitController> _targets)
    {
        targetsInCombatSphere = _targets;
    }
    public void AttachToTarget(UnitController _target)
    {
        weaponLines[selectedArmamentSlotNum].SetTarget(_target);
    }
}
