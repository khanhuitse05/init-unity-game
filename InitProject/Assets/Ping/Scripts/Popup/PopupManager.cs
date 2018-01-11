using UnityEngine;
using System;
using UnityEngine.UI;
namespace Ping
{
    public class PopupManager : MonoBehaviour
    {
        private static PopupManager Instance { get; set; }
        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #region Popup
        [SerializeField] private Transform root;
        [SerializeField] private GameObject prefabConfirmPopup;
        [SerializeField] private GameObject PrefabInfoPopup;
        [SerializeField] private GameObject prefabMesage;
        // ShowInfoPopUp
        public static void ShowInfoPopUp(string title, string message, Action actionOk, string ok = "OK")
        {
            Instance._ShowInfoPopUp(title, message, actionOk, ok);
        }
        void _ShowInfoPopUp(string title, string message, Action actionOk, string ok = "OK")
        {
            GameObject popup = SpawnPopup(PrefabInfoPopup);
            PopupInfoComponent script = popup.GetComponent<PopupInfoComponent>();
            script.Init(title, message, actionOk, ok);
        }
        // ShowYesNoPopUp
        public static void ShowYesNoPopUp(string title, string message, Action actionYes, Action actionNo, string yes = "YES", string no = "NO")
        {
            Instance._ShowYesNoPopUp(title, message, actionYes, actionNo, yes, no);
        }
        void _ShowYesNoPopUp(string title, string message, Action actionYes, Action actionNo, string yes = "YES", string no = "NO")
        {
            GameObject popup = SpawnPopup(prefabConfirmPopup);
            PopupConfirmComponent script = popup.GetComponent<PopupConfirmComponent>();
            script.Init(title, message, actionYes, actionNo, yes, no);
        }
        // ShowMessage
        public static void ShowMessage(string message)
        {
            Instance._ShowMessage(message);
        }
        void _ShowMessage(string message)
        {
            GameObject popup = SpawnPopup(prefabMesage);
            MessageComponent script = popup.GetComponent<MessageComponent>();
            script.Init(message);            
        }
        
        GameObject SpawnPopup(GameObject prefab)
        {
            GameObject popup = GameObject.Instantiate(prefab) as GameObject;
            popup.SetActive(true);
            popup.transform.SetParent(root);
            popup.transform.localPosition = Vector3.zero;
            popup.transform.localScale = Vector3.one;
            return popup;
        }
        #endregion

        #region Loading
        public GameObject LoadingUI;
        public Text txtLoading;
        bool oldBackKeyState = false;
        bool revertBackKeyState = false;
        public static void SetTextLoading(string _value)
        {
            Instance.txtLoading.text = _value;
        }
        public static void ShowLoading(string _txtLoading = "Loading...", bool disableBackKey = true)
        {
            Instance._ShowLoading(_txtLoading, disableBackKey);
        }
        void _ShowLoading(string _txtLoading = "Loading...", bool disableBackKey = true)
        {
            if (disableBackKey && !revertBackKeyState)
            {
                revertBackKeyState = true;
                oldBackKeyState = GameStatesManager.enableBackKey;
                GameStatesManager.enableBackKey = false;
            }
            txtLoading.text = _txtLoading;
            LoadingUI.SetActive(true);
        }
        public static void HideLoading()
        {
            Instance._HideLoading();
        }
        void _HideLoading()
        {
            if (revertBackKeyState)
            {
                revertBackKeyState = false;
                GameStatesManager.enableBackKey = oldBackKeyState;
            }
            LoadingUI.SetActive(false);
        }
        #endregion


#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5)) PopupManager.ShowInfoPopUp("Title", "Cheat Show Popup", null);
            if (Input.GetKeyDown(KeyCode.F6)) PopupManager.ShowYesNoPopUp("Title", "Cheat Show Popup", null, null);
            if (Input.GetKeyDown(KeyCode.F7))
            {
                if (LoadingUI.activeSelf == false) PopupManager.ShowLoading("Cheat Loading...");
                else PopupManager.HideLoading();
            }
            if (Input.GetKeyDown(KeyCode.F8)) PopupManager.ShowMessage("Date Time Now: " + DateTime.Now.ToUniversalTime());
        }
#endif

    }
}