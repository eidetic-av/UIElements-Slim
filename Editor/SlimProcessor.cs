using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Eidetic.Editor.Slim
{
    class SlimProcessor : AssetPostprocessor
    {
        static async void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var slimFilePaths = importedAssets
                .Where(filePath => filePath.Split('.')[1] == "slim");

            foreach (var filePath in slimFilePaths)
            {
                var fileName = filePath.Split('.')[0];

                // Start the slim build ruby gem

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "slimrb";
                startInfo.Arguments = "-p " + fileName + ".slim " + fileName + ".temp";

                await Process.Start(startInfo);

                // When it's done, insert the UIElements boilerplate

                var tempOutput = File.ReadAllText(fileName + ".temp");
                var insertIndex = tempOutput.IndexOf("<UXML");
                var uxmlString = tempOutput.Insert(insertIndex + 5,
                    " xmlns:xsi=\"http:/www.w3.org/2001/XMLSchema-instance\""
                    + " xmlns=\"UnityEngine.UIElements\""
                    + " xsi:noNamespaceSchemaLocation=\"../UIElementsSchema/UIElements.xsd\""
                    + " xsi:schemaLocation=\"UnityEngine.UIElements ../UIElementsSchema/UnityEngine.UIElements.xsd\"");

                File.Delete(fileName + ".temp");

                var uxmlDoc = new XmlDocument();
                uxmlDoc.LoadXml(uxmlString);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                // Save the document to a file and auto-indent the output.
                XmlWriter writer = XmlWriter.Create(fileName + ".uxml", settings);
                uxmlDoc.Save(writer);
            }
        }
    }
}