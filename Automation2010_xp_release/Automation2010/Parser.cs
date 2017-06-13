using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.IO;

namespace Automation2010
{
    public class Parser
    {

        public static Table ParseHtml(string filename)
        {
            Table newTable = new Table();
            newTable.amount = new List<string>();
            newTable.num = new List<string>();
            bool flag = true;
            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(HtmlFromFile(filename));
                int tables_counter = 0; // Счётчик таблиц
                int rows_counter = 0;   // Счётчик строк
                int cols_counter = 0;   // Счётчик ячеек (столбцов)
                foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
                {
                    if (tables_counter == 1)    // Материал, имя, лст, компания
                    {
                        rows_counter = 1;       // Счётчик строк
                        foreach (HtmlNode row in table.SelectNodes("tr"))   // Обход строк
                        {
                            if (rows_counter == 5)                          // lstPath, name
                                foreach (HtmlNode cell in row.SelectNodes("th|td")) // Обход ячеек
                                {
                                    newTable.lstPath = cell.InnerText.Replace("&nbsp;", "");
                                    newTable.name = newTable.lstPath.Remove(0, 3); // Удаляем "C:\"
                                    try
                                    {
                                        int i = 0;
                                        char ch = '0';
                                        // Имя
                                        i = newTable.name.Length - 1;
                                        ch = '0';
                                        // Удаляем всё до последнего слеша
                                        while (ch != '\\')
                                        {
                                            i--;
                                            ch = newTable.name[i];
                                        }
                                        newTable.name = newTable.name.Remove(0, i + 1);

                                        ch = '0';
                                        i = 0;
                                        // Удаляем всё до первого имени
                                        while (ch != '.')
                                        {
                                            ch = newTable.name[i];
                                            i++;
                                        }
                                        newTable.name = newTable.name.Remove(i - 1);
                                    }
                                    catch
                                    {

                                    }
                                }

                            if (rows_counter == 6)                          //УП name
                                foreach (HtmlNode cell in row.SelectNodes("th|td")) // Обход ячеек
                                {
                                    newTable.lstPath = cell.InnerText.Replace("&nbsp;", "");
                                    newTable.lstPath = newTable.lstPath.Replace("(", "");
                                    newTable.lstPath = newTable.lstPath.Replace(")", "");
                                    newTable.lstPath = newTable.lstPath.Replace(" ", "");
                                    newTable.name = newTable.lstPath;
                                }
                            rows_counter++;
                        }
                    }
                    if (tables_counter == 5)    // Номер, количество, картинка 
                    {
                        rows_counter = 0;       // Счётчик строк, 0 - header 
                        foreach (HtmlNode row in table.SelectNodes("tr"))   // Обход строк
                        {
                            cols_counter = 1;
                            if (rows_counter == 5)                          // Количество
                                foreach (HtmlNode cell in row.SelectNodes("th|td")) // Обход ячеек
                                {
                                    if (cols_counter == 2)                  // amount
                                        newTable.amount.Add(cell.InnerText.Replace("&nbsp;", ""));
                                    cols_counter++;
                                }
                            if (rows_counter == 20) rows_counter = 1;
                            else rows_counter++;
                        }
                    }
                    tables_counter++;
                }
            }
            catch (ExecutionEngineException e)
            {
                flag = false;
            }
            if (flag) return newTable;
            else return null;
        }

        private static string HtmlFromFile(string fileName)
        {
            StringBuilder html = new StringBuilder();
            StreamReader file = new StreamReader(fileName, Encoding.GetEncoding("windows-1251"));
            string line;
            while ((line = file.ReadLine()) != null)
                html.Append(line);

            file.Close();
            return html.ToString();
        }

    }

}
