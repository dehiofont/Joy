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



        //CombatSphereController.OnArmamentSelectionChangeEventArgs += CMC_OnArmamentSelectionChange;
    }

    //private void CMC_OnArmamentSelectionChange()

    private void GenerateTexList<T>(List<T> _textList)
    {
        stringList.Clear();

        Armament currentArmament;
        UnitController unitController;

        for(int i = 0; i < _textList.Count; i++)
        {
            if (_textList[i] is Armament)
            {
                currentArmament = _textList[i] as Armament;
                stringList.Add(currentArmament.GetName());                
            }
            else if(_textList[i] is UnitController)
            {
                unitController = _textList[i] as UnitController;
                stringList.Add(unitController.GetName());                
            }
        }
        PopulateTextList();
    }

    public void GenerateTextList(List<Armament> _armament)
    {
        stringList.Clear();
        for (int i = 0; i < _armament.Count; i++)
        {
            stringList.Add(_armament[i].GetName());
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
}
