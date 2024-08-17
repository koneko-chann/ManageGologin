using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageGologin.Helper
{
    public static class WebAndJs
    {
        public static Dictionary<string, string> GetWebAndJs()
        {
            CheckWebAndJsCounts();
            var webAndJs = new Dictionary<string, string>();

            // Đọc từng dòng của file navigationwebs.txt
            string webFilesPath = Path.Combine(Environment.CurrentDirectory, Resources.NavigationWebsFile);
            List<string> webLines = new List<string>();
            if (File.Exists(webFilesPath))
            {
                webLines.AddRange(File.ReadAllLines(webFilesPath));
            }

            // Đọc nội dung của các file trong thư mục JS
            string jsFolderPath = Path.Combine(Environment.CurrentDirectory, Resources.JSFolder);
            var jsFilesContent = new List<string>();
            if (Directory.Exists(jsFolderPath))
            {
                var jsFiles = Directory.GetFiles(jsFolderPath, "*.txt");
                foreach (var jsFile in jsFiles)
                {
                    string fileName = Path.GetFileName(jsFile);
                    string fileContent = File.ReadAllText(jsFile);
                    jsFilesContent.Add (fileContent);
                }
            }

            // Ghép nội dung của từng dòng từ navigationwebs.txt với nội dung của từng file .txt theo thứ tự
            
            for (int i = 0; i < webLines.Count; i++)
            {
                string webLine = webLines[i];
                string jsFileContent= jsFilesContent[i];
                webAndJs.Add(webLine, jsFileContent);
            }

            return webAndJs;
        }
        public static void CheckWebAndJsCounts()
        {
            // Đọc số lượng dòng trong file navigationwebs.txt
            string webFilesPath = Path.Combine(Environment.CurrentDirectory, Resources.NavigationWebsFile);
            int webLinesCount = 0;
            if (File.Exists(webFilesPath))
            {
                webLinesCount = File.ReadAllLines(webFilesPath).Length;
            }

            // Đọc số lượng file trong thư mục JS
            string jsFolderPath = Path.Combine(Environment.CurrentDirectory, Resources.JSFolder);
            int jsFilesCount = 0;
            if (Directory.Exists(jsFolderPath))
            {
                jsFilesCount = Directory.GetFiles(jsFolderPath, "*.txt").Length;
            }

            // Kiểm tra nếu số lượng dòng trong web và số lượng file trong JS không bằng nhau
            if (webLinesCount != jsFilesCount)
            {
                throw new Exception($"The number of web lines ({webLinesCount}) and JS files ({jsFilesCount}) do not match.");
            }
        }
    }
}
