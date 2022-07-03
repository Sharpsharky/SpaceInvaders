using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField] private float xSpeed, ySpeed;

    private RawImage image;

    private void Awake()
    {
        image = GetComponent<RawImage>();
    }

    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(xSpeed, ySpeed) * Time.deltaTime, image.uvRect.size);
    }
}
