using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class QuestionReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public Questions questionsInJson;
    public WorldTimeAPI timeData;

    public static List<Question> easyQuestionsList = new List<Question>();
    public static List<Question> mediumQuestionsList = new List<Question>();
    public static List<Question> hardQuestionsList = new List<Question>();
    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine(GetData());
        
        questionsInJson = JsonUtility.FromJson<Questions>(jsonFile.text);
        
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

    IEnumerator GetData()
    {
        //string uri = "http://worldtimeapi.org/api/timezone/America/Belem";
        string uri = "https://api-gamification.herokuapp.com/quest";
        string url = "https://raw.githubusercontent.com/RaffeuL/Quizz-Party/main/Quizz-Party/Assets/Quizz/questions.json";

        
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(": Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                string json = webRequest.downloadHandler.text;
                
                //Debug.Log("Aqui: " + json);
                //timeData = JsonUtility.FromJson<WorldTimeAPI>(json);
                questionsInJson = JsonUtility.FromJson<Questions>(json);  
                //Debug.Log("Aqui: " + timeData.datetime);
                break;
        }        
    }

    private void FillQuestions()
    {        
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
