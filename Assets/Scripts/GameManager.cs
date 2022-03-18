using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{

    private Trivia trivia;

    public GameObject preguntas;

    public GameObject resp1;

    public GameObject resp2;

    public GameObject resp3;

    public GameObject resp4;



    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(GetRequest("https://opentdb.com/api.php?amount=10&category=15"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetRequest(string uri)
    {
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();



            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    trivia = JsonUtility.FromJson<Trivia>(webRequest.downloadHandler.text);

                    //trivia.results = JsonUtility.FromJson<Questions>();

                    //Debug.Log(trivia.response_code+" "+trivia.results[0].category);

                    foreach (Questions item in trivia.results){

                        Debug.Log("Categoría: "+item.category);
                        Debug.Log("Tipo: "+item.type);
                        Debug.Log("Dificultad: "+item.dificulty);
                        Debug.Log("Pregunta: "+item.question);
                        Debug.Log("Respuesta correcta: "+item.correct_answer);
                        
                        for(int i = 0; i < item.incorrect_answers.Count; i++){

                            Debug.Log("Respuesta incorrecta "+(i+1)+": "+item.incorrect_answers[i]);

                        }
                    }
                    break;
            }
        }
    }
}
