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

    // keeping check of the current Score
    void Update()
    {
        text.text = "Score: " + score;
    }
}
