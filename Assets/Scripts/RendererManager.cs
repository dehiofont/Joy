using FomeCharacters;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RendererManager : MonoBehaviour
    {
        public List<Renderer> listOfRenderers = new List<Renderer>();
        [SerializeField] UnitController UnitController;
    }
}