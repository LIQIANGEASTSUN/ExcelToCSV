﻿using System.Collections.Generic;
using System.Text;
using System;

namespace ExcelExport
{
    /// <summary>
    /// 导出给客户端使用的 csv
    /// </summary>
    public class WriteCsvClient
    {
        private FileWriteWithLine fileWriteWithLine;
        private StringBuilder sb = new StringBuilder();

        public WriteCsvClient(ReadExcel readExcel)
        {
            if (!readExcel.IsValid)
            {
                return;
            }
            string savePath = FileHandle.CheckCsvClientFile(readExcel.ExcelPath);
            fileWriteWithLine = new FileWriteWithLine(savePath);

            foreach(List<string> list in readExcel.RowList)
            {
                WriteRow(readExcel, list);
            }

            fileWriteWithLine.Close();
        }

        private void WriteRow(ReadExcel readExcel, List<string> list)
        {
            sb.Clear();
            HashSet<int> exportColHash = readExcel.ClientExportColHash;
            for (int i = 0; i < list.Count; i++)
            {
                if (!exportColHash.Contains(i))
                {
                    continue;
                }

                if (i < list.Count - 1)
                {
                    sb.Append(string.Format("{0}{1}", list[i], ","));
                }
                else
                {
                    sb.Append(string.Format("{0}", list[i]));
                }
            }

            fileWriteWithLine.AppendLine(sb.ToString());
        }
    }
}
