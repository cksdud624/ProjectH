using NUnit.Framework;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour, ICommandReceiver
{
    public void ExecuteCommand(ScenarioCommand command)
    {
        Debug.Log("ĳ���Ͱ� ����");
    }
}
