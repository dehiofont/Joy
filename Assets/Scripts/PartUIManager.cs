using FomeCharacters;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.DedicatedServer;
using UnityEngine.UI;

public class PartUIManager : MonoBehaviour
{
    UnitController player;
    CombatUIMenu PartsMenu;

    [SerializeField] List<Canvas> listOfCanvases;
    [SerializeField] List<TextMeshProUGUI> listOfTexts;
    [SerializeField] Image selector;

    private int targetSelected = 0;
    private UnitController target;

    private bool partsMenuOn = false;
    private int selectorPositionInList = 0;
    private UnitController selectedTarget;
    private List<UnitController> listOfTargetsInCombatSphere;

    private List<UnitController> parts = new List<UnitController>();

    private void Awake()
    {
        //Debug.Log("CombatController awake");
        Event.OnPlayerSpawn += GetPlayerRef;
    }
    private void Start()
    {
        PartsMenu = new CombatUIMenu(listOfCanvases, listOfTexts, selector);
        PartsMenu.TurnOnOffMenu(0);

        Event.OnTargetSelectionChange += TargetSelected;
        Event.OnTargetSelectionChange += TurnOffMenu;
        Event.OnTargetDetectionFinish += PartsSetup;
        Event.OnPartSelectionChange += MenuSetup;
        Event.OnCombatSphereClose += TurnOffMenu;
    }

    private void GetPlayerRef(UnitController _player)
    {
        player = _player;
    }
    private void TargetSelected(int _selected)
    {
        targetSelected = _selected;
    }

    private void PartsSetup(List<UnitController> _targets)
    {
        if (_targets.Count > 0)
        {
            parts = _targets[targetSelected].parts;
        }
    }    
    private void MenuSetup(int _selected)
    {
        selectorPositionInList = _selected;

        PartsMenu.GenerateTextList(parts);
        PartsMenu.SetSelectorPosition(_selected);

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
