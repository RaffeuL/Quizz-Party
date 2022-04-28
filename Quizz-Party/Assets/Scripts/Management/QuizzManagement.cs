using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuizzManagement : MonoBehaviour
{ 
    private static List<Question> easyQuestionsNotUsed;
    private static List<Question> mediumQuestionsNotUsed;
    private static List<Question> hardQuestionsNotUsed;

    private static Question currentQuestion;

    [SerializeField] private Text questionText;
    [SerializeField] private Text answer1;
    [SerializeField] private Text answer2;
    [SerializeField] private Text answer3;
    [SerializeField] private Text answer4;

    private string correctAnswer;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuizz()
    {
        Debug.Log("A questão é: " + currentQuestion.question);
        foreach(Answer answers in currentQuestion.answers)
        {
            Debug.Log("Resposta " + answers.answer);
        }

        questionText.text = currentQuestion.question;
        answer1.text = currentQuestion.answers[0].answer;
        answer2.text = currentQuestion.answers[1].answer;
        answer3.text = currentQuestion.answers[2].answer;
        answer4.text = currentQuestion.answers[3].answer;
        int indexCorrectAnswer;
        int.TryParse(currentQuestion.correct, out indexCorrectAnswer);
        correctAnswer = currentQuestion.answers[indexCorrectAnswer].answer;
        Debug.Log("Resposta Certa " + correctAnswer);
    }

    public void GetEasyRandomQuestion()
    {
        Debug.Log("wake wake aaae");
        int questionIndex = Random.Range(0, easyQuestionsNotUsed.Count);
        currentQuestion = easyQuestionsNotUsed[questionIndex];
        easyQuestionsNotUsed.RemoveAt(questionIndex);
        
    }

    public void GetMediumRandomQuestion()
    {
        Debug.Log("wake wake aaae");
        int questionIndex = Random.Range(0, mediumQuestionsNotUsed.Count);
        currentQuestion = mediumQuestionsNotUsed[questionIndex];
        mediumQuestionsNotUsed.RemoveAt(questionIndex);
    }

    public void GetHardRandomQuestion()
    {
        Debug.Log("wake wake aaae");
        int questionIndex = Random.Range(0, hardQuestionsNotUsed.Count);
        currentQuestion = hardQuestionsNotUsed[questionIndex];
        hardQuestionsNotUsed.RemoveAt(questionIndex);
    }

    public void CheckAnswer(Text answer)
    {
        if(answer.text == correctAnswer)
        {
            Debug.Log("Acerto!");
        }else
        {
            Debug.Log("Erro");
        }
    }
}
