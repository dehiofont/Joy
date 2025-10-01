using System;
using System.Collections.Generic;
using UnityEngine;

namespace FomeCharacters
{
    public class CombatSphereSelectionController : MonoBehaviour
    {
        public enum CombatSphereStage
        {
            ArmamentSelect,
            TargetSelect,
            PartSelect,
            Fire
        }
        private int combatSphereStageEnumCount = Enum.GetNames(typeof(CombatSphereStage)).Length;
        public enum EnemiesInCombatSphereStatus
        {
            InCombatSphere,
            Selected,
            LeavingCombatSphere
        }

        private List<GameObject> enemiesInCS;
        private int enemiesInCSCount = 0;
        private int slotNumInEnemyList = 0;
        public int currentSelectedSlot = 0;

        private CombatSphereStage currentCombatSphereStage = CombatSphereStage.ArmamentSelect;

        private bool buttonUpActivated;
        private bool buttonDownActivated;

        private void Start()
        {
            Debug.Log(currentCombatSphereStage);
        }
        private void Update()
        {
            if(GameManager.Instance.CombatSphereController.combatSphereOn == true)
            {

                if (Input.GetKeyDown("w"))
                {
                    buttonUpActivated = true;
                    buttonDownActivated = false;
                    CombatSphereStageAction();
                }
                if (Input.GetKeyDown("s"))
                {
                    buttonDownActivated = true;
                    buttonUpActivated = false;
                    CombatSphereStageAction();
                }
                if (Input.GetKeyDown("e"))
                {
                    buttonUpActivated = false;
                    buttonDownActivated = false;
                    currentCombatSphereStage++;
                    Debug.Log(currentCombatSphereStage);
                    SetCorrectPhase();
                    CombatSphereStageAction();
                }
                if (Input.GetKeyDown("q"))
                {
                    buttonUpActivated = false;
                    buttonDownActivated = false;
                    currentCombatSphereStage--;
                    Debug.Log(currentCombatSphereStage);
                    SetCorrectPhase();
                    CombatSphereStageAction();
                }

                //if (GameManager.PlayerCombatSphereEnemyDetector.enemiesInCombatSphere.Count == 0)
                //{
                //    GameManager.CombatSphereMenuController.SetNoAvailableTargetsUI();
                //}

                //if(GameManager.PlayerCombatSphereEnemyDetector.enemiesInCombatSphere.Count < 0)
                //{
                //    SetNoAvailableTargetsUI();
                //}

            }
        }

        private void SetCorrectPhase()
        {
            if((int)currentCombatSphereStage > combatSphereStageEnumCount - 1)
            {
                currentCombatSphereStage = CombatSphereStage.Fire;
            }
            if ((int)currentCombatSphereStage < 0)
            {
                currentCombatSphereStage = CombatSphereStage.ArmamentSelect;
            }
        }
        private void CombatSphereStageAction()
        {
            //if (GameManager.PlayerCombatSphereEnemyDetector.enemiesInCombatSphere.Count > 0)
            //{
                switch (currentCombatSphereStage)
                {
                    case CombatSphereStage.ArmamentSelect:

                        GameManager.Instance.backgroundArmamentCanvas.sortingLayerName = "UI Background";
                        GameManager.Instance.backgroundTargetCanvas.sortingLayerName = "UI Text";

                        break;
                    case CombatSphereStage.TargetSelect:
                        GameManager.Instance.backgroundArmamentCanvas.sortingLayerName = "UI Text";
                        GameManager.Instance.backgroundTargetCanvas.sortingLayerName = "UI Background";
                        if (GameManager.Instance.targetCanvases[0].enabled == false)
                        {
                        }

                        if (GameManager.Instance.TargetDetector.targetsinCombatSphere.Count > 1)
                        {
                            if (buttonUpActivated == true)
                            {
                                slotNumInEnemyList--;
                            }
                            else if (buttonDownActivated == true)
                            {
                                slotNumInEnemyList++;
                            }
                            ChangeEnemySelection();
                        }
                        break;
                    case CombatSphereStage.PartSelect:
                        if (GameManager.Instance.partCanvases[0].enabled == false)
                        {
                        }
                        break;
                    case CombatSphereStage.Fire:
                        GameManager.Instance.CombatSphereController.CombatSphereEnablerToggle();
                        currentCombatSphereStage = CombatSphereStage.ArmamentSelect;
                        break;
                }
            //}
            //else
            //{ 

            //}
        }


        private void ChangeEnemySelection()
        {
            enemiesInCSCount = RetrieveNumOfEnemiesInCombatSphere();
            if (slotNumInEnemyList < 0)
            {
                slotNumInEnemyList = enemiesInCSCount - 1;
            }
            if (slotNumInEnemyList > (enemiesInCSCount - 1)) 
            {
                slotNumInEnemyList = 0;
            }
            SetEnemyMatToInCombatSphere(GameManager.Instance.TargetDetector.targetsinCombatSphere[currentSelectedSlot]);
            SetEnemyMatToSelected(GameManager.Instance.TargetDetector.targetsinCombatSphere[slotNumInEnemyList]);
            currentSelectedSlot = slotNumInEnemyList;
            GameManager.Instance.PlayerWeaponLineController.AttachToFoundEnemy();
        }
        private int RetrieveNumOfEnemiesInCombatSphere()
        {
            return GameManager.Instance.TargetDetector.targetsinCombatSphere.Count;
        }
        public void ResetSelectedObjectsMats()
        {
            foreach (GameObject _enemy in GameManager.Instance.TargetDetector.targetsinCombatSphere)
            {
                ChangeCharsMaterialInCombatSphere(_enemy, EnemiesInCombatSphereStatus.LeavingCombatSphere);
            }
        }
        public void SetEnemyMatToInCombatSphere(GameObject currentTarget)
        {
            List<GameObject> _enemiesInCombatSphere = GameManager.Instance.TargetDetector.targetsinCombatSphere;
            if (_enemiesInCombatSphere.Count > 0)
            {
                for(int i = 0; i < _enemiesInCombatSphere.Count; i++)
                {
                    if(_enemiesInCombatSphere[i] == currentTarget)
                    {
                        SetEnemyMatToSelected(_enemiesInCombatSphere[i]);
                        GameManager.Instance.PlayerWeaponLineController.AttachToFoundEnemy();
                    }
                    else
                    {
                        ChangeCharsMaterialInCombatSphere(_enemiesInCombatSphere[i], EnemiesInCombatSphereStatus.InCombatSphere);
                    }
                }
            }
            else
            {
            }
        }

        public void SetNewSelectedTarget()
        {

        }
        public void SetEnemyMatToSelected(GameObject _character)
        {
            ChangeCharsMaterialInCombatSphere(_character, EnemiesInCombatSphereStatus.Selected);
        }
        public void ChangeCharsMaterialInCombatSphere(GameObject _character, EnemiesInCombatSphereStatus _charMatState)
        {
            if (_character.TryGetComponent<UnitController>(out UnitController _characterController))
            {
                if (_characterController.GetCharacterTeam() == UnitController.CharacterTeam.Enemy)
                {
                    switch (_charMatState)
                    {
                        case EnemiesInCombatSphereStatus.InCombatSphere:
                            _characterController.SetCharacterInCombatSphereMat();
                            break;
                        case EnemiesInCombatSphereStatus.Selected:
                            _characterController.SetCharacterSelectedMat();
                            break;
                        case EnemiesInCombatSphereStatus.LeavingCombatSphere:
                            _characterController.SetCharacterBaseMat();
                            break;
                        default:
                            _characterController.SetCharacterBaseMat();
                            break;
                    }
                }
            }
        }
        public void ResetSelection()
        {
            enemiesInCSCount = 0;
            slotNumInEnemyList = 0;
            currentSelectedSlot = 0;
        }    
    }
}