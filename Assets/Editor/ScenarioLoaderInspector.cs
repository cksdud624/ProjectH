using ExcelDataReader;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(ScenarioLoader))]
public class ScenarioLoaderInspector : Editor
{
    private ScenarioLoader mLoader;
    private string mTableName;
    
    private Dictionary<eScenarioColumn, int> mMapping = new Dictionary<eScenarioColumn, int>();

    private SerializedProperty mScenarioDatasProperty;
    private ReorderableList mScenarioDatasList;
    private Dictionary<int, ReorderableList> mCommandsLists = new();
    private Dictionary<int, bool> mIsFolded = new();

    private static bool mIsFoldout;

    protected virtual void OnEnable()
    {
        mLoader = (ScenarioLoader)target;
        mTableName = mLoader.TableName;

        mScenarioDatasProperty = serializedObject.FindProperty("scenarioDatas");
        mScenarioDatasList = new ReorderableList(serializedObject, mScenarioDatasProperty);

        mScenarioDatasList.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Scenario Datas");
        };

        mScenarioDatasList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = mScenarioDatasList.serializedProperty.GetArrayElementAtIndex(index);

            SerializedProperty nameProperty = element.FindPropertyRelative("nameData");
            SerializedProperty textProperty = element.FindPropertyRelative("text");
            SerializedProperty commandsProperty = element.FindPropertyRelative("commands");

            //ÀÌ¸§
            Rect nameRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(nameRect, nameProperty, new GUIContent("Name"));

            //ÅØ½ºÆ®
            Rect textRect = new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 4f, rect.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(textRect, textProperty, new GUIContent("Text"));

            if (!mIsFolded.ContainsKey(index))
                mIsFolded.Add(index, false);

            mIsFolded[index] = EditorGUI.Foldout(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2 + 8f, rect.width,
                EditorGUIUtility.singleLineHeight), mIsFolded[index], "Scenario Commands");

            //Ä¿¸ÇÆ® ¸®½ºÆ®
            if (mIsFolded[index])
            {
                ReorderableList commandsList = new ReorderableList(element.serializedObject, commandsProperty);
                commandsList.drawHeaderCallback = (Rect r) =>
                {
                    EditorGUI.LabelField(r, "Scenario Commands");
                };

                commandsList.drawElementCallback = (Rect r, int i, bool a, bool f) =>
                {
                    if (mIsFolded[index])
                    {
                        SerializedProperty commandElement = commandsList.serializedProperty.GetArrayElementAtIndex(i);

                        SerializedProperty targetProperty = commandElement.FindPropertyRelative("target");
                        SerializedProperty idProperty = commandElement.FindPropertyRelative("id");
                        SerializedProperty actionProperty = commandElement.FindPropertyRelative("action");
                        SerializedProperty effectProperty = commandElement.FindPropertyRelative("effect");
                        SerializedProperty usePositionProperty = commandElement.FindPropertyRelative("usePosition");
                        SerializedProperty positionProperty = commandElement.FindPropertyRelative("position");
                        SerializedProperty delayProperty = commandElement.FindPropertyRelative("delay");
                        //Å¸°Ù
                        Rect targetRect = new Rect(r.x, r.y, r.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(targetRect, targetProperty, new GUIContent("Target"));
                        //ID
                        Rect idRect = new Rect(r.x, r.y + EditorGUIUtility.singleLineHeight + 4, r.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(idRect, idProperty, new GUIContent("ID"));
                        //¾×¼Ç
                        Rect actionRect = new Rect(r.x, r.y + EditorGUIUtility.singleLineHeight * 2 + 8, r.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(actionRect, actionProperty, new GUIContent("Action"));
                        //ÀÌÆåÆ®
                        Rect effectRect = new Rect(r.x, r.y + EditorGUIUtility.singleLineHeight * 3 + 12, r.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(effectRect, effectProperty, new GUIContent("Effect"));
                        //À§Ä¡ »ç¿ë
                        Rect usePositionRect = new Rect(r.x, r.y + EditorGUIUtility.singleLineHeight * 4 + 16, r.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(usePositionRect, usePositionProperty, new GUIContent("Use Position"));
                        //À§Ä¡
                        Rect positionRect = new Rect(r.x, r.y + EditorGUIUtility.singleLineHeight * 5 + 20, r.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(positionRect, positionProperty, new GUIContent("Position"));
                        //µô·¹ÀÌ
                        Rect delayRect = new Rect(r.x, r.y + EditorGUIUtility.singleLineHeight * 7 + 28, r.width, EditorGUIUtility.singleLineHeight);
                        EditorGUI.PropertyField(delayRect, delayProperty, new GUIContent("Delay"));
                    }
                };

                commandsList.elementHeight = EditorGUIUtility.singleLineHeight * 8 + 32f;
                mCommandsLists[index] = commandsList;


                Rect commandsRect = new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2 + 8f, rect.width, mCommandsLists[index].GetHeight());
                mCommandsLists[index].DoList(commandsRect);
            }
        };

        mScenarioDatasList.elementHeightCallback = (index) => 
        {
            if (mIsFolded.ContainsKey(index) && mIsFolded[index])
            {
                ReorderableList list = mCommandsLists.ContainsKey(index) ? mCommandsLists[index] : null;
                if (list != null)
                {
                    return EditorGUIUtility.singleLineHeight + list.GetHeight() + 30f;
                }
            }
            return EditorGUIUtility.singleLineHeight * 3 + 10f;
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //¿¢¼¿ ÀÌ¸§
        float width = EditorGUIUtility.currentViewWidth;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Table Name", GUILayout.Width(width * 0.3f));
        string newTableName = EditorGUILayout.DelayedTextField(mTableName, GUILayout.Width(width * 0.7f - 40f));
        if(newTableName != mTableName)
        {
            mLoader.TableName = newTableName;
            mTableName = newTableName;
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();

        //¹öÆ°
        if(mLoader.ScenarioDatas == null)
            mLoader.ScenarioDatas = new List<ScenarioData>();

        if (GUILayout.Button("½Ò¼þÀÌ Ã´°á"))
        {
            LoadExcelFile();
            EditorUtility.SetDirty(mLoader);
            AssetDatabase.SaveAssets();
        }

        //½Ã³ª¸®¿À µ¥ÀÌÅÍ
        mIsFoldout = EditorGUILayout.Foldout(mIsFoldout, "Items", true);
        if (mIsFoldout)
        {
            EditorGUILayout.BeginVertical(new GUIStyle("button"));
            mScenarioDatasList.DoLayoutList();
            EditorGUILayout.EndVertical();
        }


        serializedObject.ApplyModifiedProperties();
    }

    private void LoadExcelFile()
    {
        string directoryPath = Application.dataPath + "/Table/Scenario/";
        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = directoryPath + mLoader.TableName + ".xlsx";
        if (!File.Exists(filePath))
        {
            Debug.Log("¿¢¼¿ ÆÄÀÏ ¾øÀ½");
            return;
        }
        mLoader.ScenarioDatas.Clear();

        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
        DataSet result = null;
        result = reader.AsDataSet();

        for(int sheet = 0; sheet < result.Tables.Count; sheet++)
        {
            if (result.Tables[sheet].Rows.Count <= 0)
            {
                Debug.Log("Reading Error");
                return;
            }

            mMapping.Clear();
            ScenarioData newData = null;
            bool isGroup = false;

            MappingColumn(result.Tables[sheet].Rows[0].ItemArray);
            for (int row = 1; row < result.Tables[sheet].Rows.Count; row++)
            {
                List<string> rowList = result.Tables[sheet].Rows[row].ItemArray.Select(x => x.ToString()).ToList();
                if(isGroup)
                {
                    if (rowList[mMapping[eScenarioColumn.GROUP]].ToUpper() == eScenarioGroup.END.ToString())
                    {
                        isGroup = false;
                        if (!CreateCommand(newData, rowList))
                            Debug.Log("Parsing Error, Sheet : " + result.Tables[sheet].TableName + ", Row : " + row);

                        mLoader.ScenarioDatas.Add(newData);
                    }
                    else
                    {
                        if (!CreateCommand(newData, rowList))
                            Debug.Log("Parsing Error, Sheet : " + result.Tables[sheet].TableName + ", Row : " + row);
                    }
                }
                else
                {
                    //case 1 group start
                    if (rowList[mMapping[eScenarioColumn.GROUP]].ToUpper() == eScenarioGroup.START.ToString())
                    {
                        isGroup = true;
                        newData = new ScenarioData();
                        newData.Name = rowList[mMapping[eScenarioColumn.NAME]];
                        newData.Text = rowList[mMapping[eScenarioColumn.TEXT]];
                        if(!CreateCommand(newData, rowList))
                            Debug.Log("Parsing Error, Sheet : " + result.Tables[sheet].TableName + ", Row : " + row);
                    }
                    //case 2 : Not Group
                    else
                    {
                        newData = new ScenarioData();
                        newData.Name = rowList[mMapping[eScenarioColumn.NAME]];
                        newData.Text = rowList[mMapping[eScenarioColumn.TEXT]];
                        if (!CreateCommand(newData, rowList))
                            Debug.Log("Parsing Error, Sheet : " + result.Tables[sheet].TableName + ", Row : " + row);

                        mLoader.ScenarioDatas.Add(newData);
                    }
                }
            }
        }

        stream.Close();
    }

    private bool CreateCommand(ScenarioData newData, List<string> rowList)
    {
        bool isNormal = true;

        ScenarioCommand command = new ScenarioCommand();
        command.Target = Util.GetEnumByString<eScenarioTarget>(rowList[mMapping[eScenarioColumn.TARGET]]);
        command.Action = rowList[mMapping[eScenarioColumn.ACTION]];
        command.ID = rowList[mMapping[eScenarioColumn.ID]];
        command.Effect = rowList[mMapping[eScenarioColumn.EFFECT]];

        string posX = rowList[mMapping[eScenarioColumn.POSITIONX]];
        string posY = rowList[mMapping[eScenarioColumn.POSITIONY]];
        if (string.IsNullOrEmpty(posX) || string.IsNullOrEmpty(posY))
            command.UsePosition = false;
        else
        {
            command.UsePosition = true;
            float x = 0;
            float y = 0;
            bool canParseX = float.TryParse(posX, out x);
            bool canParseY = float.TryParse(posY, out y);

            if (!canParseX || !canParseY)
            {
                isNormal = false;
            }

            command.Position = new Vector2(x, y);
        }
        float delay = 0;
        float.TryParse(rowList[mMapping[eScenarioColumn.DELAY]], out delay);
        command.Delay = delay;
        if (newData.Commands == null)
            newData.Commands = new List<ScenarioCommand>();
        newData.Commands.Add(command);
        return isNormal;
    }

    private void MappingColumn(object[] items)
    {
        for(int i = 0; i < items.Length; i++)
        {
            eScenarioColumn column = Util.GetEnumByString<eScenarioColumn>(items[i].ToString().ToUpper());
            if (column == eScenarioColumn.NONE)
            {
                Debug.Log("Mapping Error at ScenarioLoaderInspector");
                continue;
            }

            mMapping.Add(column, i);
        }
    }
}