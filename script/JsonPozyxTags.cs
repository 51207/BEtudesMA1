public class JsonPozyxTags
    {

        public class AnchorData
        {
            public double rss { get; set; }
            public string anchorId { get; set; }
            public string tagId { get; set; }
        }

        public class Sensor
        {
            public string name { get; set; }
            public object value { get; set; }
        }

        public class TagData
        {
            public int blinkIndex { get; set; }
            public IList<Sensor> sensors { get; set; }
        }

        public class Coordinates
        {
            public int z { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }

        public class Rates
        {
            public double success { get; set; }
            public double packetLoss { get; set; }
            public double update { get; set; }
        }

        public class Metrics
        {
            public int latency { get; set; }
            public Rates rates { get; set; }
        }

        public class Extras
        {
            public string version { get; set; }
            public IList<object> zones { get; set; }
        }

        public class Data
        {
            public IList<AnchorData> anchorData { get; set; }
            public TagData tagData { get; set; }
            public Coordinates coordinates { get; set; }
            public Metrics metrics { get; set; }
            public int coordinatesType { get; set; }
            public Extras extras { get; set; }
        }

        public class Example
        {
            public bool success { get; set; }
            public Data data { get; set; }
            public double timestamp { get; set; }
            public string tagId { get; set; }
            public string version { get; set; }
        }

    }