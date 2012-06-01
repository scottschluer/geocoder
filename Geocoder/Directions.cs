using System.Collections.Generic;

namespace Geocoder
{
    public class Directions
    {
        public Directions()
        {
            Steps = new List<DirectionStep>();
        }

        public string Duration { get; set; }
        public string Distance { get; set; }
        public List<DirectionStep> Steps { get; set; }
    }
}
