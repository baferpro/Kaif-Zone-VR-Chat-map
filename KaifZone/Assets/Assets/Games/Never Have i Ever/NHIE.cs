/*
 * Never Have I Ever
 * Made by Child of the Beast
 * Version 1.4
 */
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.Udon.Common.Interfaces;

namespace UdonVR.Childofthebeast.NHIE
{
    public class NHIE : UdonSharpBehaviour
    {
        [SerializeField] private string[] QuestionsSFW = new string[] { "летал(а) на самолете", "хвастался(ась) чем-то, чего не делал", "водил(а) пьяным",
            "смеялся(ась) над кем-то", "видел(а), чтобы отставали от Кардашьян", "крал(а) что-то дороже 1000 рублей",
            "ни на что не ставил(а)", "делал(а) стойку на руках одной рукой", "преследовал(а) бувшую(его) девушку(парня) в социальных сетях",
            "лгал(а) другу, чтобы избежать большего зла", "сбегал(а) из класса", "хватал(а) чужого человека за руку",
            "влюблался(ась) в человека через соц. сети", "лгал(а) в этой игре", "говорил(а) 'Я люблю тебя', не чувствуя это", "был выгнан из бара",
            "лепил(а) жевачку под стол", "переставал(а) вспоминать свою первую любовь", "кусал(а) свой язык",
            "заходил(а) в ванную и не мыл(а) после этого руки", "имел(а) прикроватной тумбочки", "напивался(ась)", "ел(а) пищу с пола",
            "лажал(а) в школе", "дрался(ась) на улице", "засыпал(а) в автобусе и проезжал(а) мимо своей станции",
            "был(а) ограблен(а)", "был(а) влюблен(а) в своего учителя", "слышал(а) серенад", "пробирался(ась) на вечеринку", "застревал(а) в лифте",
            "был(а) за границей", "переправлял(а) что-либо через границу", "был(а) в наручниках",
            "делал(а) того, о чем сожалею", "засыпал(а) в кино", "ковырял(а) в носу публично", "участвовал(а) в шоу талантов"};
        [SerializeField] private string[] QuestionsNSFW = new string[] { "фоткался(ась) в нижнем белье", "делал(а) засосы",
            "посылал(а) случайно маме неподобающее смс-сообщение, которое было предназначено для моей(го) девушки(парня)",
            "целовал(а) своего лучшего друга", "загорал(а) частично или полностью голым(ой)", "спал(а) голым(ой)", "делал(а) это с незнакомцем",
            "слал(а) случайно неподобающее сообщение своему боссу", "имал(а) 'Друга с привилегиями'",
            "занимался(ась) сексом с кем-то из университета", "делал(а) это на море", "занимался(ась) сексом в спальном мешке",
            "лгал(а) о себе, чтобы перепихнуться", "делал(а) это в джакузи",
            "занимался(ась) сексом с кем-то старше меня на 10 и более лет", "носил(а) чужое нижнее белье", "целовал(а) незнакомца", "смотрел(а) порно",
            "был(а) в наручниках не из-за нарушения закона", "брил(а) лобковые волосы своего партнера",
            "был(а) пойман(а) на сексе", "наблюдал(а) за другом", "делал выстрелов в тело", "играл(а) со взбитыми сливками",
            "просил(а) кого-то прислать нюдсы", "был(а) в сексе втроём", "занимался(ась) сексом в первый день знакоства",
            "делал(а) это, пока член семьи был в этой же комнате", "был(а) заперт(а) голым на улице", "носил(а) одежду, чтобы спрятать засос",
            "играл(а) в ролевые игры", "был(а) пойман(а) за просмотром чего-то непослушного",
            "встречался(ась) с двумя людьми одновременно", "дрочил(а) на видео с YouTube", "заказывал(а) проститутку",
            "говорил(а) не то имя во время секса", "использовал(а) смазку", "фантазировал(а) о ком-то в комнате",
            "встречался(ась) с кем-то того же пола", "симулировал(а) оргазм", "сомневался(ась) в своей сексуальности", "был(а) в магазине для взрослых",
            "занимался(ась) сексом в ванной", "флиртовал(а) с учителем", "целовал(а) кого-то не зная его имени" };
        [UdonSynced] Vector3 SyncedVars = new Vector3(0f, 0f, 0f);
        private Vector3 _localVars;
        private float _Current;
        private float _GameType;
        private float _CurrentQuestion;

        [SerializeField] private Text questionBoard;
        [SerializeField] private GameObject circleobj;
        [SerializeField] private GameObject classicobj;
        [SerializeField] private GameObject selectMenu;
        [SerializeField] private GameObject gameMenu;
        [SerializeField] private Text questionSFWCountBoard;
        [SerializeField] private Text questionNSFWCountBoard;
        [SerializeField] private Text questionBOTHCountBoard;

        [Tooltip("This tells the game how many frames to wait until trying to update the menu with a new question.")]
        public int UpdateRate = 10;
        private int _Frame;
        [Tooltip("WARNING: This will spam console with debugging every time UpdateRate is ran.")]
        public bool _Debug = false;
        private void SyncData(bool _a)
        {
            if (_a == true)
            {
                SyncedVars.x = _Current;
                SyncedVars.y = _GameType;
                SyncedVars.z = _CurrentQuestion;
                _localVars = SyncedVars;
            }
            else
            {
                if (SyncedVars == _localVars) return;
                _Current = SyncedVars.x;
                _GameType = SyncedVars.y;
                _CurrentQuestion = SyncedVars.z;
                _localVars = SyncedVars;
            }
        }

        private void Start()
        {
            questionSFWCountBoard.text = QuestionsSFW.Length.ToString() + " Вопросов";
            questionNSFWCountBoard.text = QuestionsNSFW.Length.ToString() + " Вопросов";
            questionBOTHCountBoard.text = (QuestionsSFW.Length + QuestionsNSFW.Length).ToString() + " Вопросов";
        }

        private void Update()
        {
            if (_Frame >= UpdateRate)
            {
                if (_localVars != SyncedVars) SyncData(false);
                if (_Debug)
                {
                    Debug.Log("[UdonVR] SyncVars: " + SyncedVars.ToString());
                    Debug.Log("[UdonVR] LocalVars: " + SyncedVars.ToString());
                }

                if (_CurrentQuestion == 1)
                {
                    questionBoard.text = QuestionsSFW[(int)_Current];
                }
                else
                {
                    questionBoard.text = QuestionsNSFW[(int)_Current];
                }
                _Frame = 0;
            }
            else
            {
                _Frame++;
            }
        }
        /*
         * Setup
         */
        public void SetupSWF()
        {
            SendCustomNetworkEvent(NetworkEventTarget.Owner, "NetworkSetupSWF");
            NewQuestion();
        }
        public void SetupNSWF()
        {
            SendCustomNetworkEvent(NetworkEventTarget.Owner, "NetworkSetupNSWF");
            NewQuestion();
        }
        public void SetupBoth()
        {
            SendCustomNetworkEvent(NetworkEventTarget.Owner, "NetworkSetupBoth");
            NewQuestion();
        }
        public void NetworkSetupSWF()
        {
            _GameType = 1;
        }
        public void NetworkSetupNSWF()
        {
            _GameType = 2;
        }
        public void NetworkSetupBoth()
        {
            _GameType = 3;
        }

        public void Setup()
        {
            SendCustomNetworkEvent(NetworkEventTarget.All, "NetworkSetup");
        }
        public void NetworkSetup()
        {
            selectMenu.SetActive(false);
            gameMenu.SetActive(true);
        }
        public void UnSetup()
        {
            SendCustomNetworkEvent(NetworkEventTarget.All, "NetworkUnSetup");
        }
        public void NetworkUnSetup()
        {
            selectMenu.SetActive(true);
            gameMenu.SetActive(false);
        }
        /*
        *
        */
        public void NewQuestion()
        {
            SendCustomNetworkEvent(NetworkEventTarget.All, "NetworkSetup");
            SendCustomNetworkEvent(NetworkEventTarget.Owner, "NetworkNewQuestion");
        }
        public void NetworkNewQuestion()
        {
            switch (_GameType)
            {
                case 1:
                    PullSFW();
                    break;
                case 2:
                    PullNSFW();
                    break;
                case 3:
                    if (Random.Range(0, 2) == 0)
                    {
                        PullSFW();
                    }
                    else
                    {
                        PullNSFW();
                    }
                    break;
            }
            SyncData(true);
        }

        private void PullSFW()
        {
            _CurrentQuestion = 1;
            _Current = Random.Range(0, QuestionsSFW.Length);
        }
        private void PullNSFW()
        {
            _CurrentQuestion = 2;
            _Current = Random.Range(0, QuestionsNSFW.Length);
        }

        public void CircleTrigger()
        {

            if (Networking.LocalPlayer.isMaster)
            {
                SendCustomNetworkEvent(NetworkEventTarget.All, "Circle");
            }
        }
        public void ClassicTrigger()
        {
            if (Networking.LocalPlayer.isMaster)
            {
                SendCustomNetworkEvent(NetworkEventTarget.All, "Classic");
            }
        }
        public void Circle()
        {
            circleobj.SetActive(true);
            classicobj.SetActive(false);
        }
        public void Classic()
        {
            circleobj.SetActive(false);
            classicobj.SetActive(true);
        }
    }
}