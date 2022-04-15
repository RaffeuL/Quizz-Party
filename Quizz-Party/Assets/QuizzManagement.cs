using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuizzManagement : MonoBehaviour
{ 

    private static List<Question> easyQuestionsNotUsed;
    private static List<Question> mediumQuestionsNotUsed;
    private static List<Question> hardQuestionsNotUsed;

    private static Question currentQuestion;

   
    // Start is called before the first frame update
    void Start()
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

    public static void ShowQuizz()
    {
        Debug.Log("A questão é: " + currentQuestion.question);
        foreach(Answer answers in currentQuestion.answers)
        {
            Debug.Log("Resposta " + answers.answer);
        }
        int indexCorrectAnswer;
        int.TryParse(currentQuestion.correct, out indexCorrectAnswer);
        Debug.Log("Resposta Correta: " + currentQuestion.answers[indexCorrectAnswer].answer);
    }

    public static void GetEasyRandomQuestion()
    {
        int questionIndex = Random.Range(0, easyQuestionsNotUsed.Count);
        currentQuestion = easyQuestionsNotUsed[questionIndex];
        easyQuestionsNotUsed.RemoveAt(questionIndex);
        
    }

    public static void GetMediumRandomQuestion()
    {
        int questionIndex = Random.Range(0, mediumQuestionsNotUsed.Count);
        currentQuestion = mediumQuestionsNotUsed[questionIndex];
        mediumQuestionsNotUsed.RemoveAt(questionIndex);
    }

    public static void GetHardRandomQuestion()
    {
        int questionIndex = Random.Range(0, hardQuestionsNotUsed.Count);
        currentQuestion = hardQuestionsNotUsed[questionIndex];
        hardQuestionsNotUsed.RemoveAt(questionIndex);
    }
}
