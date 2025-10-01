using FomeCharacters;
using UnityEngine;

public class CombatSphereBubble : MonoBehaviour
{
    [SerializeField] UnitController characterController;
    [SerializeField] TargetDetector CSEnemyDetector;
    [SerializeField] float combatSphereMaxSize;
    [SerializeField] float combatSphereMinSize;
    [SerializeField] float combatSphereSpeedScaler = 1;
    [SerializeField] int resizeDirection = 1;
    [SerializeField] float combatSphereSingleVector;
    [SerializeField] GameManager GameManager;

    private GameObject combatSphere;
    public bool combatSphereOn = false;

    private void Start()
    {
        combatSphereSingleVector = combatSphereMinSize;
        SetCombatSphereSize();
    }
    private void Update()
    {
            
    }

    public void StartResizingCombatSphere(int _direction)
    {
        resizeDirection = _direction;
    }
    private void MaintainCombatSphereBounds()
    {
        if (combatSphereSingleVector > combatSphereMaxSize)
        {
            combatSphereSingleVector = combatSphereMaxSize;
        }
        else
        {
            combatSphereSingleVector = combatSphereMinSize;
        }

        SetCombatSphereSize();
    }

    private void SetCombatSphereSmall()
    {
        combatSphereSingleVector = combatSphereMaxSize;
    }
    private void SetCombatSphereLarge()
    {
        combatSphereSingleVector = combatSphereMinSize;
    }

    private void SetCombatSphereSize()
    {
        combatSphere.transform.localScale = new Vector3(combatSphereSingleVector, combatSphereSingleVector, combatSphereSingleVector);
    }
    private void ResizeCombatSphere()
    {
        combatSphereSingleVector += Time.deltaTime * (combatSphereSpeedScaler * resizeDirection);

        if (combatSphereSingleVector < combatSphereMinSize || combatSphereSingleVector > combatSphereMaxSize)
        {
            MaintainCombatSphereBounds();
        }
        else
        {
            SetCombatSphereSize();
        }
    }
}
