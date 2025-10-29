using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static FomeCharacters.UnitController;
using static UnityEngine.GraphicsBuffer;

namespace FomeCharacters
{
    public class ObjectHighlighter : MonoBehaviour
    {
        List<UnitController> unitControllersInStage;
        List<UnitController> targetsInCombatSphere = new List<UnitController>();
        List<UnitController> partsInTarget = new List<UnitController>();

        List<UnitController> unitControllers = new List<UnitController>();
        int targetSelected = 0;
        int partSelected = 0;
        UnitController player;
        private void Awake()
        {
            Event.OnPlayerSpawn += GetPlayer;
        }
        private void Start()
        {
            unitControllersInStage = GameManager.Instance.unitControllersInStage;
            //targetsInCombatSphere = _listOfTargetsInCombatSphere;
            //selectedInList = _selectedInList;

            Event.OnArmamentSelectionChange += ArmamentHighlight;
            Event.OnTargetSelectionChange += TargetHighlight;
            Event.OnPartSelectionChange += PartHighlight;
            Event.OnTargetDetectionFinish += SetTargetsInCombatSphere;
            Event.OnCombatSphereClose += RemovingAllCombatSphereHighlights;
        }

        private void GetPlayer(UnitController _player)
        {
            player = _player;
        }
        private void SetTargetsInCombatSphere(List<UnitController> _targetsInCombatSphere)
        {
            RemoveHighlights(targetsInCombatSphere);

            targetsInCombatSphere.Clear();
            foreach(UnitController _target in _targetsInCombatSphere)
            {
                targetsInCombatSphere.Add(_target);
            }
        }
        private void ArmamentHighlight(int _selected)
        {
            HighlightAll(targetsInCombatSphere);
        }
        private void TargetHighlight(int _selected)
        {
            targetSelected = _selected;

            RemoveHighlightsFromAllUnitsInScene();

            if (targetsInCombatSphere.Count > 0)
            {
                HighlightAll(targetsInCombatSphere);
                HighlightSelected(targetsInCombatSphere, targetSelected);
            }
        }
        private void PartHighlight(int _selected)
        {
            partSelected = _selected;
            partsInTarget = targetsInCombatSphere[targetSelected].parts;
            HighlightAll(partsInTarget);
            HighlightSelected(partsInTarget, partSelected);
        }
        private void RemovingAllCombatSphereHighlights()
        {
            RemoveHighlights(targetsInCombatSphere);
            RemoveHighlights(partsInTarget);
        }
        private void HighlightSelected(List<UnitController> _unitControllers, int _selected)
        {
            HighlightAll(_unitControllers);
            foreach (Renderer _renderer in _unitControllers[_selected].rendereManager.listOfRenderers)
            {
                _renderer.material = _unitControllers[_selected].matCharacterSelected;
            }
        }
        private void HighlightAll(List<UnitController> _unitControllers)
        {
            RemoveHighlights(_unitControllers);
            foreach (UnitController _unitController in _unitControllers)
            {
                if (_unitController.getCharacterType() != CharacterType.Player)
                {
                    foreach (Renderer _renderer in _unitController.rendereManager.listOfRenderers)
                    {
                        _renderer.material = _unitController.matCharacterInCombatSphere;
                    }
                }
            }
        }
        public void RemoveHighlights(List<UnitController> _unitControllers)
        {
            if(unitControllersInStage.Count != 0)
            {
                foreach (UnitController _unitController in _unitControllers)
                {
                    if (_unitController.getCharacterType() != CharacterType.Player)
                    {
                        foreach(Renderer _renderer in _unitController.rendereManager.listOfRenderers)
                        {
                            _renderer.material = _unitController.matCharacterBase;
                        }
                    }
                }
            }
        }

        private void RemoveHighlightsFromAllUnitsInScene()
        {
            RemoveHighlights(unitControllersInStage);

            //foreach(UnitController _unitController in unitControllersInStage)
            //{
            //    if(_unitController.getCharacterType() != CharacterType.Part)
            //    {
            //        RemoveHighlights(_unitController.parts);
            //    }
            //}
        }
    }
}