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
            var webAndJs = new Dictionary<string, string>();

            // Đọc từng dòng của file navigationwebs.txt
            string webFilesPath = Path.Combine(Environment.CurrentDirectory, "navigationwebs.txt");
            List<string> webLines = new List<string>();
            if (File.Exists(webFilesPath))
            {
                webLines.AddRange(File.ReadAllLines(webFilesPath));
            }

            // Đọc nội dung của các file trong thư mục JS
            string jsFolderPath = Path.Combine(Environment.CurrentDirectory, "JS");
            var jsFilesContent = new Dictionary<string, string>();
            if (Directory.Exists(jsFolderPath))
            {
                var jsFiles = Directory.GetFiles(jsFolderPath, "*.txt");
                foreach (var jsFile in jsFiles)
                {
                    string fileName = Path.GetFileName(jsFile);
                    string fileContent = File.ReadAllText(jsFile);
                    jsFilesContent[fileName] = fileContent;
                }
            }

            // Ghép nội dung của từng dòng từ navigationwebs.txt với nội dung của từng file .txt
            foreach (var webLine in webLines)
            {
                foreach (var jsFile in jsFilesContent)
                {
                    string combinedContent = webLine + Environment.NewLine + jsFile.Value;
                    string combinedKey = webLine + "_" + jsFile.Key; // Tạo khóa duy nhất cho mỗi kết hợp
                    webAndJs[combinedKey] = combinedContent;
                }
            }

            return webAndJs;
        }
        public static void CheckWebAndJsCounts()
        {
            // Đọc số lượng dòng trong file navigationwebs.txt
            string webFilesPath = Path.Combine(Environment.CurrentDirectory, "navigationwebs.txt");
            int webLinesCount = 0;
            if (File.Exists(webFilesPath))
            {
                webLinesCount = File.ReadAllLines(webFilesPath).Length;
            }

            // Đọc số lượng file trong thư mục JS
            string jsFolderPath = Path.Combine(Environment.CurrentDirectory, "JS");
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
