using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс консоли
/// </summary>


namespace Consoleum
{

    public abstract class ConsoleCommand { 
        public abstract string Name { get; protected set; }
        public abstract string Command { get; protected set; }
        public abstract string Description { get; protected set; }
        public abstract string Help { get; protected set; }

        public void AddCommandToConsole() {
            string addMessage = " command has been added to console.";
            DeveloperConsole.AddCommandToconsole(Command,this);
            Debug.Log(Name+addMessage);

        }

        public abstract void RunCommand(string [] args);
    }


    public class DeveloperConsole : MonoBehaviour
    {

        public static DeveloperConsole Instance { get; private set; }
        public static Dictionary<string, ConsoleCommand> Commands { get; private set; }
        [Header("UI Components")]
        public Canvas consolCanvas;
        public ScrollRect scrollRect;
        public Text consoleText;
        public Text inputText;
        public InputField consoleInput;
        private List<string> InputComands = new List<string>();
        private int commandLine;


        public float scaleTime;

        private void HandleLog(string  logMessage,string trackrace,LogType type) {
            var _message = $"[{type.ToString()}] {logMessage}";
            AddMessageToConsole(_message);

        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        public void ClearConsole() {
            consoleText.text = "";
        }

        private void CreateCommands() {
            CommandPause.CreateCommand();
            CommandQuit.CreateCommand();
            CommandSpawn.CreateCommand();
            CommandClear.CreateCommand();
            CommandHelp.CreateCommand();
            CommandPrintAllObj.CreateCommand();
            CommandUISpawn.CreateCommand();
            CommandUIParenting.CreateCommand();
            CommandSky.CreateCommand();
            CommandPlayOn.CreateCommand();
            CommandMCUI.CreateCommand();
            CommandDisplay.CreateCommand();
            //ParseInput("play");
        }

        public static void AddCommandToconsole(string _name,ConsoleCommand _command) {
            if (!Commands.ContainsKey(_name))
            {
                Commands.Add(_name, _command);
            }
        }
        
        public void AddMessageToConsole(string msg) {
            consoleText.text += msg + '\n';
            scrollRect.verticalNormalizedPosition = 0f;
        }
        public void ParseInput(string s) {

            string[] _input = s.Split(' ');
            
            if (_input.Length==0 )
            {
                Debug.LogWarning("Command not recognized.");
                return;
            }

            if (!Commands.ContainsKey(_input[0]))
            {

                Debug.LogWarning($"{_input[0]} Command not recognized.");
            }
            else {//esli komanda ecny vo vvode

                List<string> args = _input.ToList();

                args.RemoveAt(0);
                //vizov pomoshi po knopke
                if (args.Contains("/help")) {
                    AddMessageToConsole("<==============================>");
                    AddMessageToConsole(Commands[_input[0]].Description);
                    AddMessageToConsole("<------------------------------>");
                    AddMessageToConsole(Commands[_input[0]].Help);
                    AddMessageToConsole("<==============================>");
                    return;
                }

                Commands[_input[0]].RunCommand(args.ToArray());

            }
              

        }
    
        private void Awake() {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            Commands = new Dictionary<string, ConsoleCommand>();
            DontDestroyOnLoad(this.gameObject);
            CreateCommands();
            commandLine = 0;
        }
            
        
        
        void Start()
    {
            consolCanvas.gameObject.SetActive(false);
    }
        
        void Update()
    {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                consolCanvas.gameObject.SetActive(!consolCanvas.gameObject.activeInHierarchy);
                if (consolCanvas.gameObject.activeInHierarchy)
                {
                    consoleInput.text = "";
                    consoleInput.ActivateInputField();
                    scaleTime = Time.timeScale;
                    Time.timeScale = 0;
                }
                else {
                    Time.timeScale = scaleTime;
                }
            }

            if (consolCanvas.gameObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (inputText.text != "") {
                        AddMessageToConsole(inputText.text);
                        ParseInput(inputText.text);
                        InputComands.Add(inputText.text);
                        consoleInput.text = "";
                        consoleInput.ActivateInputField();
                    }
                }
            }
            if (InputComands.Count>10) {
                InputComands.RemoveAt(0);
            }

            if (InputComands.Count>0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    consoleInput.text = InputComands[commandLine].ToString();
                    //inputText.text = "rty";
                    consoleInput.ActivateInputField();
                    commandLine--;
                    if (commandLine < 0) {
                        commandLine = InputComands.Count()-1;
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    consoleInput.text = InputComands[commandLine];
                    //inputText.text = "rty3";
                    consoleInput.ActivateInputField();
                    commandLine++;
                    if (commandLine > InputComands.Count()-1)
                    {
                        commandLine = 0;
                    }
                }
            }


        }



    


        //public static void AddStaticMessageToConsole(string msg) { 
        //    DeveloperConsole.Instance.consoleText.text += msg + '\n';
        //    DeveloperConsole.Instance.scrollRect.verticalNormalizedPosition = 0f;
        //}

}

}







