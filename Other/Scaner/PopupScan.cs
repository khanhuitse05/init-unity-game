using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

namespace Ping9
{
    public class PopupScan : MonoBehaviour
    {
        public static PopupScan Instance { get; private set; }
        void Awake()
        {
            Instance = this;
            guiMain.SetActive(false);
        }

        public GameObject guiMain;
        public Text title;
        public Text body;
        public RawImage rendererCamera;
        WebCamTexture webCamTexture;
        AspectRatioFitter fit;
        bool isScan = false;
        public void Init(Action<string> _finish)
        {
            actionFinish = _finish;
            if (fit == null)
            {
                fit = rendererCamera.gameObject.GetComponent<AspectRatioFitter>();
            }
#if UNITY_ANDROID
            StartCoroutine(RequestCamera());
#else
            webCamTexture = new WebCamTexture();
            rendererCamera.texture = webCamTexture;
            StartScan();
#endif
        }

        IEnumerator RequestCamera()
        {
            AndroidRuntimePermissions.Permission resultPermission = AndroidRuntimePermissions.RequestPermission("android.permission.CAMERA");
            if (resultPermission == AndroidRuntimePermissions.Permission.Granted)
            {
                webCamTexture = new WebCamTexture();
                rendererCamera.texture = webCamTexture;
                StartScan();
            }
            else
                PopupManager.ShowPopupCameraAuthorizationFaill(OnExit);

            yield return new WaitForSeconds(0.5f);
            //if (webCamTexture.GetPixels().Length < 1000)
            //{
            //    PopupManager.ShowPopupCameraAuthorizationFaill(OnExit);
            //}
        }
        public void StartScan()
        {
            guiMain.SetActive(true);
            webCamTexture.Play();
            isScan = true;
        }
        void StopScan()
        {
            webCamTexture.Stop();
            isScan = false;
        }
        void Update()
        {
            if (isScan)
            {
                var _result = Scanner.Decode(webCamTexture);
                if (_result != null)
                {
                    ShowResult(_result);
                }
                // Fix camera
                if (webCamTexture != null && webCamTexture.isPlaying)
                {
                    float ratio = (float)webCamTexture.width / (float)webCamTexture.height;
                    fit.aspectRatio = ratio; // Set the aspect ratio
                    float scaleY = webCamTexture.videoVerticallyMirrored ? -1f : 1f; // Find if the camera is mirrored or not
                    rendererCamera.rectTransform.localScale = new Vector3(1f, scaleY, 1f); // Swap the mirrored camera
                    int orient = -webCamTexture.videoRotationAngle;
                    rendererCamera.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
                }
            }
            
        }
        void ShowResult(Result _result)
        {
            result = _result;
            StopScan();
            if (actionFinish == null)
            {
                if (Utils.IsUrl(result.Text))
                {
                    string _title = Localization.Get("H_QR_Title").ToUpper();
                    string _message = Localization.Get("H_QR_GoToUrl") + "\n\n<color=#79BE29FF>" + result.Text + "</color>";
                    string _accept = Localization.Get("C_Accept");
                    string _decline = Localization.Get("C_Decline");
                    PopupManager.ShowYesNoPopUp(_title, _message, Accept, OnExit, _accept, _decline, OnExit);
                }
                else
                {
                    string _title = Localization.Get("H_QR_Title").ToUpper();
                    string _finish = Localization.Get("C_Finish");
                    PopupManager.ShowInfoPopUp(_title, result.Text, OnExit, _finish, OnExit);
                }
            }
            else
            {
                string _title = Localization.Get("H_QR_Title").ToUpper();
                string _finish = Localization.Get("C_Finish");
                string _again = Localization.Get("C_TryAgain");
                PopupManager.ShowYesNoPopUp(_title, result.Text, OnFinish, StartScan, _finish, _again, StartScan);
            }
        }
        Action<string> actionFinish;
        Result result;
        void OnFinish()
        {
            if (actionFinish != null)
            {
                actionFinish(result.Text);
            }
            else
            {

            }
            OnExit();
        }
        void Accept()
        {
            StopScan();
            guiMain.SetActive(false);
            Application.OpenURL(result.Text);
        }
        public void OnExit()
        {
            StopScan();
            guiMain.SetActive(false);
            webCamTexture = null;
        }
    }
}