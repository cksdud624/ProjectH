using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Scenario", menuName = "Table Loader/Scenario Loader")]
public class ScenarioLoader : ScriptableObject
{
    [SerializeField]
    private string tableName;
    public string TableName { get { return tableName; } set { tableName = value; } }

    [SerializeField]
    private List<ScenarioData> scenarioDatas;
    public List<ScenarioData> ScenarioDatas { get { return scenarioDatas; } set { scenarioDatas = value; } }
}