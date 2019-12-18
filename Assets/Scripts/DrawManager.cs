using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawManager : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float drawValue = 0f;
    [SerializeField]
    private Image fillImage = null;
    [SerializeField]
    private Color lowColor = Color.green;
    [SerializeField]
    private Color highColor = Color.red;

    private float maxHeight;
    private float cachedDrawVal = -1f;

    public float DrawStrength
    {
        set
        {
            // Limit ranges
            drawValue = Mathf.Clamp01(value);
        }
        get
        {
            return drawValue;
        }
    }

    void Start()
    {
        maxHeight = fillImage.rectTransform.sizeDelta.y;
    }

    void Update()
    {
        // Bail if a refresh is not necessary
        if (drawValue == cachedDrawVal)
        {
            return;
        }

        fillImage.color = Color.Lerp(lowColor, highColor, drawValue);
        Vector2 appliedSizeDelta = new Vector2(fillImage.rectTransform.sizeDelta.x, Mathf.Lerp(0f, maxHeight, drawValue));
        fillImage.rectTransform.sizeDelta = appliedSizeDelta;
        cachedDrawVal = drawValue;
    }
}
