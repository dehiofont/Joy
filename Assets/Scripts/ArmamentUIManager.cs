using System.Collections.Generic;
using FomeCharacters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmamentUIManager
{
    private CombatUIMenu ArmamentMenu;
    private TargetDetector TargetDetector;
    private CombatSphereBubble CombatSphereBubble;

    private float sphereSpeedScaler;
    private GameObject combatSphere;
    private List<UnitController> listOfAllPotentialTargets;
    private int selectorPositionInList = 0;
    private Image selector;
    private List<Canvas> listOfArmamentCanvases;
    private List<TextMeshProUGUI> listOfTexts;
    private List<Armament> listOfArmaments;
    private List<UnitController> listOfTargetsInCombatSphere;
    private Canvas noTargetsUI;
    private Armament selectedArmament;

    private bool armamentMenuOn = false;

    public ArmamentUIManager(
        List<UnitController> _listOfAllPotentialTargets,
        List<UnitController> _listOfTargetsInCombatSphere,
        List<Canvas> listOfcanvases,
        List<TextMeshProUGUI> listOfTexts,
        Image selector,
        List<Armament> _listOfArmaments,
        int _selectorPositionInList,
        Canvas _noTargetsUI,
        GameObject _combatSphere,
        float _sphereSpeedScaler)
    {
        listOfAllPotentialTargets = _listOfAllPotentialTargets;
        listOfTargetsInCombatSphere = _listOfTargetsInCombatSphere;
        listOfArmaments = _listOfArmaments;
        noTargetsUI = _noTargetsUI;
        selectorPositionInList = _selectorPositionInList;
        combatSphere = _combatSphere;
        sphereSpeedScaler = _sphereSpeedScaler;

        ArmamentMenu = new CombatUIMenu(listOfcanvases, listOfTexts, selector);
        ArmamentMenu.TurnOnOffMenu(0);
        
        TargetDetector = new TargetDetector(listOfAllPotentialTargets, listOfTargetsInCombatSphere);

        CombatSphereBubble = new CombatSphereBubble(listOfArmaments, selectorPositionInList, sphereSpeedScaler, combatSphere);

        TurnOffNoTargetsUI();

        Event.OnArmamentSelectionChange += MenuSetup;
        Event.OnCombatSphereClose += TurnOffMenu;
        Event.OnTargetSelectionChange += TurnOffMenu;
        Event.OnNoTargetsInSphere += TurnOnNoTargetsUI;
    }

    private void MenuSetup(int _selectorPositionInList)
    {
        TurnOffNoTargetsUI();

        selectorPositionInList = _selectorPositionInList;
        TargetDetector.getAllTargetsInRangeOfArmament(GetSelectedArmament().GetRange());
        
        ArmamentMenu.GenerateTextList(listOfArmaments);
        ArmamentMenu.SetSelectorPosition(_selectorPositionInList);
        
        if(armamentMenuOn == false)
        {
            ArmamentMenu.TurnOnOffMenu(1);
        }
        armamentMenuOn = true;
    }

    private Armament GetSelectedArmament()
    {
        return listOfArmaments[selectorPositionInList];
    }
    private void TurnOffMenu()
    {
        ArmamentMenu.TurnOnOffMenu(0);
        TurnOffNoTargetsUI();
        armamentMenuOn = false;
    }

    private void TurnOffMenu(int _temp)
    {
        ArmamentMenu.TurnOnOffMenu(0);
        TurnOffNoTargetsUI();
        armamentMenuOn = false;
    }

    private void TurnOnNoTargetsUI()
    {
        noTargetsUI.enabled = true;
    }
    private void TurnOffNoTargetsUI()
    {
        noTargetsUI.enabled = false;
    }
}

