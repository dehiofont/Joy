using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FomeCharacters
{
    public class CombatSphereController : MonoBehaviour
    {
        public UnitController player;
        private List<UnitController> unitControllersInStage;

        private List<Armament> armaments;
        private enum CombatSpherePhases
        {
            waitingToStart,
            armamentUIStart,
            targetUIStart,
            partsUIStart,
            projectileFire
            //endingCombatSelection
        }
        private CombatSpherePhases currentCombatPhase;

        private bool menuSelectionMode = false;

        public bool combatSphereOn = false;

        //HIGHLIGHTER
        ObjectHighlighter TargetHighlighter;

        //BUBBLE
        [SerializeField] GameObject combatSphere;
        [SerializeField] float sphereSpeedScaler = 20;

        private List<UnitController> targetsInCombatSphere = new List<UnitController>();
        private List<UnitController> partsInTarget = new List<UnitController>();

        private UnitController currentTarget;
        private int selectedItemInUIList = 0;
        private int currentListCount = 0;

        private UnitController finalTarget;
        private Armament finalArmament;

        private void Awake()
        {
            Debug.Log("CombatController awake");
            Event.OnPlayerSpawn += GetPlayerRef;
        }
        private void Start()
        {
            unitControllersInStage = GameManager.Instance.unitControllersInStage;

            currentCombatPhase = CombatSpherePhases.waitingToStart;

            Event.OnTargetDetectionFinish += GetListOfTargetsInSphere;
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
                        player.SetCharacterMovement(false);
                        currentListCount = armaments.Count;
                        Event.OnArmamentSelectionChange?.Invoke(selectedItemInUIList);
                        menuSelectionMode = true;
                        break;
                    case CombatSpherePhases.targetUIStart:
                        currentListCount = targetsInCombatSphere.Count;
                        Event.OnTargetSelectionChange?.Invoke(selectedItemInUIList);
                        menuSelectionMode = true;
                        break;
                    case CombatSpherePhases.partsUIStart:
                        GetListOfPartsInTarget();
                        currentListCount = partsInTarget.Count;
                        Event.OnPartSelectionChange?.Invoke(selectedItemInUIList);
                        menuSelectionMode = true;
                        break;
                    case CombatSpherePhases.projectileFire:
                        Event.OnProjectileFire?.Invoke();
                        ExitCombatSphere();
                        break;
                    //case CombatSpherePhases.endingCombatSelection:
                    //    ExitCombatSphere();
                    //    break;
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
        private void SetFinalTargetAndArmament()
        {

        }
        private void GetPlayerRef(UnitController _player)
        {
            player = _player;
            armaments = player.playerArmaments;
        }

        private void GetListOfTargetsInSphere(List<UnitController> _targetsInSphere)
        {
            targetsInCombatSphere = _targetsInSphere;

            //Event.OnTargetDetectionFinish -= GetListOfTargetsInSphere;
        }
        private void GetListOfPartsInTarget()
        {
            partsInTarget = targetsInCombatSphere[selectedItemInUIList].parts;
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
                ExitCombatSphere();
            }
        }
        private void ExitCombatSphere()
        {
            Event.OnCombatSphereClose?.Invoke();
            player.SetCharacterMovement(true);
            combatSphereOn = false;
            currentCombatPhase = CombatSpherePhases.waitingToStart;
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
                    Event.OnPartSelectionChange?.Invoke(selectedItemInUIList);
                    break;
            }
        }
        public void AdvanceCombatPhase(int _direction)
        {
            if (_direction == 1) 
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
