using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MegaFiers;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class FFPCtrl : MonoBehaviour
{
    public string Name;
    public MegaBend LRbend;
    public MegaBend FBbend;
    public MegaStretch stretch;

    private Slider Alpha;
    private Slider Left_right;
    private Slider Front_back;
    private Slider Stretch;

    private GameObject sliderGroup;

    public float BendScale;
    private Text alpha_text_value;
    private Text lr_text_value;
    private Text fb_text_value;
    private Text str_text_value;

    private InputField[] inputFields;

    Material m;

    // Start is called before the first frame update
    void Start()
    {
        BendScale = 80;
        MegaBend[] bends = this.GetComponentsInChildren<MegaBend>();
        if (bends[0].axis.Equals(MegaAxis.Y))
        {
            LRbend = bends[0];
            FBbend = bends[1];
        }
        else
        {
            FBbend = bends[0];
            LRbend = bends[1];
        }
        stretch = this.GetComponent<MegaStretch>();

        m = GetComponent<MeshRenderer>().material;
    }

    public void SetSilderGroup(GameObject g)
    {
        sliderGroup = g;
        g.SetActive(true);
        inputFields = g.GetComponentsInChildren<InputField>();
        Slider[] sliders = g.GetComponentsInChildren<Slider>();
        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].name.Equals("Alpha"))
            {
                Alpha = sliders[i];
                inputFields[0].onEndEdit.AddListener(OnAlphaInputFiledEndEdit);
                Alpha.transform.Find("Reset").GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnAlphaInputFiledEndEdit("0.5");
                });
            }
            if (sliders[i].name.Equals("Left-rightBend"))
            {
                Left_right = sliders[i];
                inputFields[1].onEndEdit.AddListener(OnLRInputFiledEndEdit);
                Left_right.transform.Find("Reset").GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnLRInputFiledEndEdit("0");
                });
            }
            if (sliders[i].name.Equals("Front-backtBend"))
            {
                Front_back = sliders[i];
                inputFields[2].onEndEdit.AddListener(OnBFInputFiledEndEdit);
                Front_back.transform.Find("Reset").GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnBFInputFiledEndEdit("0");
                });
            }
            if (sliders[i].name.Equals("Stretch"))
            {
                Stretch = sliders[i];
                inputFields[3].onEndEdit.AddListener(OnStretchInputFiledEndEdit);
                Stretch.transform.Find("Reset").GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnStretchInputFiledEndEdit("0");
                });
            }
        }

        g.transform.Find("exit").GetComponent<Button>().onClick.AddListener(() => { g.SetActive(false); });

        Alpha.onValueChanged.AddListener(OnAlphaValueChanged);
        Left_right.onValueChanged.AddListener(OnLeftRightValueChanged);
        Front_back.onValueChanged.AddListener(OnFrontBackValueChanged);
        Stretch.onValueChanged.AddListener(OnStretchValueChanged);
        g.SetActive(false);
    }

    private void OnAlphaInputFiledEndEdit(string v)
    {
        float value = float.Parse(v);
        if (value < 0 || value > 1) return;
        OnAlphaValueChanged(value);
        Alpha.value = value;
    }

    private void OnLRInputFiledEndEdit(string v)
    {
        float value = float.Parse(v);
        //v * BendScale - (BendScale / 2)
        float temp_v = (value / BendScale) + 0.5f;
        if (temp_v > 1 || temp_v < 0) return;

        OnLeftRightValueChanged(temp_v);
        Left_right.value = temp_v;
    }

    private void OnBFInputFiledEndEdit(string v)
    {
        float value = float.Parse(v);
        //v * BendScale - (BendScale / 2)
        float temp_v = (value / BendScale) + 0.5f;
        if (temp_v > 1 || temp_v < 0) return;

        OnFrontBackValueChanged(temp_v);
        Front_back.value = temp_v;
    }

    private void OnStretchInputFiledEndEdit(string v)
    {
        float value = float.Parse(v);
        float temp_v = (value + 0.1f) * 4;
        Debug.Log(temp_v);
        if (temp_v < 0 || temp_v > 1) return;
        OnStretchValueChanged(temp_v);
        Stretch.value = temp_v;
    }


    private void OnAlphaValueChanged(float v)
    {
        //alpha_text_value.text = v.ToString("F2");
        m.color = new Color(m.color.r, m.color.g, m.color.b, v);
        inputFields[0].text = v.ToString("F2");
    }
    private void OnLeftRightValueChanged(float v)
    {
        float temp_v = v * BendScale - (BendScale / 2);
        LRbend.angle = temp_v;
        Debug.Log($"v: {v}   temp_v: {temp_v}");
        inputFields[1].text = temp_v.ToString("F2");
        //lr_text_value.text = temp_v.ToString("F2");
    }
    private void OnFrontBackValueChanged(float v)
    {
        float temp_v = v * BendScale - (BendScale / 2);
        FBbend.angle = temp_v;
        inputFields[2].text = temp_v.ToString("F2");
        //fb_text_value.text = temp_v.ToString("F2");
    }
    private void OnStretchValueChanged(float v)
    {
        Debug.Log(v);
        float temp_v = 0.25f * v - 0.1f;
        stretch.amount = temp_v;
        inputFields[3].text = temp_v.ToString("F2");
        //str_text_value.text = temp_v.ToString("F2");
    }

    private void OnDisable()
    {
        sliderGroup.SetActive(false);
    }
}

