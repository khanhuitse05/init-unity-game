using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Ping
{
    public class Utils
    {
        /// FPS
        public const int _fps = 60;
        public static void SetFPS()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = _fps;
        }
        public static Sprite loadResourcesSprite(string param)
        {
            return Resources.Load<Sprite>("" + param);
        }

        public static List<T> Shuffle<T>(List<T> paramArray)
        {
            List<T> list = new List<T>(paramArray);
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
        public static T[] CloneArray<T>(T[] paramArray)
        {
            if (paramArray == null)
                return null;
            return paramArray.Clone() as T[];
        }
        public static List<T> CloneArray<T>(List<T> paramArray)
        {
            if (paramArray == null)
                return null;
            List<T> list = new List<T>(paramArray.ToArray());
            return list;
        }
        public static List<T> ConverArray<T>(T[] paramArray)
        {
            if (paramArray == null)
                return null;
            List<T> list = new List<T>();
            for (int i = 0; i < paramArray.Length; i++)
            {
                list.Add(paramArray[i]);
            }
            return list;
        }
        public static GameObject Spawn(GameObject paramPrefab, Transform paramParent = null)
        {
            GameObject newObject = UnityEngine.Object.Instantiate(paramPrefab) as GameObject;
            newObject.transform.SetParent(paramParent);
            newObject.transform.localPosition = Vector3.zero;
            newObject.transform.localScale = paramPrefab.transform.localScale;
            newObject.SetActive(true);
            return newObject;
        }
        public static void setActive(GameObject paramObject, bool paramValue)
        {
            if (paramObject != null)
                paramObject.SetActive(paramValue);
        }
        public static void removeAllChildren(Transform paramParent, bool paramInstant = true)
        {
            if (paramParent == null)
                return;
            for (int i = paramParent.childCount - 1; i >= 0; i--)
            {
                if (paramInstant)
                {
                    UnityEngine.Object.DestroyImmediate(paramParent.GetChild(i).gameObject);
                }
                else
                {
                    paramParent.GetChild(i).gameObject.SetActive(false);
                    UnityEngine.Object.Destroy(paramParent.GetChild(i).gameObject);
                }
            }
        }
        public static void Snap(RectTransform thisRect, RectTransform targetRect, float xPadding, float yPadding, bool sizeOnly = true)
        {
            if (!sizeOnly)
                thisRect.position = targetRect.position;

            Vector2 tempVect2;
            tempVect2.x = targetRect.rect.width + xPadding;
            tempVect2.y = targetRect.rect.height + yPadding;
            thisRect.sizeDelta = tempVect2;
        }
    }
}