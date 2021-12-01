using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    private Slider slider;
    public float FillSpeed = 0.5f;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            slider.value = slider.value + FillSpeed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            slider.value = 0f;
        }
    }
}
