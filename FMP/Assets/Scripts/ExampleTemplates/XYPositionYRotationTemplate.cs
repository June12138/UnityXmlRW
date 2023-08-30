using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

//this template saves all positional information but no rotational information
public class XYPositionYRotationTemplate : RuntimeXmlLoader
{
    public override void SaveEntry(XmlAnnotation entry)
    {
        //editing xml elements of a character
        XEHolder el =
        new XEHolder("character", new XAttribute("id", entry.key), new XAttribute("prefab", entry.prefab),
            new XEHolder("position"),
            new XEHolder("rotation")
        );
        el.Element("position").SetAttributeValue("x", entry.transform.localPosition.x);
        el.Element("position").SetAttributeValue("z", entry.transform.localPosition.z);
        el.Element("rotation").SetAttributeValue("y", entry.transform.localRotation.eulerAngles.y);
        xmlHolder.xmlRoot.Add(el);
    }
    public override void LoadEntry(XmlAnnotation entry)
    {
        XEHolder position = xmlHolder.FindFirst("id", entry.key).Element("position");
        XEHolder rotation = xmlHolder.FindFirst("id", entry.key).Element("rotation");
        float x = position.FAttribute("x");
        float y = entry.transform.localPosition.y;
        float z = position.FAttribute("z");
        float w = rotation.FAttribute("y");
        //                                                    () ()
        //                                                    (0 w 0)
        entry.transform.localRotation = Quaternion.Euler(0, w, 0);
        entry.transform.localPosition = new Vector3(x, y, z);
    }
}