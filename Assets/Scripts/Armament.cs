using UnityEngine;

public class Armament
{
    private string name;
    private float range;
    private bool active;
    private float frequency = .5f;
    private float timeSinceLastShot;
    public Projectile.ProjectileTypes projectileType;
    private int armamentSlotNum;
    public Armament(string _name, float _range, float _frequency, Projectile.ProjectileTypes _projectileType)
    {
        name = _name; 
        range = _range;        
        frequency = _frequency;
        projectileType = _projectileType;

        timeSinceLastShot = frequency;
    }

    public string GetName()
    {
        return name;
    }

    public float GetRange()
    {
        return range;
    }
    public float GetFrequency()
    {
        return frequency;
    }
    //public new Projectile.ProjectileTypes GetType()
    //{
    //    return projectileType;
    //}
    public bool GetArmamentStatus()
    {
        return active;
    }
    public void ToggleActivation(int _toggle)
    {
        if (_toggle > 0)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }
    public float GetShotTimer()
    {
        return timeSinceLastShot;
    }
    public void addToShotTimer(float _time)
    {
        timeSinceLastShot += _time;
    }
    public void resetShotTimer()
    {
        timeSinceLastShot = 0;
    }
}
