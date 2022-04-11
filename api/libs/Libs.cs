#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.

using Npgsql;
using System.Data;

namespace api.libs
{
    public class Libs
    {
        static public string saveNullNpgsqlString(NpgsqlDataReader dr, int index)
        {
            if (dr.IsDBNull(index))  
            {
                return "";
            }
            return dr.GetString(index);
        }
        static public string convertListObjectInStringUpdateSql<T>(List<T> values, List<string> names)
        {
            string sqlUpdate = "";
            for (int i = 0; i < values.Count; i++)
            {
                T value = values[i];
                if (value != null)
                {
                    sqlUpdate += "(";
                    foreach (var name in names)
                    {
                        if (value.GetType().GetProperty(name).GetValue(value) is string)
                        {
                            sqlUpdate += $@"'{value.GetType().GetProperty(name).GetValue(value)}',";
                        }
                        else
                        {
 
                            sqlUpdate += $@"{value.GetType().GetProperty(name).GetValue(value)},";
                        }
                    }
                    sqlUpdate = sqlUpdate.Remove(sqlUpdate.Length - 1);
                    sqlUpdate += "),";
                }
            }
            sqlUpdate = sqlUpdate.Remove(sqlUpdate.Length - 1);
            return sqlUpdate;
        }

        static public Dictionary<string, Dictionary<string, string>> createCustomErrors(Dictionary<string, string> errors_key)
        {
            Dictionary<string, Dictionary<string, string>> response = new Dictionary<string, Dictionary<string, string>>();
            response.Add("errors", errors_key);
            return response;

        }
        static public void checkBdOpen(NpgsqlConnection db)
        {
            if(db.State != ConnectionState.Open)
            {
                db.Open();
            }
        }
    }
}
