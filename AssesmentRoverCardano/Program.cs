using AssesmentRoverCardano.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AssesmentRoverCardano
{
    public class Program
    {

        public static void Main(string[] args)
        {
            List<string> commands = new List<string>();
            Console.WriteLine("Enter input (type END to stop):");
            string singleCommand = string.Empty;

            do
            {
                singleCommand = Console.ReadLine();

                if (singleCommand.ToUpper() != "END")
                    commands.Add(singleCommand.ToUpper());
                else
                    break;

            } while (true);

            List<Rover> listRovers = null;
            Plateu plateu = null;
            bool commandValid = CommandValidator(commands, out listRovers, out plateu);


        }

        public static bool CommandValidator(List<string> listToValidate, out List<Rover> listRovers, out Plateu plateu)
        {
            Regex regexRoverCoordinates = new Regex(@"^[0-9]{1}\s[0-9]{1}\s[NWSE]{1}$");
            Regex regexRoverCommand = new Regex(@"^[LMR]*$");
            Regex regexPlateuSize = new Regex(@"^[0-9]{1}\s[0-9]{1}$");

            listRovers = new List<Rover>();

            if (listToValidate.Count < 3 || listToValidate.Count % 2 == 0)
            {
                Console.WriteLine("Invalid number of commands - " + listToValidate.Count);
                Console.ReadKey();
                listRovers = null;
                plateu = null;
                return false;
            }
            else
            {
                if (!regexPlateuSize.Match(listToValidate[0]).Success)
                {
                    Console.WriteLine("Invalid plateu dimensions. Press key to terminate");
                    Console.ReadKey();
                    listRovers = null;
                    plateu = null;
                    return false;
                }

                plateu = new Plateu(Convert.ToInt32(listToValidate[0].Substring(0, 1)), Convert.ToInt32(listToValidate[0].Substring(2, 1)));

                for (int i = 1; i < listToValidate.Count; i++)
                {
                    Console.WriteLine("i: " + i.ToString());

                    if (!regexRoverCoordinates.Match(listToValidate[i]).Success)
                    {
                        Console.WriteLine("Invalid rover position. Press key to terminate");
                        Console.ReadKey();
                        listRovers = null;
                        plateu = null;
                        return false;
                    }

                    if (!regexRoverCommand.Match(listToValidate[i + 1]).Success)
                    {
                        Console.WriteLine("Invalid rover command. Press key to terminate");
                        Console.ReadKey();
                        listRovers = null;
                        plateu = null;
                        return false;
                    }

                    if (!plateu.OccupiedCoordinates.Contains(new Point(Convert.ToInt32(listToValidate[i].Substring(0, 1)), Convert.ToInt32(listToValidate[i].Substring(2, 1)))))
                    {
                        OrientationEnum c = (OrientationEnum)Enum.Parse(typeof(OrientationEnum), listToValidate[i].Substring(4, 1), true);

                        Rover temp = new Rover(listToValidate[i + 1], Convert.ToInt32(listToValidate[i].Substring(0, 1)), Convert.ToInt32(listToValidate[i].Substring(2, 1)));
                        plateu.OccupiedCoordinates.Add(new Point(Convert.ToInt32(listToValidate[i].Substring(0, 1)), Convert.ToInt32(listToValidate[i].Substring(2, 1))));

                        listRovers.Add(temp);
                    }
                    i++;
                }
            }

            return true;
        }

    }
}
