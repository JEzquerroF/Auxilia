using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    public void UpdateHealthbar(float currentValue)
    {
        slider.value = currentValue;
    }

   void Update()
   {
        transform.rotation = cam.transform.rotation;
        transform.position = target.position + offset;
   }
}
