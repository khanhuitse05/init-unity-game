/// <summary>
/// Turns the popup list it's attached to into a language selection list.
/// </summary>
using UnityEngine;

public class LanguageSelection : MonoBehaviour
{
    public GameObject pfItem;
    public RectTransform content;
    void Start()
    {
        GameObject _objSpawn;
        LanguageItem _item;
        int lenght = Localization.knownLanguages.Length;
        for (int i = 0; i < lenght; i++)
        {
            _objSpawn = Utils.Spawn(pfItem, content);
            _item = _objSpawn.GetComponent<LanguageItem>();
            _item.Init(Localization.knownLanguages[i], OnChange);
            if (Localization.language == Localization.knownLanguages[i])
            {
                _item.OnChoose();
            }
        }
        content.sizeDelta = new Vector2(content.sizeDelta.x, lenght * pfItem.GetComponent<RectTransform>().sizeDelta.y);
    }
    public void onShow()
    {
        gameObject.SetActive(true);
    }
    public void onHide()
    {
        gameObject.SetActive(false);
    }
    public void OnChange(string _value)
    {
        Localization.language = LanguageItem.current.id;
        onHide();
    }
}
