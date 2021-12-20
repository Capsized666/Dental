using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Consoleum;

/// <summary>
/// Класс запускающий консольные команды из скрипта
/// </summary>


    public class Anterpriner : MonoBehaviour
    {

        public static Anterpriner Instance { get; private set; }
        [SerializeField]
        private TextAsset startList;
        [SerializeField]
        private TextAsset todoList;

        private string[] listOfOperation;
        [SerializeField]
        public bool WS { get { return workingState; }  set { workingState = value; } }

        private bool workingState=false;
        
        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            readlist(0);
        }

        void Start()
        {
            
          
        }

        void FixedUpdate()
        {
            if (workingState) {
                if (listOfOperation[0]!=null)
                {
                    DeveloperConsole.Instance.ParseInput(listOfOperation[0]);
                    print(listOfOperation[0]);
                    var midlist = MakeFirstStep(listOfOperation);
                    listOfOperation = new string[midlist.Length];
                    listOfOperation = midlist;
                    return;
                }
            }
        }
     
        private string[] MakeFirstStep(string[] loo)
        {
            string[] output = new string[loo.Length];
            for (int i = 1; i < output.Length; i++)
            {
                output[i - 1] = loo[i];
            }
            return output;
        }

        private void readlist(int v)
        {
            if (v == 0)
            {
                listOfOperation = startList.text.Split('\n');
            }
            else
            {
                listOfOperation = todoList.text.Split('\n');
            }
        }
    }

                    
                


        



