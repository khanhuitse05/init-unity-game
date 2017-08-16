using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #region Popup
    public Transform root;
    public GameObject prefabConfirmPopup;
    public GameObject PrefabInfoPopup;
    public GameObject prefabMesage;
    List<MessageComponent> listMessage = new List<MessageComponent>();
    
    public void InitInfoPopUp(string message, Action ok, string _ok = "OK")
    {
        GameObject popup = SpawnPopup(PrefabInfoPopup);
        PopupInfoComponent script = popup.GetComponent<PopupInfoComponent>();
        script.Init(message, ok, _ok);
    }
    public void InitYesNoPopUp(string message, Action yes, Action no, string _yes = "YES", string _no = "NO")
    {
        GameObject popup = SpawnPopup(prefabConfirmPopup);
        PopupConfirmComponent script = popup.GetComponent<PopupConfirmComponent>();
        script.Init(message, yes, no, _yes, _no);
    }
    public void InitMessage(string message)
    {
        GameObject popup = SpawnPopup(prefabMesage);
        MessageComponent script = popup.GetComponent<MessageComponent>();
        float _size = script.Init(message);
        for (int i = 0; i < listMessage.Count; i++)
        {
            if (listMessage[i] != null)
            {
                listMessage[i].OnMoveUp(_size);
            }
        }
        listMessage.Add(script);
    }
    public void OnDestroyMessagePopup(MessageComponent _item)
    {
        listMessage.Remove(_item);
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
    public void SetTextLoading(string _value)
    {
        txtLoading.text = _value;
    }
    public void ShowLoading(string _txtLoading = "Loading...",bool disableBackKey = true)
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
    public void HideLoading()
    {
        if (revertBackKeyState)
        {
            revertBackKeyState = false;
            GameStatesManager.enableBackKey = oldBackKeyState;
        }
        LoadingUI.SetActive(false);
    }
#endregion


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F5)) PopupManager.Instance.InitInfoPopUp("Cheat Show Popup", null);
        if (Input.GetKeyDown(KeyCode.F6)) PopupManager.Instance.InitYesNoPopUp("Cheat Show Popup", null, null);
        if (Input.GetKeyDown(KeyCode.F7))
        {
            if (LoadingUI.activeSelf == false) PopupManager.Instance.ShowLoading("Cheat Loading...");
            else PopupManager.Instance.HideLoading();
        }
        if (Input.GetKeyDown(KeyCode.F8)) PopupManager.Instance.InitMessage("Date Time Now: " + DateTime.Now.ToUniversalTime());
#endif
    }
    
}