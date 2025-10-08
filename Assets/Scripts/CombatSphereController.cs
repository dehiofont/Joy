using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FomeCharacters
{
    public class CombatSphereController : MonoBehaviour
    {
        private enum CombatSpherePhases
        {
            waitingToStart,
            armamentUIStart,
            targetUIStart,
            partsUIStart,
            projectileFire,
            endingCombatSelection
        }
        private CombatSpherePhases currentCombatPhase;

        private bool menuSelectionMode = false;

        public bool combatSphereOn = false;

        //ARMAMENTS
        private ArmamentUIManager ArmamentUIManager;
        [SerializeField] List<Armament> listOfArmaments = new List<Armament>();
        [SerializeField] List<Canvas> armamentCanvases = new List<Canvas>();
        [SerializeField] List<TextMeshProUGUI> armamentTexts = new List<TextMeshProUGUI>();
        [SerializeField] Image armamentSelector;
        [SerializeField] Canvas noTargetsUI;
        private List<UnitController> listOfAllPotentialTargets;

        //TARGETS
        private TargetUIManager TargetUIManager;
        [SerializeField] List<Canvas> targetCanvases = new List<Canvas>();
        [SerializeField] List<TextMeshProUGUI> targetTexts = new List<TextMeshProUGUI>();
        [SerializeField] Image targetSelector;

        //PARTS
        private PartUIManager PartUIManager;
        private List<UnitController> listOfPartsInTarget = new List<UnitController> ();
        [SerializeField] List<Canvas> partsCanvases = new List<Canvas>();
        [SerializeField] List<TextMeshProUGUI> partTexts = new List<TextMeshProUGUI>();
        [SerializeField] Image partSelector;

        //HIGHLIGHTER
        TargetHighlighter TargetHighlighter;

        //BUBBLE
        [SerializeField] GameObject combatSphere;
        [SerializeField] float sphereSpeedScaler = 20;

        private List<UnitController> targetsInCombatSphere = new List<UnitController>();

        private UnitController currentTarget;
        private int selectedItemInUIList = 0;
        private int currentListCount = 0;
        private void Start()
        {
            listOfAllPotentialTargets = GameManager.Instance.listOfAllPotentialTargets;
            currentCombatPhase = CombatSpherePhases.waitingToStart;

            Armament Cannon = new Armament("Cannon", 10);
            Armament Archer = new Armament("Archer", 15);
            listOfArmaments.Add(Cannon);
            listOfArmaments.Add(Archer);

            ArmamentUIManager = new ArmamentUIManager(
                listOfAllPotentialTargets,
                targetsInCombatSphere,
                armamentCanvases,
                armamentTexts,
                armamentSelector,
                listOfArmaments,
                selectedItemInUIList,
                noTargetsUI,
                combatSphere,
                sphereSpeedScaler);

            TargetUIManager = new TargetUIManager(
                targetsInCombatSphere,
                targetCanvases,
                targetTexts,
                targetSelector,
                selectedItemInUIList);

            //PartUIManager = new PartUIManager(
            //    targetsInCombatSphere,
            //    listOfPartsInTarget,
            //    targetCanvases,
            //    targetTexts,
            //    targetSelector,
            //    selectedItemInUIList);

            TargetHighlighter = new TargetHighlighter(
                listOfAllPotentialTargets,
                targetsInCombatSphere, 
                selectedItemInUIList);

        }
        private void Update()
        {
            Event.OnUpdate?.Invoke();

            if (menuSelectionMode == false && currentCombatPhase != CombatSpherePhases.waitingToStart)
            {
                selectedItemInUIList = 0;

                switch(currentCombatPhase)
                {
                    case CombatSpherePhases.armamentUIStart:
                        GameManager.Instance.PlayerUnitController.SetCharacterMovement(false);
                        currentListCount = listOfArmaments.Count;
                        Event.OnArmamentSelectionChange?.Invoke(selectedItemInUIList);
                        menuSelectionMode = true;
                        break;
                    case CombatSpherePhases.targetUIStart:
                        currentListCount = targetsInCombatSphere.Count;
                        Event.OnTargetSelectionChange?.Invoke(selectedItemInUIList);
                        menuSelectionMode = true;
                        break;
                    case CombatSpherePhases.partsUIStart:

                        break;
                    case CombatSpherePhases.endingCombatSelection:
                        currentCombatPhase = CombatSpherePhases.waitingToStart;
                        
                        GameManager.Instance.PlayerUnitController.SetCharacterMovement(true);
                        break;
                }
            }
            else
            {
                if (Input.GetKeyDown("w"))
                {
                    MoveSelector(1);
                }
                if (Input.GetKeyDown("s"))
                {
                    MoveSelector(-1);
                }

                if(targetsInCombatSphere.Count > 0)
                {
                    if (Input.GetKeyDown("e"))
                    {
                        AdvanceCombatPhase(1);
                    }
                    if (Input.GetKeyDown("q"))
                    {
                        AdvanceCombatPhase(-1);
                    }
                }
            }
        }
        public void CombatSphereEnablerToggle()
        {
            if (combatSphereOn == false)
            {
                Event.OnCombatSphereOpen?.Invoke();
                AdvanceCombatPhase(1);
                combatSphereOn = true;
            }
            else 
            {
                Event.OnCombatSphereClose?.Invoke();
                currentCombatPhase = CombatSpherePhases.waitingToStart;
                GameManager.Instance.PlayerUnitController.SetCharacterMovement(true);
                combatSphereOn = false;
            }
        }
        public void MoveSelector(int _direction)
        {
            int _listCount = currentListCount - 1;

            if (_direction == 1)
            {
                selectedItemInUIList--;
            }
            else
            {
                selectedItemInUIList++;
            }

            if (selectedItemInUIList < 0)
            {
                selectedItemInUIList = _listCount;
            }
            if (selectedItemInUIList > _listCount)
            {
                selectedItemInUIList = 0;
            }

            switch (currentCombatPhase)
            {
                case CombatSpherePhases.armamentUIStart:
                    Event.OnArmamentSelectionChange?.Invoke(selectedItemInUIList);
                    break;
                case CombatSpherePhases.targetUIStart:
                    Event.OnTargetSelectionChange?.Invoke(selectedItemInUIList);
                    break;
                case CombatSpherePhases.partsUIStart:
                    break;
            }
        }
        public void AdvanceCombatPhase(int _direction)
        {
            if(currentCombatPhase == CombatSpherePhases.endingCombatSelection)
            {
                currentCombatPhase = CombatSpherePhases.waitingToStart;
            }
            else if (_direction == 1) 
            {
                currentCombatPhase++;
            }
            else
            { 
                currentCombatPhase--; 
            }
            menuSelectionMode = false;
        }
    }
}
