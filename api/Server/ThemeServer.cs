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

        /**
         * function mapBdMhemeModel - заполняет с бд данные в объект для вывода
         */
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

        /**
         *  function getAll - получить все темы
         */
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
        /**
        *  function getAll - получить темы с пагинацией
        */
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
        /**
        *  function getNameWhereId - получить тему по id 
        */
        public ThemeModel getNameWhereId(int id)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlTheme["getNameWhereId"], db);
            sql.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = sql.ExecuteReader();
            ThemeModel data = new ThemeModel();
            while (dr.Read())
            {
                data.id = dr.GetInt32(0);
                data.name = dr.GetString(1);
                data.description = dr.GetString(2);
            }
            db.Close();
            return data;



        }
        /**
        *  function getCountWhereName - получить количество записей по имени валидация занятого имени
        */
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
        /**
        *  function getCountWhereId - получить количество записей по id валидация занятого имени
        */
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
        /**
        *  function checkIdRows -  проверка есть ли записи по id
        */
        public bool checkIdRows(int id)
        {
            if (getCountWhereId(id) == 0)
            {
                return false;
            }
            return true;
        }
        /**
        *  function checkIdRows -  проверка есть ли записи по id
        */
        public bool checkSave(ThemeBodyModel themeBodyModel)
        {
            if (getCountWhereName(themeBodyModel.name) > 0)
            {
                return false;
            }
            return true;
        }

        public InfoAndId save(ThemeBodyModel themeBodyModel)
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
            db.Close();
            InfoAndId result = new InfoAndId("тема успешно создана");
            result.id = id;
            return result;
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
        
        public void deleteIn(ThemeBodyModelDeleteIn themeBodyModelDeleteIn)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["deleteThemeIds"], db);
            
            sql.Parameters.AddWithValue("@ids_theme", themeBodyModelDeleteIn.id);
            sql.ExecuteNonQuery();
            sql.CommandText = SqlCommand.sqlTheme["deleteIn"];
            sql.Parameters.AddWithValue("@ids", themeBodyModelDeleteIn.id);
            sql.ExecuteNonQuery();
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
