using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private GameCenter mGameCenter;
    [SerializeField]
    private BackgroundBase mBackground;
    [SerializeField]
    private CharacterSpawner mCharacterSpawner;
    [SerializeField]
    private ImageSpawner mImageSpawner;
    [SerializeField]
    private TextBase mTextBase;

    private void Awake()
    {
        mGameCenter.MappingTarget(eScenarioTarget.BACKGROUND, mBackground);
        mGameCenter.MappingTarget(eScenarioTarget.CHARACTER, mCharacterSpawner);
        mGameCenter.MappingTarget(eScenarioTarget.IMAGE, mImageSpawner);
        mGameCenter.MappingTarget(eScenarioTarget.TEXT, mTextBase);
    }
}
