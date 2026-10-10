using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{

    [SerializeField] private Volume v;
    [SerializeField] private Slider s;

    private LiftGammaGain gamma;
    
    public void ChangeBrightness(float value)
    {
        if (v.profile.TryGet<LiftGammaGain>(out gamma))
        {
            s.onValueChanged.AddListener(ChangeGamma);
        }
        Debug.Log(Screen.brightness);
    }
    
    private void ChangeGamma(float value)
    {
        if (gamma != null)
        {
            Vector4 newGammaValue = new Vector4(value, value, value, value);
            gamma.gamma.Override(newGammaValue);
        }
    }
}
