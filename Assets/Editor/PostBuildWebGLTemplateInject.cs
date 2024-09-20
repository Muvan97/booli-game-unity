#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Editor
{
    public class PostBuildWebGLTemplateInject : MonoBehaviour
    {
        private const string TemplatePath = "Assets/Editor/Template";

        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target is BuildTarget.WebGL)
            {
                Debug.Log($"Injection started: {pathToBuiltProject}");
                Inject(pathToBuiltProject);
                Debug.Log($"Injection succeed: {pathToBuiltProject}");
            }
        }

        public static void Inject(string pathToBuiltProject)
        {
            UpdateFiles(pathToBuiltProject);
            InjectTemplateValues(pathToBuiltProject);
        }

        private static void UpdateFiles(string pathToBuiltProject)
        {
            var files = Directory.GetFiles(TemplatePath).Where(f => f.Contains(".meta") == false);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var destFile = Path.Combine(pathToBuiltProject, fileName);
                File.Copy(file, destFile, true);
            }
        }

        private static void InjectTemplateValues(string pathToBuiltProject)
        {
            var buildDir = Path.Combine(pathToBuiltProject, "Build");
            var candidates = Directory.GetFiles(buildDir);
            var indexHtmlPath = Path.Combine(pathToBuiltProject, "index.html");

            var injectOperation = new SeveralHtmlInjectOperation(
                new HtmlInjectOperation("{{LOADER_FILE}}", "loader.js"),
                new HtmlInjectOperation("{{DATA_FILE}}", "data."),
                new HtmlInjectOperation("{{FRAMEWORK_FILE}}", "framework.js."),
                new HtmlInjectOperation("{{WASM_FILE}}", "wasm.")
            );

            var indexHtmlContent = File.ReadAllText(indexHtmlPath);
            injectOperation.InjectTo(candidates, indexHtmlContent, out var resultContent);
            File.WriteAllText(indexHtmlPath, resultContent);
        }

        private interface IHtmlInjectOperation
        {
            void InjectTo(IEnumerable<string> candidateFiles, string indexHtmlContent, out string result);
        }

        private class HtmlInjectOperation : IHtmlInjectOperation
        {
            private readonly string placeHolder;
            private readonly string filePattern;

            public HtmlInjectOperation(string placeHolder, string filePattern)
            {
                this.placeHolder = placeHolder;
                this.filePattern = filePattern;
            }

            public void InjectTo(IEnumerable<string> candidateFiles, string indexHtmlContent, out string result)
            {
                foreach (var candidate in candidateFiles)
                {
                    if (candidate.Contains(filePattern))
                    {
                        var fileName = Path.GetFileName(candidate);
                        result = indexHtmlContent.Replace(placeHolder, fileName);
                        return;
                    }
                }

                throw new FileNotFoundException($"File with pattern {filePattern} not found");
            }
        }

        private class SeveralHtmlInjectOperation : IHtmlInjectOperation
        {
            private readonly IHtmlInjectOperation[] htmlInjectOperations;

            public SeveralHtmlInjectOperation(params IHtmlInjectOperation[] htmlInjectOperations)
            {
                this.htmlInjectOperations = htmlInjectOperations;
            }

            public void InjectTo(IEnumerable<string> candidateFiles, string indexHtmlContent, out string result)
            {
                result = indexHtmlContent;

                foreach (var operation in htmlInjectOperations)
                {
                    operation.InjectTo(candidateFiles, result, out result);
                }
            }
        }
    }
}