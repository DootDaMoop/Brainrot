using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public bool IsCorrectAnswer() {
        foreach(Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>()) {
            if(toggle.isOn) {
                int SelectedIndex = toggle.transform.GetSiblingIndex();
                Debug.Log($"Selected Index: {SelectedIndex}, Correct Answer Index: {CorrectAnswerIndex}, Is Correct: {SelectedIndex == CorrectAnswerIndex}");
                return SelectedIndex == CorrectAnswerIndex;
            }
        }
        // SelectedToggle is null
        return false;
    }

    public void SetCorrectAnswerIndex(int index) {
        this.CorrectAnswerIndex = index;
    }

    public ToggleGroup GetToggleGroup() {
        return this.toggleGroup;
    }
}
