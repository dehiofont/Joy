using FomeCharacters;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CombatUIMenu
{
    private List<Canvas> canvases;
    private List<TextMeshProUGUI> textList;
    private Image selector;

    private Vector3 selectorPos;
    private float selectedTextYPos;

    private List<string> stringList = new List<string>();
    private int currentSelection = 0;

    public event EventHandler OnArmamentSelectionChange;

    public CombatUIMenu
        (List<Canvas> _canvas,
        List<TextMeshProUGUI> _textList, 
        Image _selector)
    {
        canvases = _canvas;
        textList = _textList;
        selector = _selector;
    }

    //private void CMC_OnArmamentSelectionChange()

    public void GenerateTextList(List<UnitController> _targets)
    {
        stringList.Clear();
        for (int i = 0; i < _targets.Count; i++)
        {
            stringList.Add(_targets[i].GetName());
        }
        PopulateTextList();
    }

    public void GenerateTextList(List<Armament> _armaments)
    {
        stringList.Clear();
        for (int i = 0; i < _armaments.Count; i++)
        {
            stringList.Add(_armaments[i].GetName());
        }
        PopulateTextList();
    }

    public void PopulateTextList()
    {
        foreach(TextMeshProUGUI _text in textList)
        {
            _text.SetText("");
        }

        for(int i = 0; i < stringList.Count;i++)
        {
            textList[i].SetText(stringList[i]);
        }
    }

    public void TurnOnOrOffCanvases(int _state)
    {
        foreach (Canvas canvas in canvases)
        {
            if (_state == 0)
            {
                canvas.enabled = false;
            }
            else if(_state == 1)
            {
                canvas.enabled = true;
            }
        }
    }
    public void TurnOnOrOffTexts(int _state)
    {
        foreach (TextMeshProUGUI _text in textList)
        {
            if (_state < 1)
            {
                _text.enabled = false;
            }
            else
            {
                _text.enabled = true;
            }
        }
    }
    public void TurnOnOrOffSelector(int _state)
    {
        if (_state < 1)
        {
            selector.enabled = false;
        }
        else
        {
            selector.enabled = true;
        }
    }
    public void TurnOnOffMenu(int _state)
    {
        if(_state < 1)
        {
            TurnOnOrOffCanvases(0);
            TurnOnOrOffTexts(0);
            TurnOnOrOffSelector(0);
        }
        else
        {
            TurnOnOrOffCanvases(1);
            TurnOnOrOffTexts(1);
            TurnOnOrOffSelector(1);
        }
    }
    public void SetSelectorPosition(int _selectedText)
    {
        selectorPos = selector.transform.localPosition;
        selectedTextYPos = textList[_selectedText].transform.localPosition.y;
        selector.transform.localPosition = new Vector3(selectorPos.x, selectedTextYPos, selectorPos.z);
    }
}
