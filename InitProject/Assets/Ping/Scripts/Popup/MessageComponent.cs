using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Ping
{
    public class MessageComponent : MonoBehaviour
    {
        string message;
        public Text messageLbl;
        public RectTransform rect;
        public Animator animator;
        public float timeAlive;
        public float Init(string message)
        {
            this.message = message;
            messageLbl.text = this.message;
            Canvas.ForceUpdateCanvases();
            StartCoroutine(ShowMessage());
            RectTransformSnap rectSnap = rect.GetComponent<RectTransformSnap>();
            rectSnap.Snap();
            return rect.sizeDelta.y;
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
            Vector3 _pos = rect.localPosition;
            rect.localPosition = new Vector3(_pos.x, _pos.y + _size + 50, _pos.z);
        }
    }
}