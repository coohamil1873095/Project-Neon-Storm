using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessController : MonoBehaviour
{
    private Light gameLight;
    
    // Start is called before the first frame update
    void Start()
    {
        gameLight = GameObject.Find("Directional Light").GetComponent<Light>();
    }

    public void SetBrightness(float sliderValue)
    {
        gameLight.intensity = sliderValue;
    }
}
