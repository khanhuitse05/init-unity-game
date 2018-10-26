using UnityEngine;
using UnityEngine.UI;

namespace Ping
{
    public class RemoteImageBehaviour : MonoBehaviour
    {
        [Tooltip("If not assigned, will try to find it in this game object")]
        [SerializeField]
        public RawImage _RawImage;
        public Image imgResult;


        bool _DestroyPending;
        public void Load(string imageURL)
        {
            var request = new SimpleImageDownloader.Request()
            {
                url = imageURL,
                onDone = result =>
                {
                    if (!_DestroyPending)
                    {
                        Texture2D texToUse = result.CreateTextureFromReceivedData();
                        texToUse.filterMode = FilterMode.Trilinear;
                        texToUse.anisoLevel = 0;
                        ShowImage(texToUse);
                    }
                },
                onError = () =>
                {
                    Debug.Log("Load image error: " + imageURL);
                }
            };
            SimpleImageDownloader.Instance.Enqueue(request);
        }

        void ShowImage(Texture2D texToUse)
        {
            if (_RawImage)
            {
                _RawImage.texture = texToUse;
            }
            else
            {
                imgResult.sprite = TextureToSprite(texToUse);
            }

        }

        public Sprite TextureToSprite(Texture2D texture)
        {
            return  Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        void OnDestroy()
        {
            _DestroyPending = true;
        }
    }

}