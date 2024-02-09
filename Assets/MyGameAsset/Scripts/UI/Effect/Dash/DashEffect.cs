using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashEffect : MonoBehaviour, IDashEffect
{
    [SerializeField] Image imageComponent;

    public void SetImageVisible(bool isVisible)
    {
        imageComponent.enabled = isVisible;
    }
}