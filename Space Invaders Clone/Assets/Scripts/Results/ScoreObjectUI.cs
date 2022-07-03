using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreObjectUI : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] TMP_Text date;

    public TMP_Text Score { get => score;}
    public TMP_Text Date { get => date;}
}
