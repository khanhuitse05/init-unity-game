using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWidthFitter : MonoBehaviour {

    public int spacing = 0;
    public float heightDefault = 0;
    public bool snapEveryFrame;
    private RectTransform thisRect;
    private void OnEnable()
    {
        if (!thisRect)
        {
            thisRect = gameObject.GetComponent<RectTransform>();
        }
        Snap();
    }
    int rate = 0;
    private void Update()
    {
        rate--;
        if (snapEveryFrame && rate < 0)
        {
            Snap();
            rate = 10;
        }
    }
    float _width;
    int _count;
    RectTransform _rect;
    Vector2 tempVect2;
    public void Snap()
    {
#if UNITY_EDITOR
        if (!thisRect)
        {
            thisRect = gameObject.GetComponent<RectTransform>();
        }
#endif
        _width = 0;
        _count = thisRect.childCount;
        for (int i = 0; i < _count; i++)
        {
            _rect = thisRect.GetChild(i) as RectTransform;
            if (_rect.gameObject.activeSelf)
            {
                _width += _rect.rect.width + spacing;
            }
        }
        _width -= spacing;
        if (heightDefault >= 0)
        {
            tempVect2.y = heightDefault;
        }
        else
        {
            tempVect2.y = thisRect.rect.height;
        }
        tempVect2.x = _width;
        thisRect.sizeDelta = tempVect2;
    }
}
