using UnityEngine;

public class OptionManager : MonoBehaviour
{
    private static OptionManager instance = null;
    public static OptionManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singleton = new GameObject("Option Manager");
                instance = singleton.AddComponent<OptionManager>();
                DontDestroyOnLoad(singleton);
            }
            return instance;
        }
    }

    private OptionData optionData;
    public OptionData OptionData
    {
        get 
        {
            if (optionData == null)
            {
                //���� �ɼ��� �ܺ� �������� ���� �� �� �Ľ��ϴ� �Լ� �ʿ�
                optionData = new OptionData();
                LoadOptionData();
            }
            return optionData; 
        }
    }

    private void LoadOptionData()
    {
        optionData.TextSpeed = 0.001f;
    }
}
