using Npgsql;
using api.Sql;
using api.Models;
using api.Models.VarCssNameTheme;
using api.libs;
namespace api.Server
{
    public class VarCssNameThemeServer
    {
        public NpgsqlConnection db = Connection.conn;

        public List<VarCssNameTheme> mapVarCssNameThemeValue(NpgsqlDataReader dr)
        {
            List<VarCssNameTheme> varCssNameThemeList = new List<VarCssNameTheme>();
            while (dr.Read())
            {
                VarCssNameTheme varCssNameTheme = new VarCssNameTheme();
                varCssNameTheme.id = dr.GetInt32(0);
                varCssNameTheme.name = dr.GetString(1);
                varCssNameTheme.value = libs.Libs.saveNullNpgsqlString(dr, 2);
                varCssNameThemeList.Add(varCssNameTheme);
            }
            return varCssNameThemeList;
        }

        public List<VarCssNameModel> mapVarCssName(NpgsqlDataReader dr)
        {
            List<VarCssNameModel> varCssNameList = new List<VarCssNameModel>();
            while (dr.Read())
            {
                VarCssNameModel varCssNameTheme = new VarCssNameModel();
                varCssNameTheme.id = dr.GetInt32(0);
                varCssNameTheme.name = dr.GetString(1);
                varCssNameTheme.description = libs.Libs.saveNullNpgsqlString(dr, 2);
                varCssNameList.Add(varCssNameTheme);
            }
            return varCssNameList;
        }



        public List<VarCssNameModel> getInsertVar(int id)
        {
            List<VarCssNameModel> varCssNameThemeList = new List<VarCssNameModel>();
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["getInsertVar"],
               db);
            sql.Parameters.AddWithValue("@id_theme", id);
            NpgsqlDataReader dr = sql.ExecuteReader();
            varCssNameThemeList = mapVarCssName(dr);
            db.Close();
            return varCssNameThemeList;

        }
        public List<VarCssNameTheme> getAll(int id)
        {
            List<VarCssNameTheme> varCssNameThemeList = new List<VarCssNameTheme>();
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["getThemeId"],
                db);
            sql.Parameters.AddWithValue("@id_theme", id);
            NpgsqlDataReader dr = sql.ExecuteReader();
            varCssNameThemeList = mapVarCssNameThemeValue(dr);
            db.Close();
            return varCssNameThemeList;
        }

        public Info delete(int id)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["delete"], db);
            sql.Parameters.AddWithValue("@id", id);
            sql.ExecuteNonQuery();
            db.Close();
            return new Info("запись успешно удалена");
        }

        public InfoAndId save(VarCssNameThemeSave varCssNameThemeSave)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["save"], db);
            sql.Parameters.AddWithValue("@id_theme", varCssNameThemeSave.id_theme);
            sql.Parameters.AddWithValue("@id_var_css", varCssNameThemeSave.id_var);
            NpgsqlDataReader dr = sql.ExecuteReader();
            int id = 0;
            while (dr.Read())
            {
                id = dr.GetInt32(0);
            }
            db.Close();
            InfoAndId result = new InfoAndId("запись успешно создана");
            result.id = id;
            return result;
        }

        public Info updateAll(VarCssNameThemeUpdateAllList varCssNameThemeUpdateAllList)
        {
            db.Open();
            /*
                TODO проверить на sql инвъекции! опасный код!!
             */
            List<string> names = new List<string>() {"id", "value" };
            string param = libs.Libs.convertListObjectInStringUpdateSql<VarCssNameThemeUpdateAll>(varCssNameThemeUpdateAllList.data, names);
            // спорное решение
            string sql_command = SqlCommand.sqlVarCssNameTheme["updateAll"];
            sql_command = sql_command.Replace("@values", param);
            NpgsqlCommand sql = new NpgsqlCommand(sql_command, db);
            // спорное решение
            sql.ExecuteNonQuery();
            db.Close();
            return new Info("записи успешно измененны"); 
        }
    }
}
