using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

class XEHolder : XElement
{
    ///<override> parent functions
    public XEHolder(XElement other) : base(other) { }
    public XEHolder(XName name) : base(name) { }
#nullable enable
    public XEHolder(XName name, object? content) : base(name, content) { }
    public XEHolder(XName name, params object?[] content) : base(name, content) { }
#nullable disable
    public XEHolder(XStreamingElement other) : base(other) { }
    new public IEnumerable<XEHolder> Elements()
    {
        return base.Elements().Cast<XEHolder>();
    }
    new public XEHolder Element(XName name)
    {
        return (XEHolder)base.Element(name);
    }
    ///</override>

    /// <summary> search the XElement and return value of the FIRST described attribute under the element tree </summary>
    /// <param name="name">name of the attribute</param>
    /// <returns>value of the attribute as string</returns>
    public string SAttribute(string name)
    {
        if (this.Attribute(name) != null)
        {
            return this.Attribute(name).Value;
        }
        else
        {
            foreach (XEHolder xel in this.Elements())
            {
                string output = xel.SAttribute(name);
                if (output != null) return output;
            }
        }
        return null;
    }
    /// <returns>value of the attribute as float</returns>
    public float FAttribute(string name)
    {
        return float.Parse(SAttribute(name));
    }
    /// <returns>value of the attribute as integer</returns>
    public int IAttribute(string name)
    {
        return int.Parse(SAttribute(name));
    }
    /// <returns>value of the attribute as boolean, where 0 = false, everything else = true</returns>
    public bool BAttribute(string name)
    {
        string str = SAttribute(name);
        if (str == "0") return false;
        else return true;
    }
}