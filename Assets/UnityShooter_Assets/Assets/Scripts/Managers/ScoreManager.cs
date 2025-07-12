using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    /// <summary>
    /// keeping check of the current Score
    /// </summary>
    private void Update()
    {
        text.text = "Score: " + score;
    }
}
