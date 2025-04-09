using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Threading.Tasks;

public class TableManager : ClassSingleton<TableManager>
{
	public Dictionary<System.Type, ITable> m_TableDictionary = new Dictionary<System.Type, ITable>();

	private FlowCommand m_FlowCommand = new FlowCommand();

	public void init()
	{

		// List<BossRecord> bossRecords = LoadCsvTable<BossRecord>("Table/BossTable");
		// List<MonsterRecord> monsterRecords = LoadCsvTable<MonsterRecord>("Table/MonsterTable");
		// List<FriendlyRecord> friendlyRecords = LoadCsvTable<FriendlyRecord>("Table/FriendlyTable");

		// BossTable bossTable = new BossTable(bossRecords);
		// MonsterTable monsterTable = new MonsterTable(monsterRecords);
		// FriendlyTable friendlyTable = new FriendlyTable(friendlyRecords);

		// m_TableDictionary.Add(typeof(BossRecord), bossTable);
		// m_TableDictionary.Add(typeof(MonsterTable), monsterTable);
		// m_TableDictionary.Add(typeof(FriendlyTable), friendlyTable);
	} 

	#if Addresable
	public async Task<List<T>> LoadCsvTableToAddressable<T>(string key) where T : new()
	{

		m_FlowCommand.Add(new Command_CheckAsset(key, (exists)=> {
			if( exists == true )
			{

			}
			else
			{

			}
		}));
		

		return null;
	}
	#endif

	public List<T> LoadCsvTable<T>(string resourcePath) where T : new()
    {
        List<T> result = new List<T>();

        try
        {
            // Resources 폴더에서 CSV 파일 읽기
            TextAsset csvFile = Resources.Load<TextAsset>(resourcePath);
            if (csvFile == null)
            {
                Debug.LogWarning($"CSV 파일을 찾을 수 없습니다: {resourcePath}");
                return result;
            }

            // 파일 내용 읽기
            string[] lines = csvFile.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // 첫 번째 줄은 헤더
            string[] headers = lines[0].Split(',');

            // 데이터 라인
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                // 반사(reflection)를 사용하여 클래스 객체 생성
              	T obj = new T();
				for (int j = 0; j < headers.Length; j++)
				{
					string propertyName = headers[j];
					string value = values[j];

					// 속성 찾기 (대소문자 구분 무시)
					var field = typeof(T).GetField(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
					if (field != null)
					{
						object convertedValue;

						// Enum 처리
						if (field.FieldType.IsEnum)
						{
							convertedValue = Enum.Parse(field.FieldType, value, ignoreCase: true); // Enum 값 변환
						}
						else if (field.FieldType == typeof(float))
						{
							// Float 변환
							convertedValue = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
						}
						else
						{
							convertedValue = Convert.ChangeType(value, field.FieldType);
						}

						field.SetValue(obj, convertedValue);
					}
					else
					{
						Debug.LogError($"Property or Field '{propertyName}' not found in type {typeof(T).Name}");
					}
				}
                result.Add(obj);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"CSV 파일 처리 중 오류 발생: {ex.Message}");
        }

        return result;
    }

    public T GetTable<T>() where T : class, ITable 
    {
        System.Type _type = typeof(T);

		ITable _find = null;
        if(m_TableDictionary.TryGetValue(_type, out _find) == false )
        {
            Debug.LogError($"{this.ToString()} ::GetTable() { _type.ToString()}");
            return null;
        }

        return _find as T;
    }


#if Addresable
	private List<T> LoadCsvTableAddresable<T>(TextAsset _asset) where T : new()
    {
        List<T> result = new List<T>();

        try
        {
            // Resources 폴더에서 CSV 파일 읽기
            if (_asset == null)
            {
                Debug.LogWarning($"CSV 파일을 찾을 수 없습니다: {_asset}");
                return result;
            }

            // 파일 내용 읽기
            string[] lines = _asset.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // 첫 번째 줄은 헤더
            string[] headers = lines[0].Split(',');

            // 데이터 라인
            for (int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');

                // 반사(reflection)를 사용하여 클래스 객체 생성
              	T obj = new T();
				for (int j = 0; j < headers.Length; j++)
				{
					string propertyName = headers[j];
					string value = values[j];

					// 속성 찾기 (대소문자 구분 무시)
					var field = typeof(T).GetField(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
					if (field != null)
					{
						object convertedValue;

						// Enum 처리
						if (field.FieldType.IsEnum)
						{
							convertedValue = Enum.Parse(field.FieldType, value, ignoreCase: true); // Enum 값 변환
						}
						else if (field.FieldType == typeof(float))
						{
							// Float 변환
							convertedValue = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
						}
						else
						{
							convertedValue = Convert.ChangeType(value, field.FieldType);
						}

						field.SetValue(obj, convertedValue);
					}
					else
					{
						Debug.LogError($"Property or Field '{propertyName}' not found in type {typeof(T).Name}");
					}
				}
                result.Add(obj);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"CSV 파일 처리 중 오류 발생: {ex.Message}");
        }

        return result;
    }
	#endif
	
	private void Update()
	{
		m_FlowCommand.UpdateLogic();
	}

}
