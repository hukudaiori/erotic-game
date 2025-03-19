using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System.IO;

public class InkManager : MonoBehaviour
{
    public TextAsset inkJSONAsset; // Ink��JSON�t�@�C��
    private Story story;

    public Text dialogueText; // UI��Text
    public Button nextButton; // ���փ{�^��

    void Start()
    {
        // Ink�̃X�g�[���[�����[�h
        story = new Story(inkJSONAsset.text);
        DisplayNextLine();

        // �{�^���̃N���b�N�C�x���g��ݒ�
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    void DisplayNextLine()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue();
        }
        else
        {
            nextButton.gameObject.SetActive(false); // ���ꂪ�I��������{�^�����\��
        }
    }
}
