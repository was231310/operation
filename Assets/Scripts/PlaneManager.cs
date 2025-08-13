using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneManager : MonoBehaviour
{
    public string DeviceName;
    //public Vector2 CameraSize;
    public float CameraFPS;

    //���շ��ص�ͼƬ����  
    WebCamTexture _webCamera;
    //public GameObject Plane;//��Ϊ��ʾ����ͷ�����
    public RawImage rawImage;


    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), "Initialize Camera"))
        {
            StartCoroutine("InitCameraCor");
        }

        //���һ����ť������������Ŀ��͹�
        if (GUI.Button(new Rect(100, 250, 100, 100), "ON/OFF"))
        {
            if (_webCamera != null && rawImage != null)
            {

                if (_webCamera.isPlaying)
                    StopCamera();
                else
                    PlayCamera();
            }
        }
        if (GUI.Button(new Rect(100, 450, 100, 100), "Quit"))
        {

            Application.Quit();
        }

    }

    public void PlayCamera()
    {
        //Plane.GetComponent<MeshRenderer>().enabled = true;
        rawImage.enabled = true;
        _webCamera.Play();
    }


    public void StopCamera()
    {
        // Plane.GetComponent<MeshRenderer>().enabled = false;
        rawImage.enabled = false;
        _webCamera.Stop();
    }

    /// <summary>  
    /// ��ʼ������ͷ
    /// </summary>  
    public IEnumerator InitCameraCor()
    {
        yield return Application.RequestUserAutho                                                                                                                           rization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            DeviceName = devices[0].name;
            _webCamera = new WebCamTexture(DeviceName, 1920, 1080, 60);

            rawImage.texture = _webCamera;
            //Plane.GetComponent<Renderer>().material.mainTexture = _webCamera;
            //Plane.transform.localScale = new Vector3(1, 1, 1);

            _webCamera.Play();
            //ǰ�ú�������ͷ��Ҫ��תһ���Ƕȣ��������ǲ���ȷ��,��������Play()������
            rawImage.rectTransform.localEulerAngles = new Vector3(0, 0, _webCamera.videoRotationAngle + 360);
        }
    }
}

