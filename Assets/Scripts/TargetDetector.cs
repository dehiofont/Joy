using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;

namespace FomeCharacters
{
    public class TargetDetector
    {
        public event Action<Armament, List<Armament>> OnDetectionFinish;

        private float armamentRange;

        public List<GameObject> targetsinCombatSphere = new List<GameObject>(); 
        public List<UnitController> unitControllersinCombatSphere = new List<UnitController>(); 
        
        public bool enemyListComplete = false;

        private Armament selectedArmament;
        private List<Armament> allArmaments;

        CombatSphereController combatSphereController;

        public TargetDetector(CombatSphereController _combatSphereController) 
        {
            combatSphereController = _combatSphereController;
            combatSphereController.OnArmamentSelectionChange += ArmamentSetup;
        }

        public void RemoveAllEntriesFromCombatSphereList()
        {
            GameManager.Instance.PlayerCombatSphereSelectionController.ResetSelectedObjectsMats();
            targetsinCombatSphere.Clear();
            GameManager.Instance.PlayerCombatSphereSelectionController.ResetSelection();
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log($"{other} ENTERED THE COMBAT SPHERE");
        //    enemiesInCombatSphere.Add(other.gameObject);
        //}

        private void ArmamentSetup(Armament _armament, List<Armament> _armaments)
        {
            selectedArmament = _armament;
            allArmaments = _armaments;
            armamentRange = selectedArmament.GetRange();

            getAllTargetsInRangeOfArmament();
        }

        public void getAllTargetsInRangeOfArmament()
        {
            targetsinCombatSphere.Clear();
            foreach(GameObject target in GameManager.Instance.listOfAllTargets)
            {
                float distance = Vector3.Distance(target.transform.position, GameManager.Instance.PlayerUnitController.GetUnitPos());
                Debug.Log($"{target} is {distance} from the player");
                if (target.GetComponent<UnitController>().GetCharacterType() != UnitController.CharacterType.Player && distance <= armamentRange)
                {
                    targetsinCombatSphere.Add(target);
                    unitControllersinCombatSphere.Add(target.GetComponent<UnitController>());
                }
            }
            Debug.Log("--------");
            for (int i = 0; i < targetsinCombatSphere.Count; i++)
            {
                Debug.Log(targetsinCombatSphere[i]);
            }
            Debug.Log("--------");

            enemyListComplete = true;
            if(targetsinCombatSphere.Count == 0)
            {
            }
            else
            {
                GameManager.Instance.PlayerCombatSphereSelectionController.SetEnemyMatToInCombatSphere(targetsinCombatSphere[0]);
                GameManager.Instance.noTargetsFoundCanvas.enabled = false;
            }

            OnDetectionFinish?.Invoke(selectedArmament, allArmaments);

            //GameManager.Instance.CombatSphereController.AdvanceCombatPhase();
        }
    }
}