using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEdible : IInteractable
{
    public float foodValue { get; set; }

    //public abstract void Eat(GameObject _eater);
}
