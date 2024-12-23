using Microsoft.Xna.Framework;

namespace CitySimGame.RoadNetwork
{
    public class Road
    {
        public Intersection Start { get; }
        public Intersection End { get; }

        public Road(Intersection start, Intersection end)
        {
            Start = start;
            End = end;
        }
    }
} 