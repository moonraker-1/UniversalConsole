using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalConsole.InputProcessor
{
    internal struct InputData
    {
        public string[]? InputWords { get; set; }
        public IInputStandards.Standards Standard { get; set; }

    }

    internal class Input : IInputStandards
    {
        /// <summary>
        /// Handles input processing
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Input words and input standard</returns>
        public InputData Take(string input = "")
        {
            try
            {
                string[] inpArr = input.Split(' ');
                inpArr = inpArr.Where(i => !string.IsNullOrEmpty(i)).ToArray();
                

                IInputStandards.Standards inputType = defineInput(inpArr);

                return new InputData
                {
                    InputWords = inpArr,
                    Standard = inputType
                };

            }
            catch
            {
                return new InputData
                {
                    InputWords = null,
                    Standard = IInputStandards.Standards.Invalid
                };
            }
        }

        /// <summary>
        /// Returns corresponding input standard for the specified input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private IInputStandards.Standards defineInput(string[] input)
        {
            switch (input.Length)
            {
                case 1:
                    return IInputStandards.Standards.Comm;
                case 2:
                    return IInputStandards.Standards.CommObj;
                case 3:
                    return IInputStandards.Standards.CommPropObj;
                case 4:
                    return IInputStandards.Standards.CommPropObj1Obj2;
                default:
                    return IInputStandards.Standards.Invalid;
            }
        }
    }
}
