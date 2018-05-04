using System;
using UnityEngine;
using System.Collections;

public enum SimpleGridAnchor
{
    BottomLeft,
    Center
}

/// <summary>
///     Uncomplete class
/// </summary>
public class SimpleGrid : MonoBehaviour
{
    public SimpleGridAnchor Anchor;
    public Vector2 CellSize;

    public void Reposition()
    {
        switch (Anchor)
        {
            case SimpleGridAnchor.BottomLeft:
                RepositionBottomLeft();
                break;

            case SimpleGridAnchor.Center:
                RepositionCenter();
                break;
        }
    }

    private void RepositionBottomLeft()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 localPosition = Vector3.zero;
            localPosition.x = i * CellSize.x;
            localPosition.y = i * CellSize.y;
            child.localPosition = localPosition;
        }
    }

    private void RepositionCenter()
    {
        Vector3 baseLocalPos = Vector3.zero;

        int count = transform.childCount;
        if (count % 2 == 0)
        {
            baseLocalPos = -CellSize * (count / 2f - 0.5f);
        }
        else
        {
            baseLocalPos = -CellSize * (count - 1f) / 2f;
        }

        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 localPosition = baseLocalPos;
            localPosition.x += i * CellSize.x;
            localPosition.y += i * CellSize.y;
            child.localPosition = localPosition;
        }
    }

    public void Rename()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.name = string.Format("{0}", i);
        }
    }
}