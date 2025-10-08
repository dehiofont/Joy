using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static FomeCharacters.UnitController;
using static UnityEngine.GraphicsBuffer;

namespace FomeCharacters
{
    public class TargetHighlighter
    {
        List<UnitController> listOfAllPotentialTargets;
        List<UnitController> listOfTargetsInCombatSphere;
        int selectedInList;

        public TargetHighlighter(
                List<UnitController> _listOfAllPotentialTargets,
                List<UnitController> _listOfTargetsInCombatSphere,
                int _selectedInList)
        {
            listOfAllPotentialTargets = _listOfAllPotentialTargets;
            listOfTargetsInCombatSphere = _listOfTargetsInCombatSphere;
            selectedInList = _selectedInList;

            Event.OnArmamentSelectionChange += HighlightAll;
            Event.OnTargetSelectionChange += HighlightSelected;
            Event.OnCombatSphereClose += RemoveAllHighlights;
        }
        private void HighlightSelected(int _selected)
        {
            HighlightAll(1);
            foreach (Renderer _renderer in listOfTargetsInCombatSphere[_selected].rendereManager.listOfRenderers)
            {
                _renderer.material = listOfTargetsInCombatSphere[_selected].matCharacterSelected;
            }
        }
        private void HighlightAll(int _temp)
        {
            RemoveAllHighlights();
            foreach (UnitController _target in listOfTargetsInCombatSphere)
            {
                if (_target.GetCharacterType() != CharacterType.Player)
                {
                    foreach (Renderer _renderer in _target.rendereManager.listOfRenderers)
                    {
                        _renderer.material = _target.matCharacterInCombatSphere;
                    }
                }
            }
        }
        public void RemoveAllHighlights()
        {
            if(listOfAllPotentialTargets.Count != 0)
            {
                foreach (UnitController _target in listOfAllPotentialTargets)
                {
                    if (_target.GetCharacterType() != CharacterType.Player)
                    {
                        foreach(Renderer _renderer in _target.rendereManager.listOfRenderers)
                        {
                            _renderer.material = _target.matCharacterBase;
                        }
                    }
                }
            }
        }
    }
}