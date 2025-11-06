using FomeCharacters;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileManager : MonoBehaviour
{
    private UnitController unitController;

    private List<UnitController> targetsInCombatSphere;
    private UnitController targetUnitController;
    private UnitController partUnitController;

    private ProjectileRepository pRepository;
    private List<Projectile> deactiveProjectiles = new List<Projectile>();

    private bool foundProjectileMatch = false;

    private void Awake()
    {
        Event.OnProjectileRepositorySpawn += GetProjectileRepository;
    }
    void Start()
    {
        unitController = GetComponent<UnitController>();
    }
    private void GetProjectileRepository(ProjectileRepository _pRepository)
    {

        pRepository = _pRepository;
        deactiveProjectiles = pRepository.deactiveProjectiles;
    }

    //FIND INACTIVE OR CREATE A NEW PROJECTILE
    public void PrepareProjectile(
        Vector3 _origin,
        UnitController _target, 
        Projectile.ProjectileTypes _projectileType)
    {
        foundProjectileMatch = false;

        if (deactiveProjectiles.Count > 0)
        {
            foreach (Projectile _projectile in deactiveProjectiles)
            {
                if (_projectile.GetProjectileType() == _projectileType && foundProjectileMatch == false)
                {
                    FireProjectile(_projectile, _target, _origin);
                    foundProjectileMatch = true;
                    break;
                }               
            }
        }
        

        if(foundProjectileMatch == false)
        {
            foreach (GameObject _gameObject in pRepository.projectilePrefabs)
            {
                if (_gameObject.GetComponent<Projectile>().GetProjectileType() == _projectileType)
                {
                    GameObject newProjectile = Instantiate(_gameObject, _origin, Quaternion.identity);
                    newProjectile.GetComponent<Projectile>().SetPRepositoryRef(pRepository);
                    //pRepository.AddActiveProjectile(newProjectile.GetComponent<Projectile>());
                    FireProjectile(newProjectile.GetComponent<Projectile>(), _target, _origin);
                }
            }
        }
    }

    //ACTIVATE AND START PROJECTILE
    private void FireProjectile(Projectile projectile, UnitController _target, Vector3 _startPoint)
    {
        projectile.ActivateProjectile(_target, _startPoint);
    }
}
