using UnityEditor;
using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;

[CustomEditor(typeof(ScenarioLoader))]
public class ScenarioLoaderInspector : Editor
{
    private ScenarioLoader mLoader;
    private string mTableName;

    protected virtual void OnEnable()
    {
        mLoader = (ScenarioLoader)target;
        mTableName = mLoader.TableName;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ScenarioLoader loader = (ScenarioLoader)target;
        float width = EditorGUIUtility.currentViewWidth;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Table Name", GUILayout.Width(width * 0.3f));
        string newTableName = EditorGUILayout.DelayedTextField(mTableName, GUILayout.Width(width * 0.7f - 25f));
        if(newTableName != mTableName)
        {
            mLoader.TableName = newTableName;
            mTableName = newTableName;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("½Ò¼þÀÌ Ã´°á"))
        {
            LoadExcelFile();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void LoadExcelFile()
    {
        string filePath = Application.dataPath + "/Table/Scenario/" + mLoader.TableName + ".xlsx";
        if (!File.Exists(filePath))
        {
            Debug.Log("¿¢¼¿ ÆÄÀÏ ¾øÀ½");
            return;
        }

        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
        DataSet result = reader.AsDataSet();

        for(int sheet = 0; sheet < result.Tables.Count; sheet++)
        {
            for(int row = 0; row < result.Tables[sheet].Rows.Count; row++)
            {
                string data1 = result.Tables[sheet].Rows[row][0].ToString();
                Debug.Log(data1);
            }
        }
        
    }
}