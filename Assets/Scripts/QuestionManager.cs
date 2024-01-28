using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    public GameObject QuestionPrefab;
    public GameObject QuestionGrid;
    private string file = "studyguides.json";

    private void Start() {
        LoadQuestions();
    }

    public void LoadQuestions() {
        string path = Path.Combine(Application.streamingAssetsPath, file);
        if(File.Exists(path)) {
            string json = File.ReadAllText(path);
            QuestionData questionData = JsonUtility.FromJson<QuestionData>(json);

            foreach(Question question in questionData.questions) {
                GameObject NewQuestion = Instantiate(QuestionPrefab, QuestionGrid.transform.position, Quaternion.identity, QuestionGrid.transform);
            }
        }
    }
}

[Serializable]
public class Question {
    public string QuestionText;
    public List<string> Answers;
    public int CorrectAnswer;
}

[Serializable]
public class QuestionData {
    public int StudyGuideVersion;
    public List<Question> questions;
}
