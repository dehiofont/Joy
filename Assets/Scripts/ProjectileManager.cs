using FomeCharacters;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ProjectileManager : MonoBehaviour
{
    private UnitController unitController;

    private List<UnitController> targetsInCombatSphere;
    private UnitController targetUnitController;
    private UnitController partUnitController;

    private ProjectileRepository pRepository;
    private List<Projectile> deactiveProjectiles = new List<Projectile>();

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
    public void PrepareProjectile(
        Vector3 _origin,
        UnitController _target, 
        Projectile.ProjectileTypes _projectileType)
    {
        bool foundProjectileMatch = false;
        if (deactiveProjectiles.Count > 0)
        {
            Debug.Log("inside the reuse part");
            foreach (Projectile _projectile in deactiveProjectiles)
            {
                if (_projectile.GetProjectileType() == _projectileType && foundProjectileMatch == false)
                {
                    FireProjectile(_projectile, _target);
                    deactiveProjectiles.Remove(_projectile);
                    //pRepository.AddActiveProjectile(_projectile);
                    foundProjectileMatch = true;
                    break;
                }               
            }
        }
        

        if(foundProjectileMatch == false)
        {
            Debug.Log("inside the create new part");
            CreateNewProjectile(_origin, _target, _projectileType);
        }
    }
    private void CreateNewProjectile(
        Vector3 _origin,
        UnitController _target,
        Projectile.ProjectileTypes _projectileType)
    {
        foreach (GameObject _gameObject in pRepository.projectilePrefabs)
        {
            if (_gameObject.GetComponent<Projectile>().GetProjectileType() == _projectileType)
            {
                GameObject newProjectile = Instantiate(_gameObject, _origin, Quaternion.identity);
                newProjectile.GetComponent<Projectile>().SetPRepositoryRef(pRepository);
                //pRepository.AddActiveProjectile(newProjectile.GetComponent<Projectile>());
                FireProjectile(newProjectile.GetComponent<Projectile>(), _target);
            }
        }
    }

    private void FireProjectile(Projectile projectile, UnitController _target)
    {
        projectile.ActivateProjectile(_target);
    }
}
