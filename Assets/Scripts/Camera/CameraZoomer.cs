using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    void Update()
    {
#if UNITY_EDITOR
        var scrollDelta = Input.mouseScrollDelta;
        if (scrollDelta.magnitude > 0)
        {
            float prevMagnitude = 0;
            float currentMagnitude = scrollDelta.magnitude;
            var inc = (currentMagnitude - prevMagnitude) * Math.Sign(scrollDelta.y);
            Debug.Log(cinemachineVirtualCamera.m_Lens.OrthographicSize - inc);
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(cinemachineVirtualCamera.m_Lens.OrthographicSize - inc, 5, 20);
        }
#endif

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            var inc = (currentMagnitude - prevMagnitude) * 0.01f;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(cinemachineVirtualCamera.m_Lens.OrthographicSize - inc, 5, 20);
        }
    }
}
