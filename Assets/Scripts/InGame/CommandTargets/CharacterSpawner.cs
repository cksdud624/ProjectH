using NUnit.Framework;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ICommandReceiver
{
    public void ExecuteCommand(ScenarioCommand command)
    {
        Debug.Log("캐릭터가 쑤욱");
    }
}
