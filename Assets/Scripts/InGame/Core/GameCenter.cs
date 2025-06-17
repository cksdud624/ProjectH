using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameCenter : MonoBehaviour
{
    //Components
    [SerializeField]
    protected GameObject mButtonGroup;
    [SerializeField]
    protected TextBase mText;
    [SerializeField]
    protected NameBase mName;
    [SerializeField]
    protected InputBase mInput;


    protected Dictionary<eButtonType, ButtonBase> mButtons = new Dictionary<eButtonType, ButtonBase>();
    protected Dictionary<eScenarioTarget, ICommandReceiver> mTargets = new Dictionary<eScenarioTarget, ICommandReceiver>();

    //GameState
    protected GameState mGameState;

    //Scenario
    [SerializeField]
    protected ScenarioLoader mScenarioLoader;
    protected int mDataIndex = -1;

    void Awake()
    {
        mGameState = new GameState();

        mText.SetGameState(mGameState);
        mName.SetGameState(mGameState);
        mInput.SetGameState(mGameState);

        MappingButtons();
        Init();
    }

    private void Start()
    {
        PlayNext();
    }

    private void Init()
    {
        Bind();
    }

    private void Bind()
    {
        mInput.OnInput += PlayInput;
    }

    private void UnBind()
    {
        mInput.OnInput -= PlayInput;
    }

    void MappingButtons()
    {
        if (mButtonGroup == null)
            Debug.Log("Binding Error");

        foreach(ButtonBase button in mButtonGroup.GetComponentsInChildren<ButtonBase>())
        {
            mButtons.Add(button.Type, button);
        }
    }

    public void MappingTarget(eScenarioTarget target, ICommandReceiver receiver)
    {
        mTargets[target] = receiver;
    }

    protected void PlayInput(eInputType inputType)
    {
        if (inputType == eInputType.Play)
            PlayNext();
    }

    public void PlayNext()
    {
        if(mGameState.IsTextPlaying)
        {
            Debug.Log("텍스트 플레이중");
            return;
        }

        mGameState.IsTextPlaying = true;

        mDataIndex++;
        if(mDataIndex >= mScenarioLoader.ScenarioDatas.Count)
        {
            //시나리오 종료
            Debug.Log("Scenario End");
            return;
        }

        ScenarioData data = mScenarioLoader.ScenarioDatas[mDataIndex];
        //1. 텍스트 출력
        mText.PlayText(data.Text);
        mName.PlayName(data.Name);
        //2. 커맨드 출력
        List<ScenarioCommand> commands = data.Commands;
        foreach(ScenarioCommand command in commands)
        {
            if(mTargets.ContainsKey(command.Target))
            {
                mTargets[command.Target].ExecuteCommand(command);
            }
        }
    }
}
