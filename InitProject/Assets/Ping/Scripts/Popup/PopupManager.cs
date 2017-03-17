using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    static PopupManager _instance;
    public static PopupManager Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Transform popUpRoot;
    public GameObject YesNoPopUpPrefab;
    public GameObject InfoPopUpPrefab;
    public GameObject MesagePopUpPrefab;
    public GameObject LoadingUI;
    public Text txtLoading;
    List<MessagePopupComponent> listMessage = new List<MessagePopupComponent>();

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.B))
        {
            PopupManager.Instance.InitInfoPopUp("Cheat Show Popup", null);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            PopupManager.Instance.InitYesNoPopUp("Cheat Show Popup", null, null);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PopupManager.Instance.InitMesage("Date Time Now: " + DateTime.Now.ToUniversalTime());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (LoadingUI.activeSelf == false)
            {
                PopupManager.Instance.ShowLoading("Cheat Loading...");
            }
            else
            {
                PopupManager.Instance.HideLoading();
            }
        }
#endif
    }
    public void InitYesNoPopUp(string message, Action yes, Action no, string _yes = "YES", string _no = "NO")
    {
        GameObject popup = null;
        popup = GameObject.Instantiate(YesNoPopUpPrefab) as GameObject;
        popup.SetActive(true);
        popup.transform.SetParent(popUpRoot.transform);
        popup.transform.localPosition = Vector3.zero;
        popup.transform.localScale = Vector3.one;
        YesNoPopUpComponent script = popup.GetComponent<YesNoPopUpComponent>();
        script.Init(message, yes, no, _yes, _no);
    }

    public void InitInfoPopUp(string message, Action ok, string _ok = "OK")
    {
        GameObject popup = null;
        popup = GameObject.Instantiate(InfoPopUpPrefab) as GameObject;
        popup.SetActive(true);
        popup.transform.SetParent(popUpRoot.transform);
        popup.transform.localPosition = Vector3.zero;
        popup.transform.localScale = Vector3.one;
        InfoPopUpComponent script = popup.GetComponent<InfoPopUpComponent>();
        script.Init(message, ok, _ok);
    }
    public void InitMesage(string message)
    {
        GameObject popup = null;
        popup = GameObject.Instantiate(MesagePopUpPrefab) as GameObject;
        popup.SetActive(true);
        popup.transform.SetParent(popUpRoot.transform);
        popup.transform.localPosition = Vector3.zero;
        popup.transform.localScale = Vector3.one;
        MessagePopupComponent script = popup.GetComponent<MessagePopupComponent>();
        script.Init(message);
        for (int i = 0; i < listMessage.Count; i++)
        {
            if (listMessage[i] != null)
            {
                listMessage[i].OnMoveUp();
            }
        }
        listMessage.Add(script);
    }
    public void OnDestroyMessagePopup(MessagePopupComponent _item)
    {
        listMessage.Remove(_item);
    }
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
}