using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessagePopupComponent : MonoBehaviour
{
    string message;
    public Text messageLbl;
    public Animator animator;
    public float timeAlive;
    public void Init(string message)
    {
        this.message = message;
        messageLbl.text = this.message;
        StartCoroutine(ShowPopUp());
    }

    IEnumerator ShowPopUp()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hide");
        }
        yield return new WaitForSeconds(timeAlive);
        Destroy(gameObject);
    }
}