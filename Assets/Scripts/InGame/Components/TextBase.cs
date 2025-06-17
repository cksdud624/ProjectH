using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBase : MonoBehaviour, ICenterComponent, ICommandReceiver
{
    //ÄÄÆ÷³ÍÆ®
    protected Image mImage;
    protected TextMeshProUGUI mText;
    protected Image mNextTextSignal;

    //µ¥ÀÌÅÍ
    protected GameState mGameState;
    protected OptionData mOptionData;

    protected string mTargetText = string.Empty;

    protected void Awake()
    {
        mImage = GetComponent<Image>();
        mText = GetComponentInChildren<TextMeshProUGUI>();
        mNextTextSignal = GetComponentInChildren<Image>();

        mOptionData = OptionManager.Instance.OptionData;
    }

    public void PlayText(string text)
    {
        mTargetText = text;
        StartCoroutine(ProcessText());
    }

    public void SetGameState(GameState gameState)
    {
        mGameState = gameState;
    }

    IEnumerator ProcessText()
    {
        mText.text = string.Empty;

#if UNITY_EDITOR
        if (mOptionData == null)
            Debug.Log("Option Data is Null");
#endif

        foreach (char ch in mTargetText)
        {
            mText.text += ch;
            yield return new WaitForSeconds(mOptionData.TextSpeed);
        }
        mGameState.IsTextPlaying = false;
    }

    public void ExecuteCommand(ScenarioCommand command)
    {
        Debug.Log("ÅØ½ºÆ®µµ ½î¿Á");
    }
}
