using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    public GameObject QuestionPrefab;
    public GameObject QuestionGrid;
    public ToggleGroup AnswerToggleGroup;
    public Toggle AnswerTogglePrefab;
    private string file = "studyguides.json";

    [Header("Page Settings")]
    public int QuestionsPerPage = 3;
    public int CurrentPageIndex = 1;
    public int TotalPages;
    public Button NextPageButton;
    public Button PrevPageButton;

    private void Start() {
        LoadQuestions();
    }

    public void LoadQuestions() {
        string path = Path.Combine(Application.streamingAssetsPath, file);
        if(File.Exists(path)) {
            string json = File.ReadAllText(path);
            QuestionData questionData = JsonUtility.FromJson<QuestionData>(json);
            TotalPages = Mathf.CeilToInt(questionData.questions.Count / QuestionsPerPage);

            // Reset Questions
            foreach(Transform question in QuestionGrid.transform) {
                Destroy(question.gameObject);
            }

            if(CurrentPageIndex == 0) {
                PrevPageButton.gameObject.SetActive(false);
            }
            else if(CurrentPageIndex == TotalPages) {
                NextPageButton.gameObject.SetActive(false);
            } else {
                NextPageButton.gameObject.SetActive(true);
                PrevPageButton.gameObject.SetActive(true);
            }

            LoadPage(CurrentPageIndex,questionData);
        }
    }

    public void LoadPage(int page, QuestionData questionData) {
        
        // Index Variables
        int StartIndex = page * QuestionsPerPage;
        int EndIndex = Mathf.Min(StartIndex + QuestionsPerPage, questionData.questions.Count);

        for(int i = StartIndex; i < EndIndex; i++) {
            Debug.Log($"Index: {i}");
            Question question = questionData.questions[i];
            GameObject NewQuestion = Instantiate(QuestionPrefab, QuestionGrid.transform.position, Quaternion.identity, QuestionGrid.transform);
            QuestionEntry questionEntry = NewQuestion.GetComponent<QuestionEntry>();
            questionEntry.SetQuestiontext($"{i+1}. {question.QuestionText}");
            AnswerToggleGroup = questionEntry.GetToggleGroup();
            
            // Creates Answer Choices for Question
            float pos = 30;
            foreach(string answer in question.Answers) {
                Toggle AnswerToggle = Instantiate(AnswerTogglePrefab, AnswerToggleGroup.transform);
                AnswerToggle.GetComponentInChildren<Text>().text = answer;
                AnswerToggle.group = AnswerToggleGroup;
                AnswerToggle.isOn = false;
                AnswerToggle.GetComponent<RectTransform>().anchoredPosition = new Vector3(-170,pos,0);
                pos -= 35;
            }
            questionEntry.SetCorrectAnswerIndex(question.CorrectAnswer);
            
        }
    }

    // Page Functions
    public void NextPage() {
        if(CurrentPageIndex < TotalPages) {
            CurrentPageIndex++;
            LoadQuestions();
        }
    }

    public void PreviousPage() {
        if(CurrentPageIndex > 0) {
            CurrentPageIndex--;
            LoadQuestions();
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
