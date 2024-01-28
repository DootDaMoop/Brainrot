using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour {
    [Header("Question Settings")]
    public GameObject QuestionPrefab;
    public GameObject QuestionGrid;
    public ToggleGroup AnswerToggleGroup;
    public Toggle AnswerTogglePrefab;
    private string file = "studyguides.json";

    [Header("Page Settings")]
    public int QuestionsPerPage = 3;
    public int CurrentPageIndex = 1;
    public int TotalPages;
    
    [Header("Buttons")]
    public Button NextPageButton;
    public Button PrevPageButton;
    public Button SubmitButton;

    private void Start() {
        string path = Path.Combine(Application.streamingAssetsPath, file);
        if(File.Exists(path)) {
            string json = File.ReadAllText(path);
            QuestionData questionData = JsonUtility.FromJson<QuestionData>(json);
            TotalPages = Mathf.CeilToInt(questionData.questions.Count / QuestionsPerPage);

            int QuestionNum = 1;
            foreach(Question question in questionData.questions) {
                //Debug.Log($"Index: ");
                
                //Question question = questionData.questions[i];
                GameObject NewQuestion = Instantiate(QuestionPrefab, QuestionGrid.transform.position, Quaternion.identity, QuestionGrid.transform);
                QuestionEntry questionEntry = NewQuestion.GetComponent<QuestionEntry>();
                questionEntry.SetQuestiontext($"{QuestionNum}. {question.QuestionText}");
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
                QuestionNum++;
            }

            LoadQuestions();
        }
    }

    public void LoadQuestions() {
        // Hides all previous page questions (Destroying will make answers unsaved)
        foreach(Transform child in QuestionGrid.transform) {
            child.gameObject.SetActive(false);
        }

        // Enable or Disable Page Turn Buttons based on if at first or last page.
        if(CurrentPageIndex == 0) {
            PrevPageButton.gameObject.SetActive(false);
        }
        else if(CurrentPageIndex == TotalPages) {
            NextPageButton.gameObject.SetActive(false);
            SubmitButton.gameObject.SetActive(true);
        } else {
            NextPageButton.gameObject.SetActive(true);
            PrevPageButton.gameObject.SetActive(true);
            SubmitButton.gameObject.SetActive(false);
        }
        
        LoadPage(CurrentPageIndex);
    }

    // Enable or Disable questions based on Current Page
    public void LoadPage(int page) {
        for(int i = 0; i < QuestionGrid.transform.childCount; i++) {
            if(i >= page * QuestionsPerPage && i < (page+1) * QuestionsPerPage) {
                QuestionGrid.transform.GetChild(i).gameObject.SetActive(true);
            }
            else {
                QuestionGrid.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void SubmitAnswers() {
        int CorrectAnswerCounter = 0;

        foreach(Transform question in QuestionGrid.transform) {
            QuestionEntry questionEntry = question.GetComponent<QuestionEntry>();

            if(questionEntry.IsCorrectAnswer()) {
                CorrectAnswerCounter++;
            }
        }
        
        float Grade = (float)CorrectAnswerCounter / QuestionGrid.transform.childCount;
        Debug.Log($"Grade: {Grade}% | {CorrectAnswerCounter} correct answers.");
        if(Grade > 0.7) {
            StudySceneManager.instance.sceneSwitcher.GoScene("GoodEnd");
        }  else {
            StudySceneManager.instance.sceneSwitcher.GoScene("End");
        }      
    }

    #region Page Functions
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
    #endregion

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
