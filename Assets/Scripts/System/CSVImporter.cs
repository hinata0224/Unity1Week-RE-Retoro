// NOTE: WebGL以外でデータを読み込むよう

//using System.Collections.Generic;
//using UniRx;
//using UnityEngine;
//using UnityEngine.AddressableAssets;
//using System.IO;
//using System;

//namespace Other_System
//{
//    public class CSVImporter
//    {
//        private TextAsset csv;

//        private List<List<string>> csvData = new(50);

//        private Subject<bool> endCSVRead = new Subject<bool>();


//        public void ReadCSV(string filePath)
//        {
//            csv = null;
//            var load = Addressables.LoadAssetAsync<TextAsset>(filePath);
//            csv = load.WaitForCompletion();

//            string[] line = csv.text.Split("\r\n");

//            for (int i = 1; i < line.Length - 1; i++)
//            {
//                string[] temp = line[i].Split(",");
//                List<string> temps = new();
//                for(int j = 0; j < temp.Length; j++)
//                {
//                    temps.Add(temp[j]);
//                }
//                csvData.Add(temps);
//            }

//            Addressables.Release(load);
//            endCSVRead.OnNext(true);
//        }
//        public List<string> GetString(int callNum)
//        {
//            List<string> returnList = new List<string>(30);

//            for (int i = 0; i < csvData.Count; i++)
//            {
//                returnList.Add(csvData[i][callNum]);
//            }
//            return returnList;
//        }
//        public IObservable<bool> GetEndCSVRead()
//        {
//            return endCSVRead;
//        }
//    }
//}
