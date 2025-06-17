using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBase : MonoBehaviour, ICenterComponent
{
    GameState mGameState;
    protected PlayerInput mInput;

    public Action<eInputType> OnInput;

    protected void Awake()
    {
        mInput = GetComponent<PlayerInput>();
        Bind();
    }

    protected void Bind()
    {
        mInput.actions[eInputType.Play.ToString()].performed += InputPlay;
    }

    protected void UnBind()
    {
        mInput.actions[eInputType.Play.ToString()].performed -= InputPlay;
    }
    
    protected void InputPlay(InputAction.CallbackContext context) 
    {
        OnInput.Invoke(eInputType.Play);
    }

    public void SetGameState(GameState gameState)
    {
        mGameState = gameState;
    }
}
