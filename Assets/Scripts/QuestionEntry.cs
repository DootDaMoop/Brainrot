using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionEntry : MonoBehaviour
{
    public TMP_Text QuestionText;
    public ToggleGroup toggleGroup;
    public int CorrectAnswerIndex;

    public void SetQuestiontext(string QuestionText) {
        this.QuestionText.text = QuestionText;
    }

    public void SetCorrectAnswerIndex(int index) {
        this.CorrectAnswerIndex = index;
    }

    
    public ToggleGroup GetToggleGroup() {
        return this.toggleGroup;
    }
    
}
