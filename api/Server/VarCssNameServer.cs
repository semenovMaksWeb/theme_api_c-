using Npgsql;
using api.Models;
using api.Sql;
using api.Models.Theme;

namespace api.Server.VarCssName
{
    public class VarCssNameServer
    {
        VarCssNameModel varCssNameModel = new VarCssNameModel();
        
        public NpgsqlConnection db = Connection.conn;
        
        public List<VarCssNameModel> getAll()
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["getAll"],
                db);
            NpgsqlDataReader dr = sql.ExecuteReader();
            List<VarCssNameModel> res = new List<VarCssNameModel>();
            while (dr.Read())
            {
                VarCssNameModel data = new VarCssNameModel(
                    dr.GetInt32(0),
                    dr.GetString(1),
                    libs.Libs.saveNullNpgsqlString(dr, 2)
                );
                res.Add(data);
            }
            db.Close();
            return res;
        }



        public VarCssNameModel getWhereId(int id)
        {
            db.Close();
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["getId"], db);
            sql.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = sql.ExecuteReader();
            VarCssNameModel data = new VarCssNameModel();
            while (dr.Read())
            {
                data.id = dr.GetInt32(0);
                data.name = dr.GetString(1);
                data.description = dr.GetString(2);
            }
            db.Close();
            return data;



        }


        public int getCountWhereName(string name)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["getCountWhereName"], db);
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
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["getCountWhereId"], db);
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

        public int getCountWhereNameAndId(int id, string name) 
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["getCountWhereNameAndId"], db);
            sql.Parameters.AddWithValue("@name", name);
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

        public bool checkSave(VarCssNameBodyModel varCssNameBodyModel)
        {
            if (getCountWhereName(varCssNameBodyModel.name) > 0)
            {
                return false;
            }
            return true;
        }

        public bool checkUpdate(int id, VarCssNameBodyModelUpdateAll varCssNameBodyModelUpdateAll)
        {
            if (getCountWhereNameAndId(id, varCssNameBodyModelUpdateAll.name) > 0 )
            {
                return false;
            }
            return true;
        }

        public int save(VarCssNameBodyModel varCssNameBodyModel)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["save"], db);
            sql.Parameters.AddWithValue("@name", varCssNameBodyModel.name);
            sql.Parameters.AddWithValue("@description", varCssNameBodyModel.description);
            NpgsqlDataReader dr = sql.ExecuteReader();
            int id = 0;
            while (dr.Read())
            {
               id = dr.GetInt32(0);
            }
            dr.Close();
            return id;
            //sql.CommandText = SqlCommand.sqlVarCssNameTheme["insertAllVarCss"];
            //sql.Parameters.AddWithValue("@id_var_css", id);
            //sql.ExecuteNonQuery();       
            }
        
        public void delete(int id)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["deleteVarCssId"], db);
            sql.Parameters.AddWithValue("@id_var_css", id);
            sql.ExecuteNonQuery();
            sql.CommandText = SqlCommand.sqlVarCssName["delete"];    
            sql.Parameters.AddWithValue("@id", id);
            sql.ExecuteNonQuery();

            db.Close();
        }
        
        public void deleteIn(ThemeBodyModelDeleteIn ids)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssNameTheme["deleteVarCssIds"], db);
            sql.Parameters.AddWithValue("@ids_var_css", ids.id);
            sql.ExecuteNonQuery();
            sql.CommandText =  SqlCommand.sqlVarCssName["deleteIn"];
            sql.Parameters.AddWithValue("@ids", ids.id);
            sql.ExecuteNonQuery();
            db.Close();
        }
       
        public void updateAll(int id, VarCssNameBodyModelUpdateAll varCssNameBodyModelUpdateAll)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["updateAll"], db);
            sql.Parameters.AddWithValue("@id", id);
            sql.Parameters.AddWithValue("@name", varCssNameBodyModelUpdateAll.name);
            sql.Parameters.AddWithValue("@description", varCssNameBodyModelUpdateAll.description);
            sql.ExecuteNonQuery();
            db.Close();
        }
        
        public void updateDescription(int id, VarCssNameBodyModelUpdateDescription varCssNameBodyModelUpdateDescription)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["updateDescription"], db);
            sql.Parameters.AddWithValue("@id", id);
            sql.Parameters.AddWithValue("@description", varCssNameBodyModelUpdateDescription.description);
            sql.ExecuteNonQuery();
            db.Close();
        }

    }   
}
