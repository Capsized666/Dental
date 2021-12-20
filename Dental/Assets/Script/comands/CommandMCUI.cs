
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Consoleum
{
    public class CommandMCUI : ConsoleCommand
    {
        public override string Name { get; protected set; }
        public override string Command { get; protected set; }
        public override string Description { get; protected set; }
        public override string Help { get; protected set; }

        public CommandMCUI()
        {
            Name = "MCUI";
            Command = "MCUI";
            Description = "Work with MCUI";
            Help = "no arguments";

            AddCommandToConsole();
        }

        public override void RunCommand(string[] args)
        {
            if (args.Length == 1 && args[0] == "dim")
            {
                
                DeveloperConsole.Instance.AddMessageToConsole(
                    $"size:{MCUI.Instance.getCanvasSize().x},{MCUI.Instance.getCanvasSize().y}"
                ) ; 
                return;
            }
            if (args.Length == 2)
            {
                if (args[1] == "dim")
                {
                    foreach (RectTransform item in MCUI.Instance.transform)
                    {
                        if (item.name == args[0]) { 
                            DeveloperConsole.Instance.AddMessageToConsole(
                                $"{item.name} size:{item.sizeDelta.x}:{item.sizeDelta.y}"
                            );
                            return;
                        }
                    }
                }
                if (args[1] == "dis")
                {
                    foreach (RectTransform item in MCUI.Instance.transform)
                    {
                        if (item.name == args[0])
                        {
                            item.gameObject.SetActive(!item.gameObject.activeInHierarchy);

                            DeveloperConsole.Instance.AddMessageToConsole(
                                $"{item.name} active is:{item.gameObject.activeInHierarchy}"
                            );
                            return;
                        }
                    }
                }
            }
            if (args.Length == 4)
            {

                if (args[1] == "setDim")
                {
                    foreach (RectTransform item in MCUI.Instance.transform)
                    {
                        if (item.name == args[0])
                        {
                            var x = float.Parse(args[2]);
                            var y = float.Parse(args[3]);
                            item.sizeDelta = new Vector2 (x,y);

                            DeveloperConsole.Instance.AddMessageToConsole(
                                $"{item.name} size changet to:{item.sizeDelta.x}:{item.sizeDelta.y}"
                            );
                            return;
                        }
                    }
                }
            }


        }


        public static CommandMCUI CreateCommand()
        {
            return new CommandMCUI();
        }
    }
}
