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
    public ToggleGroup AnswerToggleGroup;
    public Toggle AnswerTogglePrefab;
    private string file = "studyguides.json";

    private void Start() {
        LoadQuestions();
    }

    public void LoadQuestions() {
        string path = Path.Combine(Application.streamingAssetsPath, file);
        if(File.Exists(path)) {
            string json = File.ReadAllText(path);
            QuestionData questionData = JsonUtility.FromJson<QuestionData>(json);

            int i = 1;
            // Creates Question and sets Question Text
            foreach(Question question in questionData.questions) {
                GameObject NewQuestion = Instantiate(QuestionPrefab, QuestionGrid.transform.position, Quaternion.identity, QuestionGrid.transform);
                QuestionEntry questionEntry = NewQuestion.GetComponent<QuestionEntry>();
                questionEntry.SetQuestiontext($"{i}. {question.QuestionText}");
                AnswerToggleGroup = questionEntry.GetToggleGroup();
                
                // Creates Answer Choices for Question
                float pos = 30;
                foreach(string answer in question.Answers) {
                    Toggle AnswerToggle = Instantiate(AnswerTogglePrefab, AnswerToggleGroup.transform);
                    AnswerToggle.GetComponentInChildren<Text>().text = answer;
                    AnswerToggle.GetComponent<RectTransform>().anchoredPosition = new Vector3(-170,pos,0);
                    pos -= 35;
                }
                i++;
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
