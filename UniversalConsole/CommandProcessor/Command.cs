using Black_Tool_Kit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniversalConsole.InputProcessor;


namespace UniversalConsole.CommandProcessor
{
    internal class Command
    {

        /// <summary>
        /// 
        /// </summary>
        public bool Process(InputData inputData)
        {
            bool processed = false;
            switch (inputData.Standard)
            {
                case IInputStandards.Standards.Invalid:

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Syntax. Use 'help' for more details about types of commands");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case IInputStandards.Standards.Key:
                    processed = processKey(inputData);
                    break;
                case IInputStandards.Standards.KeyObj:
                    processed = processKeyObj(inputData);
                    break;
                case IInputStandards.Standards.KeyPropObj:
                    processed = processKeyPropObj(inputData);
                    break;
                case IInputStandards.Standards.KeyPropObj1Obj2:
                    processed = processKeyPropObj1Obj2(inputData);
                    break;

            }
            return processed;
        }

        /// <summary>
        /// Sends request to execute a one-word command.
        /// </summary>
        /// <param name="iData"></param>
        /// <returns></returns>
        private bool processKey(InputData iData)
        {

            if(Enum.TryParse(iData.InputWords[0].ToUpper(), out IKeyWords.Keys key))
            {
                return KeywordExecutable.Execute(key);
            }
            return false;
        }

        /// <summary>
        /// Send 
        /// </summary>
        /// <param name="iData"></param>
        /// <returns></returns>
        private bool processKeyObj(InputData iData)
        {
            if (Enum.TryParse(iData.InputWords[0].ToUpper(), out IKeyWords.Keys key) && iData.InputWords.Length == 2)
            {
                return KeywordObjectExecutable.Execute(key, iData.InputWords[1]);
            }
            return true;
        }

        private bool processKeyPropObj(InputData iData)
        {
            return false;
        }

        private bool processKeyPropObj1Obj2(InputData iData)
        {
            return false;
        }

        private bool keyExists(string k)
        {
            return Enum.TryParse(k, out IKeyWords.Keys key);
        }

    }
}
