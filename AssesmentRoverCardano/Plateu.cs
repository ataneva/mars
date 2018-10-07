using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentRoverCardano
{
    public class Plateu
    {
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public List<Point> OccupiedCoordinates { get; set; }

        public Plateu(int x, int y)
        {
            MaxWidth = x;
            MaxHeight = y;
        }

        public bool IsCoordinateFree(Point location)
        {
            foreach (var item in OccupiedCoordinates)
            {
                if (item.X == location.X && item.Y == location.Y)
                    return false;
            }

            return true;
        }

        public bool IsOutOfBoundaries(Point point)
        {
            if (MaxWidth >= point.X || MaxHeight >= point.Y)
            {
                return true;
            }

            return false;
        }
    }
}
