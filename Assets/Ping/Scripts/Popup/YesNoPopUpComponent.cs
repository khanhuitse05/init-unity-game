using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class YesNoPopUpComponent : MonoBehaviour
{
    Action actionYes;
    Action actionNo;
    string message;
    string txtNo;
    string txtYes;
    public Animator animator;
    public Text messageLbl;
    public Text noLbl;
    public Text yesLbl;

    public void Init(string message, Action yes, Action no, string _yes = "YES", string _no = "NO")
    {
        this.message = message;
        this.txtNo = _no;
        this.txtYes = _yes;
        actionYes = yes;
        actionNo = no;
        messageLbl.text = this.message;
        yesLbl.text = this.txtYes;
        noLbl.text = this.txtNo;
        messageLbl.text = this.message;
        StartCoroutine(ShowPopUp());
    }
    public void OnYesBtnClicked()
    {
        StartCoroutine(ClosePopUp(true));
    }
    public void OnNoBtnClicked()
    {
        StartCoroutine(ClosePopUp(false));
    }
    IEnumerator ShowPopUp()
    {
        if (animator != null)
        {
            animator.SetTrigger("Show");
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator ClosePopUp(bool yes)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hide");
        }
        yield return new WaitForSeconds(0.5f);

        if (yes && actionYes != null)
            actionYes();
        else if (!yes && actionNo != null)
            actionNo();
        Destroy(gameObject);
    }
}