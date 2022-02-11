using Npgsql;
using api.Sql;
using api.Models.VarCssNameTheme;
using api.libs;
namespace api.Server
{
    public class VarCssNameThemeServer
    {
        public NpgsqlConnection db = Connection.conn;

        public List<VarCssNameTheme> getAll(int id)
        {
            List<VarCssNameTheme> varCssNameThemeList = new List<VarCssNameTheme>();
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["getThemeId"],
                db);
            sql.Parameters.AddWithValue("@id_theme", id);
            NpgsqlDataReader dr = sql.ExecuteReader();
            while (dr.Read())
            {
                VarCssNameTheme varCssNameTheme = new VarCssNameTheme();
                varCssNameTheme.id = dr.GetInt32(0);
                varCssNameTheme.name = dr.GetString(1);
                varCssNameTheme.value = libs.Libs.saveNullNpgsqlString(dr, 2);
                varCssNameThemeList.Add(varCssNameTheme);
            }
           db.Close();
           return varCssNameThemeList;
        }

        public string updateAll(List<VarCssNameThemeUpdateAll> varCssNameThemeUpdateAll)
        {
            db.Open();
            /*
                TODO проверить на sql инвъекции! опасный код!!
             */
            List<string> names = new List<string>() {"id", "value" };
            string param = libs.Libs.convertListObjectInStringUpdateSql<VarCssNameThemeUpdateAll>(varCssNameThemeUpdateAll, names);
            // спорное решение
            string sql_command = SqlCommand.sqlVarCssNameTheme["updateAll"];
            sql_command = sql_command.Replace("@values", param);
            NpgsqlCommand sql = new NpgsqlCommand(sql_command, db);
            // спорное решение
            sql.ExecuteNonQuery();
            db.Close();
            return "Успешно измененно";
        }
    }
}
