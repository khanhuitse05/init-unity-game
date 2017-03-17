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
        currentPos = transform.position;
        StartCoroutine(ShowPopUp());
    }

    IEnumerator ShowPopUp()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hide");
        }
        yield return new WaitForSeconds(timeAlive);
        PopupManager.Instance.OnDestroyMessagePopup(this);
        Destroy(gameObject);
    }
    public void OnMoveUp()
    {
        currentPos.y += 2.0f;
        iTween.MoveTo(gameObject, currentPos, 0.1f);
    }
    Vector3 currentPos;
}