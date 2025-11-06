using FomeCharacters;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private string name;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float projectileLife;
    [SerializeField] private float lifeTimer;
    [SerializeField] private bool projectileActive = false;
    [SerializeField] private bool projectileJustFired;
    [SerializeField] private UnitController target;
    [SerializeField] private GameObject model;

    private ProjectileRepository pRepository;

    public enum ProjectileTypes
    {
        Arrow,
        CanonBall
    }

    [SerializeField] private ProjectileTypes projectileType;
    void Start()
    {
        lifeTimer = projectileLife;
    }

    void Update()
    {
        if(projectileActive == true)
        {
            if(projectileJustFired == true)
            {
                model.SetActive(true);
                //pRepository.AddActiveProjectile(this);
                gameObject.transform.LookAt(target.transform.position);
                lifeTimer = 0;

                projectileJustFired = false;
            }

            lifeTimer += Time.deltaTime;

            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

            if(projectileLife < lifeTimer ||
                Vector3.Distance(gameObject.transform.position, target.transform.position) < 0.5f)
            {
                DectivateProjectile();
            }
        }
    }

    public string GetName()
    {
        return name; 
    }
    public ProjectileTypes GetProjectileType()
    {
        return projectileType; 
    }
    public void ActivateProjectile(UnitController _target)
    {
        //gameObject.SetActive(true);
        target = _target;
        projectileActive = true;
        projectileJustFired = true;
        pRepository.RemoveDeactivatedProjectile(this);
        //pRepository.AddActiveProjectile(this);
    }
    public void DectivateProjectile()
    {
        projectileActive = false;
        pRepository.AddDeactivatedProjectile(this);
        lifeTimer = 0;
        model.SetActive(false);
    }

    public void SetPRepositoryRef(ProjectileRepository _pRepository)
    {
        pRepository = _pRepository;
        pRepository.allProjectiles.Add(this);
    }

}
