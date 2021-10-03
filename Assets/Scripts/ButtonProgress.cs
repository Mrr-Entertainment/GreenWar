using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Usage:
// Create a button you want to show some progress on
// Create a child-GameObject and attach this script to it
// Add a Sprite to the child-GameObjects Image (this will be the "Overlay")
// Call this scripts StartFill-method with the time it should take
// Enjoy
// You can then wait for it to fill up or stop it manually using the StopFill-method

/* [RequireComponent(typeof(Image))] */
public class ButtonProgress : MonoBehaviour
{
    #region Variables & Fields
    public Text text;
    public bool showText = false;

    private Image image;

    private bool shouldFill = false;
    private float fillTime = 0.0f;
    private float elapsedFillTime = 0.0f;

    public bool flashWhiteOnFinish = true;
    public float flashDuration = 0.1f;
    public int flashCount = 2;

	public Button button;
    private Color originalColor;
    #endregion

    #region Unity Methods
    void Awake()
    {
        // Set standards
        image = GetComponent<Image>();
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Radial360;
        image.fillOrigin = 2;  // 2 == Top -> There is no enum in Unity for this
        image.fillAmount = 0.0f;
        image.fillClockwise = true;

        originalColor = image.color;

        if (showText)
        {
            if (text == null)
            {
                Debug.LogError("No Text to show ButtonProgress to!");
                showText = false;
            }
            else
            {
                text.text = string.Empty;
            }
        }
    }

    void Update()
    {
        if (shouldFill && fillTime != 0.0f)
        {
            elapsedFillTime += Time.deltaTime;
            if (elapsedFillTime <= fillTime)
            {
                var fillPercentage = 1-  elapsedFillTime/ fillTime;
                image.fillAmount = fillPercentage;

                if (showText)
                {
                    text.text = (fillTime - elapsedFillTime).ToString("0.00");
                }
            }
            else
            {
                StopFill();
            }
        }
    }
    #endregion

    #region Public Methods
    public void StartFill(float timeToFill)
    {
        shouldFill = true;
        fillTime = 10f;
        elapsedFillTime = 0.0f;
        image.fillAmount = 1.0f;

        if (showText)
        {
            text.text = string.Empty;
        }
		button.enabled = false;
    }

    public void StopFill()
    {
        if (showText)
        {
            text.text = string.Empty;
        }

        shouldFill = false;
        fillTime = 0.0f;
        elapsedFillTime = 0.0f;

        if (flashWhiteOnFinish)
        {
            StopCoroutine(Flash());
            StartCoroutine(Flash());
        }
        else
        {
            image.fillAmount = 0.0f;
        }

		button.enabled = true;
    }
    #endregion

    #region Non Public Methods
    private IEnumerator Flash()
    {
        image.fillAmount = 1.0f;

        for (int i = 0; i < flashCount; i++)
        {
            image.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
            image.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }

        image.fillAmount = 0.0f;
    }
    #endregion
}

