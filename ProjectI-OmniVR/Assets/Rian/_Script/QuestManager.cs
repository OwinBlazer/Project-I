using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {
    public GameObject PanelQuest;
    public GameObject continueButton;
    public GameObject decisionButton;
    public GameObject declineButton;

    public Text questDialogueTxt;
    public Text nameNpcTxt;
    public Text rewardTxt;

    private Queue<string> questSentences;
    public Quest questing;
    public PlayerSO playerSO;
    public bool isReward;
    public bool isConfirm;
    public bool isLast;

    private void Start()
    {
        questSentences = new Queue<string>();
    }

    public void StartDialogue(Quest quest)
    {
        nameNpcTxt.text = quest.npcName;
        questSentences.Clear();
        declineButton.SetActive(false);
        decisionButton.SetActive(false);
        continueButton.SetActive(true);
        PanelQuest.SetActive(true);
        isLast = false;

        foreach (string sentence in quest.questDialogue)
        {
            questSentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DefaultDialogue(Quest quest)
    {
        nameNpcTxt.text = quest.npcName;
        continueButton.SetActive(false);
        decisionButton.SetActive(false);
        declineButton.SetActive(true);
        PanelQuest.SetActive(true);
        questSentences.Clear();

        questSentences.Enqueue(quest.defaultDialogue);

        string questSentence = questSentences.Dequeue();
        questDialogueTxt.text = questSentence;
        StartCoroutine(TypeSentence(questSentence));

    }

    public void RewardDialogue(Quest quest)
    {
        isReward = true;
        nameNpcTxt.text = quest.npcName;
        continueButton.SetActive(true);
        decisionButton.SetActive(false);
        PanelQuest.SetActive(true);
        rewardTxt.gameObject.SetActive(true);
        rewardTxt.text = "Reward : " + questing.goldReward.ToString() + " G";
        questSentences.Clear();
        isLast = false;

        //questSentences.Enqueue(quest.rewardDialogue);
        foreach (string sentence in quest.rewardDialogue)
        {
            questSentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void ConfirmDeclineDialogue()
    {
        continueButton.SetActive(false);
        decisionButton.SetActive(false);
        declineButton.SetActive(true);
        PanelQuest.SetActive(true);
        questSentences.Clear();

        if (isConfirm == true)
        {
            questSentences.Enqueue(questing.confirmDialogue);
        }
        else
        {
            questSentences.Enqueue(questing.declineDialogue);
        }

        string questSentence = questSentences.Dequeue();
        questDialogueTxt.text = questSentence;
        StartCoroutine(TypeSentence(questSentence));
    }

    public void DisplayNextSentence()
    {
        continueButton.SetActive(false);

        if (isReward == false)
        {
            if (questSentences.Count == 1)
            {
                DecisionDialogue();
                isLast = true;
            }
        }
        else if (isReward == true)
        {
            if (questSentences.Count == 1)
            {
                declineButton.SetActive(true);
                isLast = true;
            }
        }

        if (questSentences.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        string questSentence = questSentences.Dequeue();
        questDialogueTxt.text = questSentence;
        StartCoroutine(TypeSentence(questSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        questDialogueTxt.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            questDialogueTxt.text += letter;
            yield return null;
        }
        if (isLast == false)
            continueButton.SetActive(true);
    }

    void DecisionDialogue()
    {
        declineButton.SetActive(false);
        continueButton.SetActive(false);
        decisionButton.SetActive(true);
    }

    void EndOfDialogue()
    {
        Debug.Log("End Of Dialogue");
    }

    public void Decline()
    {
        isReward = false;
        isConfirm = false;
        if (questing.questStatus == 2)
        {
            RewardConfirm();
        }
        PanelQuest.SetActive(false);
    }

    public void ConfirmQuest()
    {
        if (playerSO.priorityQuest == null)
        {
            if (questing.priority == true)
            {
                playerSO.priorityQuest = questing;
            }
            else
            {
                playerSO.activQuest.Add(questing);
            }
            questing.questStatus = 1;
            isConfirm = true;
            ConfirmDeclineDialogue();
        }
        else
        {
            Decline();
        }
    }

    public void RewardConfirm()
    {
        questing.questStatus = 3;
        Decline();
        rewardTxt.gameObject.SetActive(false);
        playerSO.activQuest.Remove(questing);
    }

}

