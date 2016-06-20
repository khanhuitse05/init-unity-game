using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InfoPopUpComponent : MonoBehaviour
{
    Action actionOK;
    string message;
    string txtOk;
    public Animator animator;
    public Text messageLbl;
    public Text okLbl;

    public void Init(string message, Action ok, string _ok = "OK")
    {
        this.message = message;
        this.txtOk = _ok;
        actionOK = ok;

        messageLbl.text = this.message;
        okLbl.text = this.txtOk;
        StartCoroutine(ShowPopUp());
    }
    public void OnYesBtnClicked()
    {
        StartCoroutine(ClosePopUp());
    }
    IEnumerator ShowPopUp()
    {
        if (animator != null)
        {
            animator.SetTrigger("Show");
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator ClosePopUp()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hide");
        }
        yield return new WaitForSeconds(0.5f);
        if (actionOK != null)
            actionOK();
        Destroy(gameObject);
    }
}