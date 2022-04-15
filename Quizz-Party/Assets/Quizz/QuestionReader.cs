using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionReader : MonoBehaviour
{
    public TextAsset jsonFile;


    public static List<Question> easyQuestionsList = new List<Question>();
    public static List<Question> mediumQuestionsList = new List<Question>();
    public static List<Question> hardQuestionsList = new List<Question>();
    // Start is called before the first frame update
    void Start()
    {
        Questions questionsInJson = JsonUtility.FromJson<Questions>(jsonFile.text);
        
        foreach (Question question in questionsInJson.easy)
        {
            easyQuestionsList.Add(question);
        }

        foreach (Question question in questionsInJson.medium)
        {
            mediumQuestionsList.Add(question);
        }

        foreach (Question question in questionsInJson.hard)
        {
            hardQuestionsList.Add(question);
        }
        
    }
}
