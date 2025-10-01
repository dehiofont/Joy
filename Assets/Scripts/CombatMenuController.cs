using FomeCharacters;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CombatMenuController : MonoBehaviour
{
    private enum MenuPhases
    {
        phase_0_Waiting,
        phase_1_Armament,
        phase_2_Target,
        phase_3_Parts,
        phase_4_Execute
    }

    private MenuPhases currentMenuPhase;

    private bool transitioningToNewMenu = false;

    CombatUIMenu armamentMenu;
    CombatUIMenu targetMenu;
    CombatUIMenu partsMenu;
    CombatUIMenu noTargetMenu;

    private List<Canvas> armamamentCanvases;
    private List<Canvas> targetCanvases;
    private List<Canvas> partCanvases;

    Image armamentSelector;
    Image targetSelector;
    Image partsSelector;

    List<TextMeshProUGUI> armamentTextList;
    List<TextMeshProUGUI> targetTextList;
    List<TextMeshProUGUI> partsTextList;

    CombatUIMenu currentMenu;

    private void Start()
    {
        armamamentCanvases = GameManager.Instance.armamentCanvases;
        targetCanvases = GameManager.Instance.targetCanvases;
        partCanvases = GameManager.Instance.partCanvases;

        armamentSelector = GameManager.Instance.armamentSelector;
        targetSelector = GameManager.Instance.targetSelector;
        partsSelector = GameManager.Instance.partSelector;

        armamentTextList = GameManager.Instance.armamentTexts;
        targetTextList = GameManager.Instance.targetTexts;
        partsTextList = GameManager.Instance.partTexts;

        armamentMenu = new CombatUIMenu(armamamentCanvases,armamentTextList,armamentSelector);
        targetMenu = new CombatUIMenu(targetCanvases,targetTextList,targetSelector);
        partsMenu = new CombatUIMenu(partCanvases,partsTextList,partsSelector);
        
        TurnOffAllUI();
        //noTargetMenu = new CombatUIMenu();

        currentMenuPhase = MenuPhases.phase_0_Waiting;

        ResetAllTextsInList(GameManager.Instance.targetTexts);
        ResetAllTextsInList(GameManager.Instance.partTexts);
        GameManager.Instance.backgroundArmamentCanvas.sortingLayerName = "UI Background";
        GameManager.Instance.backgroundTargetCanvas.sortingLayerName = "UI Text";
    }

    private void Update()
    {
        if (currentMenuPhase != MenuPhases.phase_0_Waiting)
        {
            if(transitioningToNewMenu == true)
            {
                TurnOffAllUI();
                switch (currentMenuPhase)
                {
                    case MenuPhases.phase_1_Armament:
                        currentMenu = armamentMenu;
                        currentMenu.GenerateTextList(GameManager.Instance.armamentController.armaments);
                        armamentMenu.TurnOnOffMenu(1);
                        transitioningToNewMenu = false;
                        break;
                    case MenuPhases.phase_2_Target:
                        currentMenu = targetMenu;
                        currentMenu.GenerateTextList(GameManager.Instance.playerArmaments);
                        currentMenu.TurnOnOffMenu(1);
                        transitioningToNewMenu = false;
                        break;
                    case MenuPhases.phase_3_Parts:
                        currentMenu = partsMenu;
                        currentMenu.TurnOnOffMenu(1);
                        transitioningToNewMenu = false;
                        break;
                    case MenuPhases.phase_4_Execute:
                        break;
                }
            }

            if(currentMenuPhase != MenuPhases.phase_4_Execute)
            {
                if (Input.GetKeyDown("w"))
                {
                    currentMenu.MoveSelector(1);
                }
                if (Input.GetKeyDown("s"))
                {
                    currentMenu.MoveSelector(-1);
                }
                if (Input.GetKeyDown("e"))
                {
                    AdvanceMenu(1);
                }
                if (Input.GetKeyDown("q"))
                {
                    AdvanceMenu(-1);
                }
            }
        }
    }
    public void TurnOffAllUI()
    {
        armamentMenu.TurnOnOffMenu(0);
        targetMenu.TurnOnOffMenu(0);
        partsMenu.TurnOnOffMenu(0);

        if(currentMenuPhase == MenuPhases.phase_0_Waiting || currentMenuPhase != MenuPhases.phase_4_Execute)
        {
        }
        else
        {
        }
    }
    public void AdvanceMenu(int _direction)
    {
        if(currentMenuPhase == MenuPhases.phase_4_Execute && _direction > 0)
        {
            currentMenuPhase = MenuPhases.phase_0_Waiting;
        }
        else
        {
            if (_direction > 0)
            {
                currentMenuPhase++;
            }
            else
            {
                currentMenuPhase--;
            }
        }

        transitioningToNewMenu = true;
    }
    private void ToggleNoTargetFoundCanvas()
    {
        if (GameManager.Instance.noTargetsFoundCanvas.enabled == true)
        {
            GameManager.Instance.noTargetsFoundCanvas.enabled = false;
        }
        else
        {
            GameManager.Instance.noTargetsFoundCanvas.enabled = true;
        }
    }

    public void ResetAllTextsInList(List<TextMeshProUGUI> _textList)
    {
        foreach(TextMeshProUGUI _text in _textList)
        {
            _text.SetText("");
        }
    }

    public void ResetAllMenusBackToDefault()
    {
        ResetAllTextsInList(GameManager.Instance.targetTexts);
        ResetAllTextsInList(GameManager.Instance.partTexts);
        GameManager.Instance.backgroundArmamentCanvas.sortingLayerName = "UI Background";
        GameManager.Instance.backgroundTargetCanvas.sortingLayerName = "UI Text";
        GameManager.Instance.noTargetsFoundCanvas.enabled = false;
    }

    public void SetNoAvailableTargetsUI()
    {
        //GameManager.backgroundTargetCanvas.sortingLayerName = "UI Text";
        //GameManager.backgroundArmamentCanvas.sortingLayerName = "UI Text";
        //GameManager.armamentSelector.color = GameManager.CombatSphereMenuController.selectColorDeactivated;
        //GameManager.targetSelector.color = GameManager.CombatSphereMenuController.selectColorDeactivated;
        //TurnOffAllCombatSphereUI();
        GameManager.Instance.noTargetsFoundCanvas.enabled = true;
    }
    

}
