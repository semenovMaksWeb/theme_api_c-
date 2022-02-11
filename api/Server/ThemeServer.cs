using Npgsql;
using api.Models;
using api.Sql;
using api.Models.Theme;

namespace api.Server.Theme
{
    public class ThemeServer
    {
        ThemeModel themeModel = new ThemeModel();
        
        public NpgsqlConnection db = Connection.conn;

        
        private List<ThemeModel> mapBdMhemeModel(NpgsqlDataReader dr)
        {
            var themeModel = new List<ThemeModel>();
            while (dr.Read())
            {
                ThemeModel data = new ThemeModel(
                    dr.GetInt32(0),
                    dr.GetString(1),
                    dr.GetString(2)
                );
                themeModel.Add(data);
            }
            return themeModel;
        }
        
        public List<ThemeModel> getAll()
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["getAll"],
                db);
            NpgsqlDataReader dr = sql.ExecuteReader();
            List<ThemeModel> res = new List<ThemeModel>();
            res = mapBdMhemeModel(dr);
            db.Close();
            return res;
        }

        public List<ThemeModel> getPages(Pagination pagination)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["getPages"],
                db);
            sql.Parameters.AddWithValue("@limit", pagination.limit);
            sql.Parameters.AddWithValue("@offset", pagination.offset);
            NpgsqlDataReader dr = sql.ExecuteReader();
            List<ThemeModel> res = new List<ThemeModel>();
            res = mapBdMhemeModel(dr);
            db.Close();
            return res;
        }

        public string getNameWhereId(int id)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["getNameWhereId"], db);
            sql.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = sql.ExecuteReader();
            string name = "";
            while (dr.Read())
            {
                name = dr.GetString(0);
            }
            db.Close();
            return name;



        }

        public int getCountWhereName(string name)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["getCountWhereName"], db);
            sql.Parameters.AddWithValue("@name", name);
            NpgsqlDataReader dr = sql.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count = dr.GetInt32(0);
            }
            db.Close();
            return count;



        }

        public int getCountWhereId(int id)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["getCountWhereId"], db);
            sql.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = sql.ExecuteReader();
            int count = 0;
            while (dr.Read())
            {
                count = dr.GetInt32(0);
            }
            db.Close();
            return count;
        }

        public bool checkIdRows(int id)
        {
            if (getCountWhereId(id) == 0)
            {
                return false;
            }
            return true;
        }

        public bool checkSave(ThemeBodyModel themeBodyModel)
        {
            if (getCountWhereName(themeBodyModel.name) > 0)
            {
                return false;
            }
            return true;
        }

        public void save(ThemeBodyModel themeBodyModel)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["save"], db);
            sql.Parameters.AddWithValue("@name", themeBodyModel.name);
            sql.Parameters.AddWithValue("@description", themeBodyModel.description);
            NpgsqlDataReader dr = sql.ExecuteReader();
            int id = 0;
            while (dr.Read())
            {
                id = dr.GetInt32(0);
            }
            dr.Close();
            sql.CommandText = SqlCommand.sqlVarCssNameTheme["insertAll"];
            sql.Parameters.AddWithValue("@id_theme", id);
            sql.ExecuteNonQuery();
            db.Close();
        }
        
        public void delete(int id)
        {
            db.Open();

            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["deleteThemeId"], db);
            sql.Parameters.AddWithValue("@id_theme", id);
            sql.ExecuteNonQuery();
            sql.CommandText = SqlCommand.sqlTheme["delete"];
            sql.Parameters.AddWithValue("@id", id);
            sql.ExecuteNonQuery();


            db.Close();
        }
        
        public void deleteIn(int[] ids)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["deleteThemeIds"], db);
            sql.Parameters.AddWithValue("@ids_theme", ids);
            sql.ExecuteNonQuery();
            sql.CommandText = SqlCommand.sqlTheme["delete"];
            sql.Parameters.AddWithValue("@ids", ids);
            db.Close();
        }
       
        public void updateAll(int id, ThemeBodyModelUpdateAll themeBodyModelUpdateAll)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["updateAll"], db);
            sql.Parameters.AddWithValue("@id", id);
            sql.Parameters.AddWithValue("@name", themeBodyModelUpdateAll.name);
            sql.Parameters.AddWithValue("@description", themeBodyModelUpdateAll.description);
            sql.ExecuteNonQuery();
            db.Close();
        }
        
        public void updateDescription(int id, ThemeBodyModelUpdateDescription themeBodyModelUpdateDescription)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["updateDescription"], db);
            sql.Parameters.AddWithValue("@id", id);
            sql.Parameters.AddWithValue("@description", themeBodyModelUpdateDescription.description);
            sql.ExecuteNonQuery();
            db.Close();
        }

    }   
}
