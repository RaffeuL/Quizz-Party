using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuizzManagement : MonoBehaviour
{
    #region Dificult
    private static List<Question> easyQuestionsNotUsed;
    private static List<Question> mediumQuestionsNotUsed;
    private static List<Question> hardQuestionsNotUsed;
    #endregion

    #region Question Texts UI
    [SerializeField] private Text questionText;
    [SerializeField] private Text answer1;
    [SerializeField] private Text answer2;
    [SerializeField] private Text answer3;
    [SerializeField] private Text answer4;
    #endregion

    private static Question currentQuestion;
    private string correctAnswer;
    void Awake()
    {   
        if(easyQuestionsNotUsed == null || easyQuestionsNotUsed.Count == 0)
        {
            easyQuestionsNotUsed = QuestionReader.easyQuestionsList;
        }

        if(mediumQuestionsNotUsed == null || mediumQuestionsNotUsed.Count == 0)
        {
            mediumQuestionsNotUsed = QuestionReader.mediumQuestionsList;
        }

        if(hardQuestionsNotUsed == null || hardQuestionsNotUsed.Count == 0)
        {
            hardQuestionsNotUsed = QuestionReader.hardQuestionsList;
        }
        
    }

    public void BuildQuizz()
    {
        questionText.text = currentQuestion.question;
        answer1.text = currentQuestion.answers[0].answer;
        answer2.text = currentQuestion.answers[1].answer;
        answer3.text = currentQuestion.answers[2].answer;
        answer4.text = currentQuestion.answers[3].answer;
        int indexCorrectAnswer;
        int.TryParse(currentQuestion.correct, out indexCorrectAnswer);
        correctAnswer = currentQuestion.answers[indexCorrectAnswer].answer;
    }

    public void GetEasyRandomQuestion()
    {
        int questionIndex = Random.Range(0, easyQuestionsNotUsed.Count);
        currentQuestion = easyQuestionsNotUsed[questionIndex];
        easyQuestionsNotUsed.RemoveAt(questionIndex);
        
    }

    public void GetMediumRandomQuestion()
    {
        int questionIndex = Random.Range(0, mediumQuestionsNotUsed.Count);
        currentQuestion = mediumQuestionsNotUsed[questionIndex];
        mediumQuestionsNotUsed.RemoveAt(questionIndex);
    }

    public void GetHardRandomQuestion()
    {
        int questionIndex = Random.Range(0, hardQuestionsNotUsed.Count);
        currentQuestion = hardQuestionsNotUsed[questionIndex];
        hardQuestionsNotUsed.RemoveAt(questionIndex);
    }

    public void CheckAnswer(Text answer)
    {
        if(answer.text == correctAnswer)
        {
            PlayerPiece.me.answeredRight = true;
        }
        else
        {
            GameSystem.Instance.EndQuizz();
        }
    }
}
