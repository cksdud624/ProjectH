using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]//
public class ScenarioData
{
    [SerializeField]
    private string nameData;
    public string Name { get { return nameData; } set { nameData = value; } }
    //
    [SerializeField]
    private string text;
    public string Text { get { return text; } set { text = value; } }

    [SerializeField]
    private List<ScenarioCommand> commands;
    public List<ScenarioCommand> Commands { get {  return commands; } set { commands = value; } }
}