using Npgsql;
using api.Models;
using api.Sql;
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

        public void save(VarCssNameBodyModel varCssNameBodyModel)
        {
                db.Open();
                NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["save"], db);
                sql.Parameters.AddWithValue("@name", varCssNameBodyModel.name);
                sql.Parameters.AddWithValue("@description", varCssNameBodyModel.description);
                sql.ExecuteNonQuery();
                db.Close();
        
            }
        
        public void delete(int id)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["delete"], db);
            sql.Parameters.AddWithValue("@id", id);
            sql.ExecuteNonQuery();
            db.Close();
        }
        
        public void deleteIn(int[] ids)
        {
            db.Open();
            NpgsqlCommand sql = new NpgsqlCommand(SqlCommand.sqlVarCssName["deleteIn"], db);
            sql.Parameters.AddWithValue("@ids", ids);
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
