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
    public GameObject choicesContainer; // �I������\������p�l��
    public Button choiceButtonPrefab; // �I�����̃{�^��

    private Story story;
    private List<Button> choiceButtons = new List<Button>();

    void Start()
    {
        if (inkJSONAsset == null)
        {
            Debug.LogError("Ink JSON Asset ���ݒ肳��Ă��܂���I");
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
            // �ʏ�̃e�L�X�g��\��
            dialogueText.text = story.Continue();
            nextButton.gameObject.SetActive(true);
            choicesContainer.SetActive(false); // �I�������\��
        }
        else if (story.currentChoices.Count > 0)
        {
            // �I����������ꍇ
            nextButton.gameObject.SetActive(false);
            DisplayChoices();
        }
        else
        {
            // ���ꂪ�I��������{�^�����\��
            nextButton.gameObject.SetActive(false);
        }
    }

    void DisplayChoices()
    {
        // �����̑I�����{�^�����폜
        foreach (Button btn in choiceButtons)
        {
            Destroy(btn.gameObject);
        }
        choiceButtons.Clear();

        // �I�������쐬
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
