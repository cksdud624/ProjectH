using UnityEngine;

public interface ICenterComponent
{
    public void SetGameState(GameState gameState);
}

public interface ICommandReceiver
{
    public void ExecuteCommand(ScenarioCommand command);
}