using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameBase : MonoBehaviour, ICenterComponent
{
    protected Image mImage;
    protected TextMeshProUGUI mName;
    GameState mGameState;

    protected void Awake()
    {
        mImage = GetComponent<Image>();
        mName = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetGameState(GameState gameState)
    {
        mGameState = gameState;
    }

    public void PlayName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;

        mName.text = name;
    }
}
