using FomeCharacters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TargetUIManager : MonoBehaviour
{
    CombatUIMenu TargetMenu;

    private bool targetMenuOn = false;

    [SerializeField] List<Canvas> listOfCanvases;
    [SerializeField] List<TextMeshProUGUI> listOfTexts;
    [SerializeField] Image selector;

    private List<UnitController> targets;

    private void Start()
    {
        TargetMenu = new CombatUIMenu(listOfCanvases, listOfTexts, selector);
        TargetMenu.TurnOnOffMenu(0);

        Event.OnTargetDetectionFinish += UpdateTargetList;
        Event.OnArmamentSelectionChange += TurnOffMenu;
        Event.OnTargetSelectionChange += MenuSetup;
        Event.OnPartSelectionChange += TurnOffMenu;
        Event.OnCombatSphereClose += TurnOffMenu;
    }

    private void UpdateTargetList(List<UnitController> _targets)
    {
        targets = _targets;
    }
    private void MenuSetup(int _selectorPositionInList)
    {
        TargetMenu.GenerateTextList(targets);
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
