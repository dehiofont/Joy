using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FomeCharacters
{
    public class CombatSphereController : MonoBehaviour
    {
        [SerializeField] GameObject combatSphere;
        [SerializeField] float combatSphereMaxSize;
        [SerializeField] float combatSphereMinSize;
        [SerializeField] float combatSphereSpeedScaler = 1;
        [SerializeField] int resizeDirection = 1;
        [SerializeField] float combatSphereSingleVector;


        public event EventHandler OnCombatSphereStart;
        public event EventHandler OnCombatSphereEnd;

        public event EventHandler OnArmamentUIStart;
        public event EventHandler OnTargetUIStart;
        public event EventHandler OnPartsUIStart;

        //public class OnArmamentSelectionChangeEventArgs : EventArgs
        //{
        //    public Armament Armament;
        //    public List<Armament> armamentList;
        //}

        public event Action<Armament, List<Armament>> OnArmamentSelectionChange;
        //public class OnTargetSelectionChangeEventArgs : EventArgs
        //{
        //   public UnitController Target;
        //   public List<UnitController> targetList;
        //}

        public event Action<UnitController, List<UnitController>> OnTargetSelectionChange;
        //public event EventHandler<OnTargetSelectionChangeEventArgs> OnTargetSelectionChange;
        
        //Copy stuff above
        public event EventHandler OnPartSelectionChange;
        
        public event EventHandler OnArmamentSelected;
        public event EventHandler OnTargetSelectied;
        public event EventHandler OnPartSelected;
        private enum CombatSpherePhases
        {
            waitingToStart,
            armamentUIStart,
            targetUIStart,
            partsUIStart,
            projectileFire,
            endingCombatSelection
        }

        private bool menuSelectionMode = false;

        private CombatSpherePhases currentCombatPhase;

        public bool combatSphereOn = false;
        private bool canResizeCombatSphere = false;
        private Collider combatSphereCollider;

        public bool phase1VisualFinished;
        public bool phase2TargetsFound;
        public bool phase3UIGenerated;



        public List<Armament> currentArmaments = new List<Armament>();
        public List<UnitController> currentTargets = new List<UnitController>();
        //Add parts here

        private Armament currentArmament;
        private UnitController currentTarget;
        public int currentListNumSelection = 0;
        private int currentListCount = 0;

        CombatUIMenu armamentMenu;
        CombatUIMenu targetMenu;
        CombatUIMenu partsMenu;

        public TargetDetector TargetDetector;
        public CombatUIMenu ArmamentUI;

        private void blerg<T>(T t)
        {
            
        }
        private void Start()
        {
            currentCombatPhase = CombatSpherePhases.waitingToStart;

            currentArmaments = GameManager.Instance.playerArmaments;

            TargetDetector = new TargetDetector();
        }

        class OnArmamentUIStartEventArgs : EventArgs 
        {
            public Armament _armament; 
        };
        private void Update()
        {
            if(menuSelectionMode == true && currentCombatPhase != CombatSpherePhases.waitingToStart)
            {
                currentListNumSelection = 0;

                switch(currentCombatPhase)
                {
                    case CombatSpherePhases.armamentUIStart:
                        GameManager.Instance.PlayerUnitController.SetCharacterMovement(false);
                        
                        currentListCount = currentArmaments.Count;
                        OnArmamentUIStart?.Invoke(this, EventArgs.Empty);
                        break;
                    case CombatSpherePhases.targetUIStart:
                        currentListCount = currentTargets.Count;
                        OnTargetUIStart?.Invoke(this, EventArgs.Empty);
                        GameManager.Instance.TargetDetector.getAllTargetsInRangeOfArmament();
                        break;
                    case CombatSpherePhases.partsUIStart:

                        OnPartsUIStart?.Invoke(this, EventArgs.Empty);
                        GameManager.Instance.CombatMenuController.AdvanceMenu(1);
                        break;
                    case CombatSpherePhases.endingCombatSelection:
                        OnCombatSphereEnd?.Invoke(this, EventArgs.Empty);
                        currentCombatPhase = CombatSpherePhases.waitingToStart;
                        
                        GameManager.Instance.PlayerUnitController.SetCharacterMovement(true);
                        break;
                }
            }
            else
            {
                if (Input.GetKeyDown("w"))
                {
                    MoveSelector(-1);
                }
                if (Input.GetKeyDown("s"))
                {
                    MoveSelector(-1);
                }
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
        public void CombatSphereEnablerToggle()
        {

            if (combatSphereOn == false)
            {
                OnCombatSphereStart?.Invoke(this, EventArgs.Empty);
                AdvanceCombatPhase(1);
                combatSphereOn = true;
            }
            else 
            {
                OnCombatSphereEnd?.Invoke(this, EventArgs.Empty);
                currentCombatPhase = CombatSpherePhases.endingCombatSelection;
                combatSphereOn = false;
            }
        }

        public void MoveSelector(int _direction)
        {
            int _listCount = currentListCount - 1;

            if (_direction == 1)
            {
                currentListNumSelection--;
            }
            else
            {
                currentListNumSelection++;
            }

            if (currentListNumSelection < 0)
            {
                currentListNumSelection = 0;
            }
            if (currentListNumSelection > _listCount)
            {
                currentListNumSelection = _listCount;
            }

            switch (currentCombatPhase)
            {
                case CombatSpherePhases.armamentUIStart:
                    //OnArmamentSelectionChange?.Invoke(currentArmament, currentArmaments);
                    Event.OnArmamentSelectionChange(currentArmament, currentArmaments);
                    break;
                case CombatSpherePhases.targetUIStart:
                    OnTargetSelectionChange?.Invoke(currentTarget, currentTargets);
                    break;
                case CombatSpherePhases.partsUIStart:
                    OnPartSelectionChange?.Invoke(this, EventArgs.Empty);
                    break;
            }

        }

        private T GetCurrentSelection <T> (List<T> _list)
        {
            return _list[currentListNumSelection];
        }
        private Armament GetSelectedArmament(int _selectedItemInList)
        {
            return GameManager.Instance.playerArmaments[_selectedItemInList];
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
        }
    }
}
