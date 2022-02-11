#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.

using Npgsql;
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
    }
}
