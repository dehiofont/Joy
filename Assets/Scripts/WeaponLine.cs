using FomeCharacters;
using System.Collections.Generic;
using UnityEngine;
public class WeaponLine
{
    private LineRenderer weaponLine;
    private int armamentSlotNum;
    private UnitController player;
    private List<Armament> armaments = new List<Armament>();

    private bool weaponLineIsFieldActive = false;
    private bool weaponLineIsPreviewActive = false;

    private UnitController target = new UnitController();

    private Vector3 startPosition = new Vector3(0,0,0);
    private Vector3 endPosition = new Vector3(0,0,0);

    private float bufferRange = 5;

    private Material material;
    private Renderer renderer;
    private float scrollSpeed = 4;
    private float offset = 0;
    private float blinkTimer = 0;
    private float blinkThreshold = .4f;
    public WeaponLine(LineRenderer _weaponLine, int _armamentSlotNum, UnitController _player)
    {
        weaponLine = _weaponLine;
        armamentSlotNum = _armamentSlotNum;
        player = _player;
        armaments = player.playerArmaments;

        renderer = weaponLine.GetComponent<Renderer>();
        material = renderer.material;

        weaponLine.enabled = false;
    }
    public void SetTarget(UnitController _target)
    {
        target = _target;
        ToggleLineVisual(1);
        Debug.Log(
            $"WeaponLine {weaponLine} Armament {armaments[armamentSlotNum]}");
    }
    private void UpdateLineRendererPositions()
    {

        startPosition = player.GetUnitPos();
        endPosition = target.GetTargetPoint();
        offset = Time.time * scrollSpeed;
        blinkTimer += Time.deltaTime;

        if (Vector3.Distance(startPosition, endPosition) > (armaments[armamentSlotNum].GetRange() + bufferRange))
        {
            ToggleFieldActive(0);
            armaments[armamentSlotNum].ToggleActivation(0);
        }
        else
        {
            weaponLine.SetPosition(1, startPosition);
            weaponLine.SetPosition(0, endPosition);

            material.mainTextureOffset = new Vector2(offset, 0);
            if (Vector3.Distance(startPosition, endPosition) > armaments[armamentSlotNum].GetRange()
                && blinkTimer > blinkThreshold)
            {
                if(renderer.enabled == true)
                {
                    renderer.enabled = false;
                }
                else
                {
                    renderer.enabled = true;
                }
                blinkTimer = 0;
            }
        }
    }
    public void ToggleLineVisual(int _toggle)
    {
        if(_toggle > 0)
        {
            weaponLine.enabled = true;
            weaponLineIsPreviewActive = true;

            Event.OnUpdate += UpdateLineRendererPositions;
        }
        else
        {
            weaponLine.enabled = false;
            weaponLineIsPreviewActive = false;
            weaponLineIsFieldActive = false;

            Event.OnUpdate -= UpdateLineRendererPositions;
        }
    }

    public void ToggleFieldActive(int _toggle)
    {
        if(_toggle > 0)
        {
            weaponLineIsFieldActive = true;
        }
        else
        {
            ToggleLineVisual(0);
        }
    }
    public int GetSlotNum()
    {
        return armamentSlotNum;
    }
    public bool GetFieldActiveStatus()
    {
        return weaponLineIsFieldActive;
    }
    public bool GetPreviewActiveStatus()
    {
        return weaponLineIsPreviewActive;
    }
}
