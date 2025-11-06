using FomeCharacters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public static class Event
{
    // PLAYER SPAWNED

    public static Action<UnitController> OnPlayerSpawn;
    //public static Action

    // COMBAT SPHERE EVENTS

    public static Action OnCombatSphereOpen;
    public static Action<bool> ToggleNoTargetsInSphere;
    public static Action OnCombatSphereClose;

    public static Action<int> OnArmamentSelectionChange;
    public static Action<int> OnTargetSelectionChange;
    public static Action<int> OnPartSelectionChange;
    //public static Action<Armament, UnitController> OnProjectileFire;
    public static Action<UnitController> OnProjectileFire;

    public static Action OnUpdate;

    // INPUT MANAGER

    // PROJECTILES
    public static Action<ProjectileRepository> OnProjectileRepositorySpawn;

    // DETECTOR

    public static  Action OnDetectionFinish;
    public static  Action<List<UnitController>> OnTargetDetectionFinish;
    public static  Action<UnitController, List<UnitController>> OnPartsDetectionFinish;


    // public static Action 

}

