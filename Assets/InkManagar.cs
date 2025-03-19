using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System.IO;

public class InkManager : MonoBehaviour
{
    public TextAsset inkJSONAsset; // InkのJSONファイル
    private Story story;

    public Text dialogueText; // UIのText
    public Button nextButton; // 次へボタン

    void Start()
    {
        // Inkのストーリーをロード
        story = new Story(inkJSONAsset.text);
        DisplayNextLine();

        // ボタンのクリックイベントを設定
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
            nextButton.gameObject.SetActive(false); // 物語が終了したらボタンを非表示
        }
    }
}
