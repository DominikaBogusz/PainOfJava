using UnityEngine;

public class QuestionPoint : QuestionManager {

    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            if (index >= questionsToBeDisplay.Count)
            {
                Debug.Log("Out of questions!");
                Destroy(this.gameObject);
                return;
            }

            //pausing the game
            Time.timeScale = 0.0f;

            spottedQuestionPointsCount++;

            FillWithData();
            questionCanvas.SetActive(true);
            ChangeButton("Odpowiedz", CheckAnswers);
        }
    }

    public void CheckAnswers()
    {
        if (IsCorrect())
        {
            Player.PlayerStats.WonPoints++;
            correctIndexes.Add(index);
        }
        else
        {
            Player.PlayerStats.LostPoints++;
            wrongIndexes.Add(index);
        }

        PointsIndicator.SetPoints(Player.PlayerStats.WonPoints, Player.PlayerStats.GetAllPoints());

        ChangeButton("Graj dalej", GoBack);
    }

    public void GoBack()
    {
        index++;

        //unpausing the game
        Time.timeScale = 1.0f;

        Destroy(this.gameObject);
    }
}
