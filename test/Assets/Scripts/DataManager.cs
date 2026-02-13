using UnityEngine;

public class DataManager : MonoBehaviour, IManage
{
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _state = "Data Manager initialized..";
        Debug.Log(_state);
    }
}
