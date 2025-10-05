using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            listOfTargetsInCombatSphere[_selected].SetCharacterSelectedMat();
        }
        private void HighlightAll(int _temp)
        {
            RemoveAllHighlights();
            for (int i = 0; i < listOfTargetsInCombatSphere.Count; i++)
            {
                listOfTargetsInCombatSphere[i].SetCharacterInCombatSphereMat();
            }
        }
        public void RemoveAllHighlights()
        {
            foreach (UnitController _target in listOfAllPotentialTargets)
            {
                _target.SetCharacterBaseMat();
            }
        }
    }
}