using AssesmentRoverCardano.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentRoverCardano
{
    public class Rover
    {
        public Point coordinates { get; set; }
        public OrientationEnum currentDir { get; set; }
        public string Command { get; set; }

        public bool ErrorFlag { get; set; }

        public Rover(string command, int initX = 0, int initY = 0, OrientationEnum initOrientation = OrientationEnum.N,  bool errorFlag = false)
        {
            coordinates = new Point(initX, initY);
            Command = command;
            currentDir = initOrientation;
            ErrorFlag = errorFlag;
        }

        public bool Move(Plateu plateu)
        {
            bool res = false;

            switch (currentDir)
            {
                case OrientationEnum.N:
                    res = incrementY(plateu);
                    break;

                case OrientationEnum.S:
                    res = decrementY(plateu);
                    break;

                case OrientationEnum.E:
                    res = incrementX(plateu);
                    break;

                case OrientationEnum.W:
                    res = decrementX(plateu);
                    break;
            }

            return res;
        }

        private bool incrementX(Plateu plateu)
        {
            Point newCoordinates = new Point(coordinates.X + 1, coordinates.Y);

            if (plateu.OccupiedCoordinates.Contains(newCoordinates))
            {
                Console.WriteLine("Position not changed. Another Rover at: (" + newCoordinates.X + ", " + newCoordinates.Y + ")");
                return false;
            }

            int newX = coordinates.X + 1;
            if (newX > plateu.MaxWidth)
            {
                newX = plateu.MaxWidth;
                ErrorFlag = true;
            }

            coordinates = new Point(newX, coordinates.Y);
            return true;
        }

        private bool decrementX(Plateu plateu)
        {
            Point newCoordinates = new Point(coordinates.X - 1, coordinates.Y);

            if (plateu.OccupiedCoordinates.Contains(newCoordinates))
            {
                Console.WriteLine("Position not changed. Another Rover at: (" + newCoordinates.X + ", " + newCoordinates.Y + ")");
                return false;
            }

            int newX = coordinates.X - 1;

            if (newX < 0)
            {
                newX = 0;
                ErrorFlag = true;
            }

            coordinates = new Point(newX, coordinates.Y);
            return true;
        }

        private bool incrementY(Plateu plateu)
        {
            Point newCoordinates = new Point(coordinates.X, coordinates.Y + 1);

            if (plateu.OccupiedCoordinates.Contains(newCoordinates))
            {
                Console.WriteLine("Position not changed. Another Rover at: (" + newCoordinates.X + ", " + newCoordinates.Y + ")");
                return false;
            }

            int newY = coordinates.Y + 1;

            if (newY > plateu.MaxHeight)
            {
                newY = plateu.MaxHeight;
                ErrorFlag = true;
            }

            coordinates = new Point(coordinates.X, newY);
            return true;
        }

        private bool decrementY(Plateu plateu)
        {
            Point newCoordinates = new Point(coordinates.X, coordinates.Y - 1);

            if (plateu.OccupiedCoordinates.Contains(newCoordinates))
            {
                Console.WriteLine("Position not changed. Another Rover at: (" + newCoordinates.X + ", " + newCoordinates.Y + ")");
                return false;
            }
            int newY = coordinates.Y - 1;

            if (newY < 0)
            {
                newY = 0;
                ErrorFlag = true;
            }

            coordinates = new Point(coordinates.X, newY);
            return true;
        }

        public override string ToString()
        {
            StringBuilder positionRover = new StringBuilder(string.Format("{0} {1} {2}", coordinates.X, coordinates.Y, currentDir.ToString()));
            if (ErrorFlag)
            {
                positionRover.AppendLine(string.Format("Rover needs assistance at his postion."));
            }

            return positionRover.ToString();

        }

        public void TurnLeft(OrientationEnum orientation)
        {
            switch (orientation)
            {
                case OrientationEnum.N:
                    orientation = OrientationEnum.W;
                    break;

                case OrientationEnum.S:
                    orientation = OrientationEnum.E;
                    break;

                case OrientationEnum.E:
                    orientation = OrientationEnum.N;
                    break;

                case OrientationEnum.W:
                    orientation = OrientationEnum.S;
                    break;
            }
        }

        public void TurnRight(OrientationEnum orientation)
        {
            switch (orientation)
            {
                case OrientationEnum.N:
                    orientation = OrientationEnum.E;
                    break;

                case OrientationEnum.S:
                    orientation = OrientationEnum.W;
                    break;

                case OrientationEnum.E:
                    orientation = OrientationEnum.S;
                    break;

                case OrientationEnum.W:
                    orientation = OrientationEnum.N;
                    break;
            }
        }
    }
}
