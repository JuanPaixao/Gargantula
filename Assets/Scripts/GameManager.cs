using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;


    public void ZoomIn()
    {
        StartCoroutine(ZoomInCoroutine());
    }
    public void ZoomOut()
    {
        StartCoroutine(ZoomOutCoroutine());
    }
    public IEnumerator ZoomInCoroutine()
    {
        while (virtualCamera.m_Lens.FieldOfView > 45f)
        {
            virtualCamera.m_Lens.FieldOfView--;
            yield return null;
        }
    }
    public IEnumerator ZoomOutCoroutine()
    {
        while (virtualCamera.m_Lens.FieldOfView <= 75f)
        {
            virtualCamera.m_Lens.FieldOfView++;
            yield return null;
        }
    }
}
