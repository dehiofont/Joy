using FomeCharacters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TargetUIManager
{
    CombatUIMenu TargetMenu;

    private bool targetMenuOn = false;
    private int selectorPositionInList = 0;
    private UnitController selectedTarget;
    private Image selector;
    private List<Canvas> listOfCanvases;
    private List<TextMeshProUGUI> listOfTexts;
    private List<UnitController> listOfTargetsInCombatSphere;
    public TargetUIManager(
            List<UnitController> _listOfTargetsInCombatSphere,
            List<Canvas> _listOfcanvases,
            List<TextMeshProUGUI> _listOfTexts,
            Image _selector,
            int _selectorPositionInList)
    {
        listOfTargetsInCombatSphere = _listOfTargetsInCombatSphere;
        selectorPositionInList = _selectorPositionInList;

        TargetMenu = new CombatUIMenu(_listOfcanvases, _listOfTexts, _selector);
        TargetMenu.TurnOnOffMenu(0);

        Event.OnArmamentSelectionChange += TurnOffMenu;
        Event.OnTargetSelectionChange += MenuSetup;
        Event.OnPartSelectionChange += TurnOffMenu;
        Event.OnCombatSphereClose += TurnOffMenu;
    }
    private void MenuSetup(int _selectorPositionInList)
    {
        selectorPositionInList = _selectorPositionInList;

        TargetMenu.GenerateTextList(listOfTargetsInCombatSphere);
        TargetMenu.SetSelectorPosition(_selectorPositionInList);

        if (targetMenuOn == false)
        {
            TargetMenu.TurnOnOffMenu(1);
        }
        targetMenuOn = true;
    }
    private void TurnOffMenu()
    {
        TargetMenu.TurnOnOffMenu(0);
        targetMenuOn = false;
    }
    private void TurnOffMenu(int _temp)
    {
        TargetMenu.TurnOnOffMenu(0);
        targetMenuOn = false;
    }
}
