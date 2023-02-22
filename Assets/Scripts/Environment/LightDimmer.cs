using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LightDimmer : MonoBehaviour
{
    private Light _light;
    [SerializeField] private float _dimSpeed;
    [SerializeField] private float _brightenSpeed;
    [SerializeField] private float _minIntensity;
    [SerializeField] private float _maxIntensity;
    public bool lightOn;

    private void Awake()
    {
        lightOn = false;
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        if(lightOn && _light.intensity < _maxIntensity + .02f)
        {
            _light.intensity += _brightenSpeed * Time.deltaTime;
        }
        else if (!lightOn && _light.intensity > _minIntensity + .02f)
        {
            _light.intensity -= _dimSpeed * Time.deltaTime; ;
        }
    }
}
