using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GameConsole.CommandTools
{
    public class MethodParser
    {
        private delegate bool ParseFunction(string input, out object output);
        private Dictionary<Type, ParseFunction> _parseFunctions = new();

        public MethodParser()
        {
            _parseFunctions = new Dictionary<Type, ParseFunction>()
        {
            {typeof(string),ParseToString },
            {typeof(bool), ParseToBool },
            {typeof(int), ParseToInt },
            {typeof(float),ParseToFloat },
            {typeof(double),ParseToDouble },
            {typeof(Vector2),ParseToVector2 },
            {typeof(Vector3),ParseToVector3 },
        };
        }

        public object[] ParseParameters(MethodInfo method, string parameters)
        {
            var methodParameters = method.GetParameters();
            var stringParameters = parameters.Split(' ', ';').Where(str => str.Length > 0).ToArray();

            bool success = true;
            object[] result = new object[methodParameters.Length];

            for (int i = 0; i < methodParameters.Length; i++)
                if (ParseParameter(methodParameters[i], stringParameters[i], out result[i]) == false)
                {
                    success = false;
                    break;
                }

            if (success)
            {

            }
            else
            {

            }

            return result;
        }

        private bool ParseParameter(ParameterInfo parameter, string stringParameter, out object output)
        {
            output = null;

            if (_parseFunctions.ContainsKey(parameter.ParameterType))
            {
                if (_parseFunctions[parameter.ParameterType].Invoke(stringParameter, out output) == false)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool ParseToString(string input, out object output)
        {
            output = input;
            return true;
        }

        private bool ParseToInt(string input, out object output)
        {
            var result = int.TryParse(input, out int parseResult);
            output = parseResult;
            return result;
        }

        private bool ParseToFloat(string input, out object output)
        {
            var result = float.TryParse(input, out float parseResult);

            output = parseResult;

            return result;
        }

        private bool ParseToDouble(string input, out object output)
        {
            var result = double.TryParse(input, out double parseResult);

            output = parseResult;

            return result;
        }

        private bool ParseToBool(string input, out object output)
        {
            bool result;

            if (input.ToLower() == "true" || input == "1")
            {
                result = true;

                output = true;
            }
            else if (input.ToLower() == "false" || input == "0")
            {
                result = true;

                output = false;
            }
            else
            {
                result = false;

                output = null;
            }

            return result;
        }

        private bool ParseToVector2(string input, out object output)
        {
            output = null;
            var axes = input.Split(',');

            if (axes.Length < 2)
                return false;

            object x, y = null;

            if (ParseToFloat(axes[0], out x) == false)
                return false;
            else
                if (ParseToFloat(axes[1], out y) == false)
                return false;


            output = new Vector2((float)x, (float)y);

            return true;
        }

        private bool ParseToVector3(string input, out object output)
        {
            output = null;
            var axes = input.Split(',');

            if (axes.Length < 3)
                return false;

            if (ParseToVector2(input, out output) == false)
                return false;

            var vec2 = (Vector2)output;

            if (ParseToFloat(axes[2], out object z) == false)
                return false;

            output = new Vector3(vec2.x, vec2.y, (float)z);

            return true;
        }
    }
}
