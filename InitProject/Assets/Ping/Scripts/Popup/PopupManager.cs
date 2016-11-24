using UnityEngine;
using System;

public class PopupManager : MonoBehaviour
{
    static PopupManager _instance;
    public static PopupManager Instance { get { return _instance; } }
    void Awake(){ _instance = this;}

    public Transform popUpRoot;
    public GameObject YesNoPopUpPrefab;
    public GameObject InfoPopUpPrefab;
    public GameObject MesagePopUpPrefab;
    public GameObject LoadingUI;

    public void InitYesNoPopUp(string message, Action yes, Action no, string _yes = "YES", string _no = "NO")
    {
        GameObject popup = null;
        popup = GameObject.Instantiate(YesNoPopUpPrefab) as GameObject;
        popup.SetActive(true);
        popup.transform.parent = popUpRoot.transform;
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
        popup.transform.parent = popUpRoot.transform;
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
        popup.transform.parent = popUpRoot.transform;
        popup.transform.localPosition = Vector3.zero;
        popup.transform.localScale = Vector3.one;
        MessagePopupComponent script = popup.GetComponent<MessagePopupComponent>();
        script.Init(message);
    }

    bool oldBackKeyState = false;
    bool revertBackKeyState = false;
    public void ShowLoading(bool disableBackKey = true)
    {
        if (disableBackKey && !revertBackKeyState)
        {
            revertBackKeyState = true;
            oldBackKeyState = GameStatesManager.enableBackKey;
            GameStatesManager.enableBackKey = false;
        }
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