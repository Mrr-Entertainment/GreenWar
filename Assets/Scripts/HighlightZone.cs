using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightZone : MonoBehaviour
{
    public float speed = 1.0f;
    Color startColor;
    Color endColor;
    float startTime;
    bool animating = false;

    void Update() {
        if(animating){
            float t = (Time.time - startTime) * speed;
            gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().materials[1].color = Color.Lerp(startColor, endColor, t);
        }
    }

    void OnMouseEnter() {
        Debug.Log("Mouse entered");
        startTime = Time.time;
        startColor = gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().materials[1].color;
        endColor = new Color(startColor.r+0.5f, startColor.g+0.5f, startColor.b+0.5f, startColor.a+0.1f);
        animating = true;
    }

    void OnMouseExit() {
        Debug.Log("Mouse exited");
        gameObject.GetComponent<UnityEngine.U2D.SpriteShapeRenderer>().materials[1].color = startColor;
        animating = false;
    }
}
