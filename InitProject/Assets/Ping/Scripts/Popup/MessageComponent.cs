using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageComponent : MonoBehaviour
{
    string message;
    public Text messageLbl;
    public Text guiLbl;
    public Animator animator;
    public float timeAlive;
    public float Init(string message)
    {
        this.message = message;
        guiLbl.text = this.message;
        messageLbl.text = this.message;
        Canvas.ForceUpdateCanvases();
        StartCoroutine(ShowMessage());
        return guiLbl.rectTransform.sizeDelta.y;
    }

    IEnumerator ShowMessage()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hide");
        }
        yield return new WaitForSeconds(timeAlive);
        PopupManager.Instance.OnDestroyMessagePopup(this);
        Destroy(gameObject);
    }
    public void OnMoveUp(float _size)
    {
        Vector3 _pos = guiLbl.rectTransform.localPosition;
        guiLbl.rectTransform.localPosition = new Vector3(_pos.x, _pos.y + _size + 50, _pos.z);
    }
}