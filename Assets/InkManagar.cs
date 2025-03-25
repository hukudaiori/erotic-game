using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using System.Collections.Generic;

public class InkManager : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    public TMP_Text dialogueText;
    public GameObject choicesContainer; // パネル
    public Button choiceButtonPrefab;   // プレハブ

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!choicesContainer.activeSelf)
                DisplayNextLine();
        }
    }

    void DisplayNextLine()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
            ClearChoices();
        }
        else if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
    }

    void DisplayChoices()
    {
        ClearChoices();

        choicesContainer.SetActive(true);

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer.transform);
            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();
            choiceText.text = choice.text.Trim();

            int choiceIndex = choice.index;
            choiceButton.onClick.AddListener(() =>
            {
                story.ChooseChoiceIndex(choiceIndex);
                DisplayNextLine();
            });

            choiceButtons.Add(choiceButton);
        }
    }

    void ClearChoices()
    {
        foreach (Button button in choiceButtons)
        {
            Destroy(button.gameObject);
        }
        choiceButtons.Clear();
        choicesContainer.SetActive(false);
    }
}
