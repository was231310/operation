using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.Stl;
using SFB;
using System.IO;

public class LoadStl : MonoBehaviour
{
    public Material stlMaterial; // 自定义材质（STL无纹理）
    public Material qianliexian_m;
    public Material zhanwei_m;
    public Material niaodao_m;

    Dictionary<string, List<CombineInstance>> organ;

    public FingerController Finger;

    public UIManager UI_Manager;

    public GameObject pangguang;
    public GameObject niaodao;
    public GameObject zhanwei;
    public GameObject qianliexian;

    public GameObject Slider_qlx;
    public GameObject Slider_nd;
    public GameObject Slider_zw;


    void Start()
    {
        organ = new Dictionary<string, List<CombineInstance>>();
    }

    public void LoadFile()
    {
        // 使用StandaloneFileBrowser打开文件对话框
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select STL", "", "stl", true);

        if (paths.Length > 0)
        {
            for (int index = 0; index < paths.Length; index++)
            {
                string filePath = paths[index];
                string name = Path.GetFileName(filePath).Split('.')[0];

                if (organ.ContainsKey(name)) return;

                List<CombineInstance> combines = new List<CombineInstance>();
                GameObject obj;
                if (!string.IsNullOrEmpty(filePath))
                {
                    try
                    {
                        #region Obj
                        if (name.Contains("前列腺") && name != "核磁前列腺")
                        {
                            obj = Instantiate<GameObject>(qianliexian);
                            obj.transform.parent = this.transform;
                            obj.transform.localEulerAngles = new Vector3(-90, 0, 90);
                            obj.name = name;

                            obj.transform.localPosition = new Vector3(41f, -35.4f, 8.8f);

                            obj.GetComponent<FFPCtrl>().SetSilderGroup(Slider_qlx);
                        }
                        //else if (name.Contains("膀胱"))
                        //{
                        //    obj = Instantiate<GameObject>(pangguang);
                        //    obj.transform.parent = this.transform;
                        //    obj.transform.localEulerAngles = new Vector3(-90, 0, 90);
                        //    obj.name = name;

                        //    obj.transform.localPosition = new Vector3(77.6f, 1, 5);

                        //    obj.GetComponent<FFPCtrl>().SetSilderGroup(Slider_qlx);
                        //}
                        else if (name.Contains("尿道"))
                        {
                            obj = Instantiate<GameObject>(niaodao);
                            obj.transform.parent = this.transform;
                            obj.transform.localEulerAngles = new Vector3(-90, 0, 90);
                            obj.name = name;

                            obj.transform.localPosition = new Vector3(36.84f, -51.5f, 11.08f);
                            obj.GetComponent<FFPCtrl>().SetSilderGroup(Slider_nd);
                        }
                        else if (name.Contains("占位"))
                        {
                            obj = Instantiate<GameObject>(zhanwei);
                            obj.transform.parent = this.transform;
                            obj.transform.localEulerAngles = new Vector3(-90, 0, 90);
                            obj.name = name;

                            obj.transform.localPosition = new Vector3(42, -31, 20);
                            obj.GetComponent<FFPCtrl>().SetSilderGroup(Slider_zw);
                        }
                        else
                        {
                            Debug.Log(filePath);
                            Mesh[] meshs = Importer.Import(filePath);
                            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            for (int i = 0; i < meshs.Length; i++)
                            {
                                CombineInstance combine = new CombineInstance();
                                combine.mesh = meshs[i];
                                combines.Add(combine);
                            }

                            Mesh mesh = new Mesh();
                            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                            mesh.CombineMeshes(combines.ToArray(), true, false);


                            obj.GetComponent<MeshFilter>().mesh = mesh;

                            obj.transform.parent = transform;
                            obj.transform.localPosition = new Vector3(-100, 80, 0);
                            obj.transform.localEulerAngles = Vector3.zero;
                            obj.name = name;
                        }
                        organ.Add(name, combines);
                        #endregion

                        #region Material
                        Material new_m = new Material(Shader.Find("Standard"));
                        if (name.Contains("动"))
                        {
                            new_m.color = Color.red;
                        }
                        else if (name.Contains("静"))
                        {
                            new_m.color = Color.blue;
                        }
                        //else if (name.Contains("尿道"))
                        //{
                        //    new_m.color = zhanwei_m;
                        //}
                        else if (name.Contains("囊"))
                        {
                            new_m.color = new Color(0.68f, 0.52f, 0.90f);
                        }
                        else if (name.Contains("NVB"))
                        {
                            new_m.color = Color.yellow;
                        }
                        else
                        {
                            new_m.color = Color.white;

                        }
                        if (name.Contains("皮"))
                        {
                            obj.GetComponent<MeshRenderer>().materials = new Material[1] { stlMaterial };
                        }
                        else if (name.Contains("前列腺") && name != "核磁前列腺")
                        {
                            obj.GetComponent<MeshRenderer>().materials = new Material[1] { qianliexian_m };
                        }
                        else if (name.Contains("占位"))
                        {
                            obj.GetComponent<MeshRenderer>().materials = new Material[1] { zhanwei_m };
                        }
                        else if (name.Contains("尿道"))
                        {
                            obj.GetComponent<MeshRenderer>().materials = new Material[1] { niaodao_m };
                        }
                        else
                        {
                            obj.GetComponent<MeshRenderer>().materials = new Material[1] { new_m };
                        }
                        #endregion

                        UI_Manager.CreateUIBtn(obj, organ.Count, name);

                        if (0 != organ.Count && !Finger.CanCtrl) Finger.CanCtrl = true;
                    }
                    catch (System.Exception ex)
                    {
                        // 处理异常情况
                        Debug.LogError("Error loading image: " + ex.Message);
                    }
                }
            }
        }
    }
}
