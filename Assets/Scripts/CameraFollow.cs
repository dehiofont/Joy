using FomeCharacters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] GameObject player;

        [SerializeField] float cameraLerpTime = .5f;
        private Vector3 currentCameraPos = new Vector3(0,0,0);
        private float elapsedTime;
        private float percentageComplete;
        private bool cameraLerpingActive = false;
        [SerializeField] private AnimationCurve curve;

        [SerializeField] Vector3 playerOffset;
        [SerializeField] Vector3 largeTargetOffset;
        [SerializeField] Vector3 mediumTargetOffset;
        [SerializeField] Vector3 smallTargetOffset;

        private int selectedTarget;
        private int selectedPart;

        private List<UnitController> targetsInCombatSphere = new List<UnitController>();

        private GameObject cameraTarget;
        private Vector3 cameraOffset;
        void Start()
        {
            cameraOffset = playerOffset;
            cameraTarget = player;

            Event.OnTargetDetectionFinish += GetTargets;
            Event.OnTargetSelectionChange += PrepTargetForCamera;
            Event.OnPartSelectionChange += PrepPartForCamera;

            Event.OnArmamentSelectionChange += PrepPlayerForCamera;
            Event.OnProjectileFire += PrepPlayerForCamera;
            Event.OnCombatSphereClose += PrepPlayerForCamera;
        }

        private void GetTargets(List<UnitController> _targetsInCombatSphere)
        {
            targetsInCombatSphere = _targetsInCombatSphere;
        }

        private void PrepTargetForCamera(int _selected)
        {
            selectedTarget = _selected;
            cameraOffset = mediumTargetOffset;
            cameraTarget = targetsInCombatSphere[selectedTarget].gameObject;
            LerpToNewCameraPos();
        }
        private void PrepPartForCamera(int _selected)
        {
            selectedPart = _selected;
            cameraOffset = smallTargetOffset;
            cameraTarget = targetsInCombatSphere[selectedTarget].parts[selectedPart].gameObject;
            LerpToNewCameraPos();
        }
        private void PrepPlayerForCamera(UnitController _target)
        {
            cameraTarget = player;
            cameraOffset = playerOffset;
            LerpToNewCameraPos();
        }
        private void PrepPlayerForCamera()
        {
            cameraTarget = player;
            cameraOffset = playerOffset;
            LerpToNewCameraPos();
        }
        private void PrepPlayerForCamera(int _selected)
        {
            cameraOffset = playerOffset;
            cameraTarget = player;
            LerpToNewCameraPos();
        }

        private void LerpToNewCameraPos()
        {
            currentCameraPos = gameObject.transform.position;
            elapsedTime = 0;
            cameraLerpingActive = true;
        }

        private void LateUpdate()
        {
            
            if(cameraLerpingActive == true)
            {
                elapsedTime += Time.deltaTime;
                percentageComplete = elapsedTime / cameraLerpTime;

                transform.position = Vector3.Lerp(
                    currentCameraPos,
                    cameraTarget.transform.position + cameraOffset,
                    //Mathf.SmoothStep(0,1,percentageComplete));
                    curve.Evaluate(percentageComplete));

                if(percentageComplete >= 1)
                {
                    cameraLerpingActive = false;
                }
            }
            else
            {
                transform.position = new Vector3(
                    cameraTarget.transform.position.x + cameraOffset.x,
                    cameraTarget.transform.position.y + cameraOffset.y,
                    cameraTarget.transform.position.z + cameraOffset.z);
            }
        }
    }
}