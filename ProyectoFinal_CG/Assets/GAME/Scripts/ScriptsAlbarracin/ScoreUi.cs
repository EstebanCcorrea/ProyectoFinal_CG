using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI txtScore;

    void Update()
    {
        if (GameManager1.Instance != null && txtScore != null)
        {
            txtScore.text = GameManager1.Instance.totalScore.ToString();
        }
    }
}
