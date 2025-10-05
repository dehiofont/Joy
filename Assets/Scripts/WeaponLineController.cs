using FomeCharacters;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class WeaponLineController : MonoBehaviour
{
    //private GameObject weaponLineRendererTarget1;
    //private GameObject weaponLineRendererTarget2;
    //private GameObject weaponLineRendererTarget3;
    //private GameObject weaponLineRendererTarget4;

    //bool lineRenderersActive = false;

    //public List<GameObject> _enemiesInCombatSphere;
    //public GameObject currentlySelectedObject;

    //public List<LineRenderer> weaponLineRenderers = new List<LineRenderer>();

    //[SerializeField] float maxWeaponDistance;

    //private void Start()
    //{
    //    foreach (LineRenderer lineRenderer in weaponLineRenderers)
    //    {
    //        lineRenderer.enabled = false;
    //    }
    //}
    ////public WeaponLineController(List<UnitController> _listOfTargets)
    ////{
    ////    foreach(LineRenderer lineRenderer in weaponLineRenderers)
    ////    {
    ////        lineRenderer.enabled = false;
    ////    }
    ////}
    //private void Update()
    //{
    //    if(lineRenderersActive == true)
    //    {
    //        UpdateLineRendererPositions();
    //    }
    //}
    //public void AttachToFoundEnemy()
    //{
    //    lineRenderersActive = true;
    //    _enemiesInCombatSphere = GameManager.Instance.listOfAllTargets;
    //    currentlySelectedObject = _enemiesInCombatSphere[GameManager.Instance.PlayerCombatSphereSelectionController.currentSelectedSlot];

    //    //weaponLineRendererTarget1 = currentlySelectedObject;

    //    weaponLineRenderers[0].enabled = true;
    //    CheckIfAnyLineRenderersAreActive();
    //}
    //private void UpdateLineRendererPositions()
    //{
    //    foreach (LineRenderer _weaponLineRenderer in weaponLineRenderers)
    //    {
    //        if (_weaponLineRenderer.enabled == true)
    //        {
    //            Vector3 _startPosition = currentlySelectedObject.transform.position;
    //            Vector3 _endPosition = GameManager.Instance.Player.transform.position;

    //            if (Vector3.Distance(_startPosition, _endPosition) > maxWeaponDistance)
    //            {
    //                _weaponLineRenderer.enabled = false;
    //                CheckIfAnyLineRenderersAreActive();
    //            }
    //            else
    //            {
    //                _weaponLineRenderer.SetPosition(1, _startPosition);
    //                _weaponLineRenderer.SetPosition(0, _endPosition);
    //            }
    //        }
    //    }
    //}
    //private void CheckIfAnyLineRenderersAreActive()
    //{
    //    foreach (LineRenderer lineRenderer in weaponLineRenderers)
    //    {
    //        lineRenderersActive = lineRenderer.enabled;
    //        if(lineRenderersActive == true)
    //        {
    //            break;
    //        }
    //    }
    //}
}
