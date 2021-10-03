using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightZone : MonoBehaviour
{
    public float speed = 8.0f;
    Color originalColor;
    Color startColor;
    Color endColor;
    float startTime;
    bool animating = false;

    void Start() 
    {
        originalColor = gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().materials[1].color;
    }

    void Update() 
    {
        if(animating){
            float t = (Time.time - startTime) * speed;
            gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().materials[1].color = Color.Lerp(startColor, endColor, t);
            if(gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().materials[1].color == endColor){
                animating = false;
            }
        }
    }

    void OnMouseEnter() 
    {
        startTime = Time.time;
        startColor = originalColor;
        endColor = new Color(startColor.r, startColor.g, startColor.b, startColor.a+0.3f);
        animating = true;
    }

    void OnMouseExit() 
    {
        startTime = Time.time;
        startColor = endColor;
        endColor = originalColor;
        animating = true;
    }
}
