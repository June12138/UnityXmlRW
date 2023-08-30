using System.Xml.Linq;
using UnityEngine;

//this template saves all positional information but no rotational information
public class XYZPositionTemplate : RuntimeXmlLoader
{
    public override void SaveEntry(XmlAnnotation entry)
    {
        //editing xml elements of a character
        XEHolder el =
        new XEHolder("character", new XAttribute("id", entry.key),
            new XEHolder("position")
        );
        el.Element("position").SetAttributeValue("x", entry.transform.localPosition.x);
        el.Element("position").SetAttributeValue("y", entry.transform.localPosition.y);
        el.Element("position").SetAttributeValue("z", entry.transform.localPosition.z);
        xmlHolder.xmlRoot.Add(el);
    }
    public override void LoadEntry(XmlAnnotation entry)
    {
        XEHolder position = xmlHolder.FindFirst("id", entry.key);
        float x = position.FAttribute("x");
        float y = position.FAttribute("y");
        float z = position.FAttribute("z");
        entry.transform.localPosition = new Vector3(x, y, z);
    }
}
