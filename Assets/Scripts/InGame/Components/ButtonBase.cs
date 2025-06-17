using UnityEngine;

public class ButtonBase : MonoBehaviour
{
    [SerializeField]
    protected eButtonType type;
    public eButtonType Type { get { return type; } }
}
