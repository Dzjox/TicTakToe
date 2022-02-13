using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [SerializeField] private float _secondsToCircle = 4;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rectTransform.eulerAngles += new Vector3(0, 0, (360 * Time.fixedDeltaTime / _secondsToCircle));
    }
}
