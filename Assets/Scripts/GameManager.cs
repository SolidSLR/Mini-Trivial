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
                case UnityWebRequest.Result.Success:

                    trivia = JsonUtility.FromJson<Trivia>(webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
