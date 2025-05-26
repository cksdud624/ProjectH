using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Scenario", menuName = "Table Loader/Scenario Loader")]
public class ScenarioLoader : ScriptableObject
{
    [SerializeField]
    private string tableName;
    public string TableName { get { return tableName; } set { tableName = value; } }

    [SerializeField]
    private List<string> scenarioTexts;
    public List<string> ScenarioTexts { get { return scenarioTexts; } }
}