using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using FomeCharacters;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArmamentManager : MonoBehaviour
{
    CombatUIMenu ArmamentMenu;
    TargetDetector TargetDetector;

    [SerializeField] List<Canvas> listOfcanvases;
    [SerializeField] List<TextMeshProUGUI> listOfTexts;
    [SerializeField] Image selector;
    [SerializeField] Armament armament;
    [SerializeField] List<Armament> listOfArmaments;

    private int selectorPositionInList = 0;

    private void Start()
    {
        ArmamentMenu = new CombatUIMenu(listOfcanvases, listOfTexts, selector);
        TargetDetector = new TargetDetector();

        Event.OnArmamentSelectionChange += ArmamentMenuSetup;
    }

    private void ArmamentMenuSetup(Armament _armament, List<Armament> _listOfArmaments)
    {
        ArmamentMenu.GenerateTextList(_listOfArmaments);

    }


}

