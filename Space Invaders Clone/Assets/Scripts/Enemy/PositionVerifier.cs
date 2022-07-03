using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionVerifier : MonoBehaviour
{
    private float frequencyVerification = 1;
    private IEnumerator checkThePosition;

    private void OnEnable()
    {
        checkThePosition = CheckPosition();
        StartCoroutine(checkThePosition);
    }
    private void OnDisable()
    {
        StopCoroutine(checkThePosition);
    }

    private IEnumerator CheckPosition()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(frequencyVerification);
            
        }
    }




}
