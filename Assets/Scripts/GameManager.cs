using FomeCharacters;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; private set; }

    public List<GameObject> listOfAllTargets = new List<GameObject>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public UnitController PlayerUnitController;
    public CombatSphereController CombatSphereController;
    public TargetDetector TargetDetector;
    public CombatSphereSelectionController PlayerCombatSphereSelectionController;
    public CombatMenuController CombatMenuController;

    public GameObject Player;
    public List<Armament> playerArmaments;
    public ArmamentController armamentController;
    
    public WeaponLineController PlayerWeaponLineController;

    public List<Canvas> armamentCanvases = new List<Canvas>();
    public Canvas backgroundArmamentCanvas;
    public List<Canvas> targetCanvases = new List<Canvas>();
    public Canvas backgroundTargetCanvas;
    public List<Canvas> partCanvases = new List<Canvas>();
    public Canvas backgroundPartCanvas;

    public Canvas inputPromptCanvas = new Canvas();
    public Canvas noTargetsFoundCanvas = new Canvas();

    public List<TextMeshProUGUI> armamentTexts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> targetTexts = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> partTexts = new List<TextMeshProUGUI>();

    public Image armamentSelector;
    public Image targetSelector;
    public Image partSelector;


}
