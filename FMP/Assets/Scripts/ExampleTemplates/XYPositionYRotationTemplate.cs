using System.Xml.Linq;
using UnityEngine;

//this template saves all positional information but no rotational information
public class XYPositionYRotationTemplate : RuntimeXmlLoader
{
    public override void SaveEntry(GameObject gameObject)
    {
        //editing xml elements of a character
        XEHolder el =
        new XEHolder("character", new XAttribute("id", gameObject.name),
            new XEHolder("position"),
            new XEHolder("rotation")
        );
        el.Element("position").SetAttributeValue("x", gameObject.transform.localPosition.x);
        el.Element("position").SetAttributeValue("z", gameObject.transform.localPosition.z);
        el.Element("rotation").SetAttributeValue("y", gameObject.transform.localRotation.eulerAngles.y);
        xmlHolder.xmlRoot.Add(el);
    }
    public override void LoadEntry(GameObject gameObject)
    {
        XEHolder position = xmlHolder.FindFirst("id", gameObject.name).Element("position");
        XEHolder rotation = xmlHolder.FindFirst("id", gameObject.name).Element("rotation");
        float x = position.FAttribute("x");
        float y = gameObject.transform.localPosition.y;
        float z = position.FAttribute("z");
        float w = rotation.FAttribute("y");
        //                                                    () ()
        //                                                    (0 w 0)
        gameObject.transform.localRotation = Quaternion.Euler(0, w, 0);
        gameObject.transform.localPosition = new Vector3(x, y, z);
    }
}