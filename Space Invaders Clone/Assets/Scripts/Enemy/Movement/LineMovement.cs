using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMovement : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(DelayAnimation());
    }

    private IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(Random.Range(0.10f,0.70f));
        animator.enabled = true;
        yield return null;
    }




}
