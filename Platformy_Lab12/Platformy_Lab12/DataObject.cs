using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class DataObject
{
    public string Name { get; set; }
    public int Value { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Value: {Value}";
    }
}
