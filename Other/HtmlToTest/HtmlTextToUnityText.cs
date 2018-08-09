using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

public static class HtmlTextToUnityText
{
    public const string NewLine = "br";
    public const string Space = "&nbsp;";
    public const string Strong = "strong";
    public const string Bold = "b";
    public const string Italic = "i";
    public const string Emphasized = "em";
    public const string FontSize = "font-size";
    public const string Color = "color";
    public const string Style = "style";

    public static string Convert(string htmlText, float sizeScale = 1)
    {
        HtmlDocument document = new HtmlDocument();
        document.LoadHtml(htmlText);

        if (document.DocumentNode == null)
            return htmlText;

        RemoveEmptyNodes(document.DocumentNode);
        RemoveSpaceNodes(document.DocumentNode);
        RemoveUnsupportAttributes(document.DocumentNode);

        ChangeStrongToBold(document.DocumentNode);
        ChangeEmphasizedToItalic(document.DocumentNode);

        //color => size, order is important
        AddUnityColorMarkup(document.DocumentNode);
        AddUnitySizeMarkup(document.DocumentNode, sizeScale);

        RemoveStyleAttributes(document.DocumentNode);

        string result = document.DocumentNode.InnerHtml;
        result = HtmlEntity.DeEntitize(result);
        result = FixUnityMarkup(result);
        result = Regex.Replace(result, @"^\s+$[\r\n\n]*", "\r\n", RegexOptions.Multiline);

        return result;
    }

    private static void RemoveEmptyNodes(HtmlNode containerNode)
    {
        if (containerNode.Attributes.Count == 0 &&
            containerNode.Name != NewLine &&
            string.IsNullOrEmpty(containerNode.InnerText))
        {
            containerNode.Remove();
        }
        else
        {
            for (int i = containerNode.ChildNodes.Count - 1; i >= 0; i--)
            {
                RemoveEmptyNodes(containerNode.ChildNodes[i]);
            }
        }
    }

    private static void RemoveSpaceNodes(HtmlNode containerNode)
    {
        if (containerNode.InnerText == Space)
        {
            containerNode.Remove();
        }
        else
        {
            for (int i = containerNode.ChildNodes.Count - 1; i >= 0; i--)
            {
                RemoveSpaceNodes(containerNode.ChildNodes[i]);
            }
        }
    }

    private static void RemoveUnsupportAttributes(HtmlNode containerNode)
    {
        HtmlNodeCollection allNode = containerNode.SelectNodes("//p | //span | //ul | //li");

        if (allNode == null)
            return;

        foreach (HtmlNode node in allNode)
        {
            for (int i = node.Attributes.Count - 1; i >= 0; i--)
            {
                if (node.Attributes[i].Name != Style)
                    node.Attributes.RemoveAt(i);
            }
        }
    }

    private static void ChangeStrongToBold(HtmlNode containerNode)
    {
        IEnumerable<HtmlNode> allNode = containerNode.Descendants(Strong);

        if (allNode == null)
            return;

        foreach (HtmlNode node in allNode)
        {
            node.Name = Bold;
        }
    }

    private static void ChangeEmphasizedToItalic(HtmlNode containerNode)
    {
        IEnumerable<HtmlNode> allNode = containerNode.Descendants(Emphasized);

        if (allNode == null)
            return;

        foreach (HtmlNode node in allNode)
        {
            node.Name = Italic;
        }
    }

    private static void AddUnityColorMarkup(HtmlNode containerNode)
    {
        HtmlNodeCollection allNode = containerNode.SelectNodes("//span");

        if (allNode == null)
            return;

        foreach (HtmlNode node in allNode)
        {
            string oldHtml = node.InnerHtml;
            string styles = node.GetAttributeValue(Style, null);
            if (string.IsNullOrEmpty(styles))
                continue;

            if (!styles.Contains(Color))
                continue;

            //remove color:
            string value = styles.Remove(0, Color.Length + 1);
            value = value.Replace(";", "");
            string newHtml = string.Format("<color={0}>{1}</color>",
                value,
                oldHtml);
            node.InnerHtml = newHtml;
        }
    }

    private static void AddUnitySizeMarkup(HtmlNode containerNode, float sizeScale)
    {
        HtmlNodeCollection allNode = containerNode.SelectNodes("//span");

        if (allNode == null)
            return;

        foreach (HtmlNode node in allNode)
        {
            string oldHtml = node.InnerHtml;
            string styles = node.GetAttributeValue(Style, null);
            if (string.IsNullOrEmpty(styles))
                continue;

            if (!styles.Contains(FontSize))
                continue;

            //remove font-size:
            string value = styles.Remove(0, FontSize.Length + 1);
            value = value.Replace("pt", "");
            float number;

            if (float.TryParse(value, out number))
            {
                number *= sizeScale;

                string newHtml = string.Format("<size={0:#0}>{1}</size>",
                    number,
                    oldHtml);
                node.InnerHtml = newHtml;
            }
        }
    }

    private static void RemoveStyleAttributes(HtmlNode containerNode)
    {
        HtmlNodeCollection allNode = containerNode.SelectNodes("//p | //span | //ul | //li");

        if (allNode == null)
            return;

        foreach (HtmlNode node in allNode)
        {
            node.Attributes.Remove(Style);
        }
    }

    private static string FixUnityMarkup(string input)
    {
        // replace space
        string pattern = @"<p>|</p>|<span>|</span>|<ul>|</ul>|<li>|</li>";
        Regex regex = new Regex(pattern);
        input = regex.Replace(input, "");

        //replace color
        string patternColor = @"<[/]color=#[0-9a-f]{3,6}>";
        Regex regexColor = new Regex(patternColor);
        input = regexColor.Replace(input, "</color>");

        //replace size
        string patternSize = @"<[/]size=[\d]*[.]*[0 - 9]*>";
        Regex regesxsize = new Regex(patternSize);
        input = regesxsize.Replace(input, "</size>");

        return input;
    }
}