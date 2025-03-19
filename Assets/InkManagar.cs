using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using System.Collections.Generic;

public class InkManager : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    public TMP_Text dialogueText;
    public Button nextButton;
    public GameObject choicesContainer; // 選択肢を表示するパネル
    public Button choiceButtonPrefab; // 選択肢のボタン

    private Story story;
    private List<Button> choiceButtons = new List<Button>();

    void Start()
    {
        if (inkJSONAsset == null)
        {
            Debug.LogError("Ink JSON Asset が設定されていません！");
            return;
        }

        story = new Story(inkJSONAsset.text);
        DisplayNextLine();
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    void DisplayNextLine()
    {
        if (story.canContinue)
        {
            // 通常のテキストを表示
            dialogueText.text = story.Continue();
            nextButton.gameObject.SetActive(true);
            choicesContainer.SetActive(false); // 選択肢を非表示
        }
        else if (story.currentChoices.Count > 0)
        {
            // 選択肢がある場合
            nextButton.gameObject.SetActive(false);
            DisplayChoices();
        }
        else
        {
            // 物語が終了したらボタンを非表示
            nextButton.gameObject.SetActive(false);
        }
    }

    void DisplayChoices()
    {
        // 既存の選択肢ボタンを削除
        foreach (Button btn in choiceButtons)
        {
            Destroy(btn.gameObject);
        }
        choiceButtons.Clear();

        // 選択肢を作成
        choicesContainer.SetActive(true);
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Choice choice = story.currentChoices[i];
            Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer.transform);
            choiceButton.GetComponentInChildren<TMP_Text>().text = choice.text;
            choiceButton.onClick.AddListener(() => ChooseChoice(choice.index));
            choiceButtons.Add(choiceButton);
        }
    }

    void ChooseChoice(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        DisplayNextLine();
    }
}
