using UnityEngine;
[ExecuteInEditMode]
public class GameConstants : MonoBehaviour
{
    public const string gameVersion = "1.0.0";
    static GameConstants _instance;
    public static GameConstants Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
    }
    public void Init()
    {
    }
}
