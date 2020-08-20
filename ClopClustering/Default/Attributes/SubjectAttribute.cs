namespace ClopClustering.Default.Attributes
{
    public readonly struct SubjectAttribute
    {
        public SubjectAttribute(string value, float weight = 0.33f, bool isKey = false)
          => (Key, Weight, IsKeyAttribute) = (value, weight, isKey);  
        public bool IsKeyAttribute { get;  }

        public float Weight { get;  }

        public string Key { get; }

        public override string ToString() => Key;
    }
}
