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
                //추후 옵션을 외부 형식으로 저정 할 때 파싱하는 함수 필요
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
