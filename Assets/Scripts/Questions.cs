using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Questions {

    public string category;

    public string type;

    public string dificulty;

    public string question;

    public string correct_answer;

    public List<string> incorrect_answers;
    
}
