using System.IO;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    // 데이터 저장
    public static void Save(UserData userData)
    {
        string json = JsonUtility.ToJson(userData, true);
        string path = Path.Combine(Application.persistentDataPath, userData.id + ".json");

        File.WriteAllText(path, json);
        Debug.Log($"[SaveUserData] 저장 완료: {path}");
    }

    // 데이터 불러오기
    public static UserData Load(string id)
    {
        string path = Path.Combine(Application.persistentDataPath, id + ".json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            UserData userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log($"[LoadUserData] 불러오기 완료: {userData.name}");
            return userData;
        }
        else
        {
            Debug.LogWarning($"[LoadUserData] 해당 ID의 파일이 없습니다: {id}");
            return null;
        }
    }

    /// <summary>
    /// 이미 존재하는 ID인지 검사
    /// </summary>
    public static bool IsUserExists(string id)
    {
        string path = Path.Combine(Application.persistentDataPath, id + ".json");
        return File.Exists(path);
    }
}
