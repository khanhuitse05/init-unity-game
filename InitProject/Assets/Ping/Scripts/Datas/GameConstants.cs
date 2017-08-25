using UnityEngine;
namespace Ping
{
    [ExecuteInEditMode]
    public class GameConstants : MonoBehaviour
    {
        public static GameConstants Instance { get; private set; }
        void Awake() { Instance = this; }
        public void Init()
        {
        }

        public const string gameVersion = "1.0.0";
    }
}