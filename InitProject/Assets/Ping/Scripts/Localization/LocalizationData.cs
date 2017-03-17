using UnityEngine;
using System.Collections.Generic;

public class LocalizationData
{
    public static List<LocalizationConfig> config;
    public static void LoadLocalization()
    {
        LoadConfig();
        LoadFormFile(LocalizationPath.common);
    }

    private static void LoadFormFile(string _path)
    {
        TextAsset DefaultLocalFile = Resources.Load<TextAsset>(_path);
        if (DefaultLocalFile != null)
            Localization.LoadCSV(DefaultLocalFile, true);
    }

    private static void LoadConfig()
    {
        IEnumerable<LocalizationConfig> dataListFromCSV =
            CsvControll.LoadCSVDataFromFile<LocalizationConfig>(LocalizationPath.config);
        config = new List<LocalizationConfig>();
        int index = 1;
        foreach (LocalizationConfig datafromCSV in dataListFromCSV)
        {
            datafromCSV.index = index;
            config.Add(datafromCSV);
            index++;
        }
    }

    public static LocalizationConfig GetConfig(string _id)
    {
        for (int i = 0; i < config.Count; i++)
        {
            if (config[i].id == _id)
            {
                return config[i];
            }
        }
        return null;
    }
}

public class LocalizationConfig
{
    public string id;
    public string name;
    public int status;
    public int index;
}

public class LocalizationPath
{
    public const string common = "Localizations/Common";
    public const string config = "Localizations/Config";
}