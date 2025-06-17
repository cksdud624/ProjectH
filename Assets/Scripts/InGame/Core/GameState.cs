using System;

public class GameState
{
    private bool isTextPlaying = false;
    public bool IsTextPlaying
    { 
        get { return isTextPlaying; } 
        set { isTextPlaying = value; }
    }
}
