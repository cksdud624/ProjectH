using UnityEngine;

public class ImageSpawner : MonoBehaviour, ICommandReceiver
{
    public void ExecuteCommand(ScenarioCommand command)
    {
        Debug.Log("�̹����� ���");
    }
}