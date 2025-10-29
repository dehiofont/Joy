using System.Collections.Generic;
using FomeCharacters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmamentUIManager : MonoBehaviour
{
    private UnitController player;
    
    private CombatUIMenu ArmamentMenu;

    [SerializeField] List<Canvas> armamentCanvases;
    [SerializeField] List<TextMeshProUGUI> listOfTexts;
    [SerializeField] Image selector;
    [SerializeField] GameObject noTargetsObject;
    private Canvas noTargetsUI;

    private List<Armament> armaments;

    private bool armamentMenuOn = false;
    private void Awake()
    {
        Event.OnPlayerSpawn += GetPlayerRef;
    }
    private void Start()
    {
        ArmamentMenu = new CombatUIMenu(armamentCanvases, listOfTexts, selector);
        ArmamentMenu.TurnOnOffMenu(0);

        noTargetsUI = noTargetsObject.GetComponent<Canvas>();

        ToggleNoTargetsUI(false);

        Event.OnArmamentSelectionChange += MenuSetup;
        Event.OnCombatSphereClose += TurnOffMenu;
        Event.OnTargetSelectionChange += TurnOffMenu;
        Event.ToggleNoTargetsInSphere += ToggleNoTargetsUI;
    }
    private void GetPlayerRef(UnitController _player)
    {
        player = _player;
        armaments = player.playerArmaments;
    }
    private void MenuSetup(int _selectedArmament)
    {
        ArmamentMenu.GenerateTextList(armaments);
        ArmamentMenu.SetSelectorPosition(_selectedArmament);
        
        if(armamentMenuOn == false)
        {
            ArmamentMenu.TurnOnOffMenu(1);
        }
        armamentMenuOn = true;
    }
    private void TurnOffMenu()
    {
        ArmamentMenu.TurnOnOffMenu(0);
        ToggleNoTargetsUI(false);
        armamentMenuOn = false;
    }
    private void TurnOffMenu(int _temp)
    {
        ArmamentMenu.TurnOnOffMenu(0);
        ToggleNoTargetsUI(false);
        armamentMenuOn = false;
    }
    private void ToggleNoTargetsUI(bool _toggleOn)
    {
        if(_toggleOn == true)
        {
            noTargetsUI.enabled = true;
            //Debug.Log("notarget on");
        }
        else
        {
            noTargetsUI.enabled = false;
            //Debug.Log("notarget off");
        }
    }
}

