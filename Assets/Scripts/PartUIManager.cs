using FomeCharacters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartUIManager
{
    CombatUIMenu PartsMenu;

    private bool partsMenuOn = false;
    private int selectorPositionInList = 0;
    private UnitController selectedTarget;
    private Image selector;
    private List<Canvas> listOfCanvases;
    private List<TextMeshProUGUI> listOfTexts;
    private List<UnitController> listOfTargetsInCombatSphere;

    private List<UnitController> listOfPartsInTarget;
    public PartUIManager(
            List<UnitController> _listOfTargetsInCombatSphere,
            List<UnitController> _listOfPartsInTarget,
            List<Canvas> _listOfcanvases,
            List<TextMeshProUGUI> _listOfTexts,
            Image _selector,
            int _selectorPositionInList)
    {
        listOfTargetsInCombatSphere = _listOfTargetsInCombatSphere;
        listOfPartsInTarget = _listOfPartsInTarget;
        selectorPositionInList = _selectorPositionInList;

        PartsMenu = new CombatUIMenu(_listOfcanvases, _listOfTexts, _selector);
        PartsMenu.TurnOnOffMenu(0);

        Event.OnArmamentSelectionChange += TurnOffMenu;
        Event.OnTargetSelectionChange += MenuSetup;
        Event.OnPartSelectionChange += TurnOffMenu;
        Event.OnCombatSphereClose += TurnOffMenu;
    }
    private void MenuSetup(int _selectorPositionInList)
    {
        selectorPositionInList = _selectorPositionInList;

        PartsMenu.GenerateTextList(listOfPartsInTarget);
        PartsMenu.SetSelectorPosition(_selectorPositionInList);

        if (partsMenuOn == false)
        {
            PartsMenu.TurnOnOffMenu(1);
        }
        partsMenuOn = true;
    }
    private void TurnOffMenu()
    {
        PartsMenu.TurnOnOffMenu(0);
        partsMenuOn = false;
    }
    private void TurnOffMenu(int _temp)
    {
        PartsMenu.TurnOnOffMenu(0);
        partsMenuOn = false;
    }

}
