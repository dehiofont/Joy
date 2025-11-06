using FomeCharacters;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileRepository : MonoBehaviour
{
    [SerializeField] List<Projectile> activeProjectiles = new List<Projectile>();
    [SerializeField] public List<Projectile> allProjectiles = new List<Projectile>();
    [SerializeField] public List<Projectile> deactiveProjectiles = new List<Projectile>();
    [SerializeField] public List<GameObject> projectilePrefabs = new List<GameObject>();
    private void Start()
    {
        Event.OnProjectileRepositorySpawn?.Invoke(this);
    }
    public void AddActiveProjectile(Projectile _projectile)
    {
        if(activeProjectiles.Contains(_projectile) == false)
        {
            activeProjectiles.Add(_projectile);
        }
    }
    public void AddDeactivatedProjectile(Projectile _projectile)
    {
        if (deactiveProjectiles.Contains(_projectile) == false)
        {
            deactiveProjectiles.Add(_projectile);
        }
    }
    public void RemoveActiveProjectile(Projectile _projectile)
    {
        if (activeProjectiles.Contains(_projectile) == true)
        {
            activeProjectiles.Remove(_projectile);
        }
    }
    public void RemoveDeactivatedProjectile(Projectile _projectile)
    {
        if (deactiveProjectiles.Contains(_projectile) == true)
        {
            deactiveProjectiles.Remove(_projectile);
        }
    }
}
