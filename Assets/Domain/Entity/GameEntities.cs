//=====================================
// ゲーム用 Entity のロード＆保持するクラス
using CsvHelper;
using Entity;
using System.IO;
using IO;
namespace Entity
{

    public class GameEnities
    {
        static public GameEnities Instance { get; private set; }
        static public GameEnities CreateInstance()
        {
            if (Instance == null)
            {
                Instance = new GameEnities();
            }
            return Instance;
        }

        public Mission[] missions { get; set; }

        static GameEnities()
        {
            CreateClassMap();
            CreateConverter();
        }

        public void Load()
        {
            missions = LoadMissionRecords();
        }

        Mission[] LoadMissionRecords()
        {
            var texts = FileLoaderServer.Locator.LoadAllText("Mission.csv");
            using (var stream = new StringReader(texts))
            {
                var reader = new CsvReader(stream);
                return reader.GetRecords<Mission>();
            }
        }

        static void CreateConverter()
        {
            TypeConverterFactory.AddConverter(typeof(IdWithType), new IdWithTypeConverter());
        }
        static void CreateClassMap()
        {
            ClassMapFactory.AddClassMap(typeof(Mission), new MissionMap());
        }
    }
}