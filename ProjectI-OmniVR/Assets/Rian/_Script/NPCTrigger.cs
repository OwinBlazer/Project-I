using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class NPCTrigger : MonoBehaviour
    {

        public Quest[] quest;
        public QuestManager questManager;

        public GameObject panelQuest;
        public int i;
        public bool questNotAvailable; //buat player yang sudah ambil quest tapi belom diselesain
        public bool questNotConfirm; //buat player yang sudah menyelesaikan quest tetapi belom report
        public PlayerSO playerSO;
        public Transform canvasLocation;
        public bool questActivated;

        public void Start()
        {
            i = 0;
            questManager = FindObjectOfType<QuestManager>();
            CheckQuest();
        }

        public void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TriggeredObject();
            }
        }

        public void TriggeredObject()
        {
        if (questActivated == true)
        {
            panelQuest.transform.position = canvasLocation.position;
            CheckQuest();
            if (questNotAvailable == false && questNotConfirm == false)
            {
                DialogueTrigger();
            }
            else if (questNotAvailable == true && questNotConfirm == false)
            {
                questManager.DefaultDialogue(quest[i]);
            }
            else if (questNotConfirm == true)
            {
                questManager.RewardDialogue(quest[i]);
            }
        }
        }

        public void DialogueTrigger()
        {
            questManager.StartDialogue(quest[i]);
            questManager.questing = quest[i];
        }

        public void CheckQuest()
        {
            int a = 0;
            foreach (Quest s in quest)
            {
                if (s.questStatus == 3)
                {
                    a++;
                    questNotAvailable = false;
                    questNotConfirm = false;
                }
                if (s.questStatus == 1)
                {
                    questNotAvailable = true;
                }
                if (s.questStatus == 2)
                {
                    questNotConfirm = true;
                }
            }
            i = a;
        }

        void ResetAllQuest()
        {
            foreach (Quest r in quest)
            {
                r.questStatus = 0;
            }
            questNotAvailable = false;
            playerSO.activQuest = new List<Quest>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                ResetAllQuest();
        }

    }
