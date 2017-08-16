using UnityEngine;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
    }
}
