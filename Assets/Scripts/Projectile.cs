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
    private Vector3 startPoint = new Vector3(0, 0, 0);

    private ProjectileRepository pRepository;

    public enum ProjectileTypes
    {
        Arrow = 0,
        CanonBall = 1,
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
            lifeTimer += Time.deltaTime;

            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

            if(projectileLife < lifeTimer ||
                (gameObject.transform.position - target.transform.position).sqrMagnitude < (0.5f * 0.5f))
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
    public void ActivateProjectile(UnitController _target, Vector3 _startPoint)
    {
        lifeTimer = 0;
        gameObject.transform.position = _startPoint;
        target = _target;
        gameObject.transform.LookAt(target.transform.position);
        model.SetActive(true);
        projectileActive = true;
        pRepository.RemoveDeactivatedProjectile(this);
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
