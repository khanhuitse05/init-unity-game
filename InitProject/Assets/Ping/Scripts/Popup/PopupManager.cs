using UnityEngine;
using System;
using UnityEngine.UI;
namespace Ping
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager Instance {  get; private set; }
        void Awake()
        {
            Instance = this;
#if DEV
            if(objDev != null) objDev.SetActive(true);
#endif
            DontDestroyOnLoad(gameObject);
        }

#region Popup
        [SerializeField] private Transform root;
        [SerializeField] private GameObject objDev;
        // ShowInfoPopUp
        public static void ShowInfoPopUp(string title, string message, Action actionOk, string ok = "OK")
        {
            Instance._ShowInfoPopUp(title, message, actionOk, ok);
        }
        void _ShowInfoPopUp(string title, string message, Action actionOk, string ok = "OK")
        {
            GameObject popup = SpawnPopup("popupOk");
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
            GameObject popup = SpawnPopup("popupYesNo");
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
            GameObject popup = SpawnPopup("popupMessage");
            MessageComponent script = popup.GetComponent<MessageComponent>();
            script.Init(message);            
        }
        
        GameObject SpawnPopup(string param)
        {
            GameObject prefab = Resources.Load<GameObject>("Popups/" + param);
            GameObject popup = Instantiate(prefab) as GameObject;
            popup.SetActive(true);
            popup.transform.SetParent(root);
            popup.transform.localPosition = Vector3.zero;
            popup.transform.localScale = Vector3.one;
            return popup;
        }

        public static int GetPopupCount()
        {
            return Instance.root.childCount;
        }
#endregion

#region LoadScene
        public PopupLoadScene loadscene;
        public static void InitLoadScene(string nextScene)
        {
            Instance._InitLoadScene(nextScene);
        }
        void _InitLoadScene(string nextScene)
        {
            loadscene.Init(nextScene);
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
            if (Input.GetKeyDown(KeyCode.F2)) PopupManager.ShowInfoPopUp("Title", "Cheat Show Popup", null);
            if (Input.GetKeyDown(KeyCode.F3)) PopupManager.ShowYesNoPopUp("Title\nwith tow row", "Cheat Show Popup" + "\n\n<color=#79BE29FF>https://bigc.vn</color>", null, null);
            if (Input.GetKeyDown(KeyCode.F4))
            {
                if (LoadingUI.activeSelf == false) PopupManager.ShowLoading("Cheat Loading...");
                else PopupManager.HideLoading();
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                int _i = UnityEngine.Random.Range(0, 10);
                string _mes = "message";
                for (int i = 0; i < _i; i++)
                {
                    _mes += " " + "Date Time Now: " + DateTime.Now.ToUniversalTime();
                }
                PopupManager.ShowMessage(_mes);
            }
        }
#endif

    }
}