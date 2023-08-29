using System.Xml.Linq;
using UnityEngine;

//this template saves all positional information but no rotational information
public class Template1 : RuntimeXmlLoader
{
    public override void SaveEntry(GameObject gameObject)
    {
        //editing xml elements of a character
        XEHolder el =
        new XEHolder("character", new XAttribute("id", gameObject.name),
            new XEHolder("transform",
                new XEHolder("position"),
                new XEHolder("rotation")
            )
        );
        el.Element("transform").Element("position").SetAttributeValue("x", gameObject.transform.localPosition.x);
        el.Element("transform").Element("position").SetAttributeValue("y", gameObject.transform.localPosition.y);
        el.Element("transform").Element("position").SetAttributeValue("z", gameObject.transform.localPosition.z);
        xmlHolder.xmlRoot.Add(el);
    }
    public override void LoadEntry(GameObject gameObject)
    {
        XEHolder position = xmlHolder.FindFirst("id", gameObject.name).Element("transform").Element("position");
        float x = position.FAttribute("x");
        float y = position.FAttribute("y");
        float z = position.FAttribute("z");
        gameObject.transform.localPosition = new Vector3(x, y, z);
    }
}
