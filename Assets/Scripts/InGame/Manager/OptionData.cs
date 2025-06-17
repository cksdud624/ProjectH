using System;
using UnityEngine;

public class OptionData
{
    [SerializeField]
    private float textSpeed;
    public float TextSpeed 
    { 
        get { return textSpeed; }
        set { textSpeed = value; }
    }
}
