using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeightFitter : MonoBehaviour {

    public int spacing = 0;
    public float widthDefault = 0;
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
    float _height;
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
        _height = 0;
        _count = thisRect.childCount;
        for (int i = 0; i < _count; i++)
        {
            _rect = thisRect.GetChild(i) as RectTransform;
            if (_rect.gameObject.activeSelf)
            {
                _height += _rect.rect.height + spacing;
            }
        }
        _height -= spacing;
        if (widthDefault >= 0)
        {
            tempVect2.x = widthDefault;
        }
        else
        {
            tempVect2.x = thisRect.rect.width;
        }
        tempVect2.y = _height;
        thisRect.sizeDelta = tempVect2;
    }
}
