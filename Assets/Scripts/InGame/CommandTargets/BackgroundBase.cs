using System.Collections.Generic;
using UnityEngine;
using System;

public class BackgroundBase : MonoBehaviour, ICommandReceiver
{
    private Dictionary<KeyValuePair<eBackgroundAction, eBackgroundEffect>, Action<ScenarioCommand>> mActionMapping = new();

    private void Awake()
    {
        mActionMapping.Clear();
        mActionMapping[CreateKeyValue(eBackgroundAction.CHANGE, eBackgroundEffect.IMMEDIATE)] = CommandChangeImmediate;
    }

    public void ExecuteCommand(ScenarioCommand command)
    {
        eBackgroundAction action = Util.GetEnumByString<eBackgroundAction>(command.Action);
        eBackgroundEffect effect = Util.GetEnumByString<eBackgroundEffect>(command.Effect);
        var key = CreateKeyValue(action, effect);
        if (mActionMapping.ContainsKey(key))
            mActionMapping[CreateKeyValue(action, effect)]?.Invoke(command);
        else
            Debug.Log("Invalid Command : BackgroundBase");
    }

    private KeyValuePair<eBackgroundAction, eBackgroundEffect> CreateKeyValue(eBackgroundAction action, eBackgroundEffect effect)
       => new KeyValuePair<eBackgroundAction, eBackgroundEffect>(action, effect);

    private void CommandChangeImmediate(ScenarioCommand command)
    {
        Debug.Log("¸®¼±Á· Ã´°á");
    }
}
