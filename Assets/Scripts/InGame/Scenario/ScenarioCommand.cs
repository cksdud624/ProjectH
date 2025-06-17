using UnityEngine;
using System;

[Serializable]
public class ScenarioCommand
{
    [SerializeField]
    private eScenarioTarget target;
    public eScenarioTarget Target { get { return target; }  set { target = value; } }

    [SerializeField]
    private string id;
    public string ID { get { return id; } set {  id = value; } }

    //���Ŀ� �ٸ� �ڷ��� ���
    [SerializeField]
    private string action;
    public string Action { get { return action; } set { action = value; } }

    [SerializeField]
    private string effect;
    public string Effect { get { return effect; } set{ effect = value;  } }

    [SerializeField]
    private bool usePosition = false;
    public bool UsePosition { get {  return usePosition; } set {  usePosition = value; } }

    [SerializeField]
    private Vector2 position;
    public Vector2 Position { get { return position; } set {  position = value; } }

    [SerializeField]
    private float delay;
    public float Delay { get { return delay; } set { delay = value; } }
}