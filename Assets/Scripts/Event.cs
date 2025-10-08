using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class Event
{
    // COMBAT SPHERE EVENTS

    public static Action OnCombatSphereOpen;
    public static Action OnNoTargetsInSphere;
    public static Action OnCombatSphereClose;

    public static Action<int> OnArmamentSelectionChange;
    public static Action<int> OnTargetSelectionChange;
    public static Action<int> OnPartSelectionChange;

    public static Action OnUnitControllersFound;

    public static Action OnUpdate;

    // INPUT MANAGER

    // public static Action 

}

