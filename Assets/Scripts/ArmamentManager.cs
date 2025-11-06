using FomeCharacters;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ArmamentManager : MonoBehaviour
{
    [SerializeField] private List<Armament> armaments = new List <Armament>();
    [SerializeField] private List<float> armamentCoolDowns = new List <float>();

    private Armament selectedArmament;

    private ProjectileManager projectileManager;

    [SerializeField] private List<GameObject> firingLocations;

    private UnitController target;

    private void Start()
    {        
        projectileManager = GetComponent<ProjectileManager>();

        Armament Cannon = new Armament("Cannon", 20, 2, Projectile.ProjectileTypes.CanonBall);
        Armament Archer = new Armament("Archer", 25, 1.5f, Projectile.ProjectileTypes.Arrow);
        AddArmament(Cannon);
        AddArmament(Archer);

        Event.OnArmamentSelectionChange += SetSelectedArmament;
        Event.OnProjectileFire += SetArmamentActive;
    }
    private void SetArmamentActive(UnitController _target)
    {
        target = _target;
        selectedArmament.ToggleActivation(1);
    }

    private void SetSelectedArmament(int _selected)
    {
        selectedArmament = armaments[_selected];
    }

    public void AddArmament(Armament _armamaent)
    {         
        armaments.Add(_armamaent);
        armamentCoolDowns.Add(0);
    }
    public void removeArmament(Armament _armamaent)
    {
        armaments.Remove(_armamaent);
    }

    public List<Armament> getArmaments()
    {
        return armaments;
    }
    void Update()
    {
        foreach (Armament _armament in armaments)
        {
            if (_armament != null && _armament.GetArmamentStatus() == true)
            {
                _armament.addToShotTimer(Time.deltaTime);
                if (_armament.GetShotTimer() > _armament.GetFrequency())
                {
                    projectileManager.PrepareProjectile(
                        firingLocations[armaments.IndexOf(_armament)].transform.position,
                        target,
                        _armament.projectileType);

                    _armament.resetShotTimer();
                }
            }
        }

        for(int i = 0; i < armaments.Count; i++)
        {
            armamentCoolDowns[i] = armaments[i].GetShotTimer();
        }
    }
}
