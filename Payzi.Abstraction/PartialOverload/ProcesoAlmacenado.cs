using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Payzi.Abstraction.PartialOverload
{
    public static class ProcesoAlmacenado
    {
        public static DbCommand LoadFunction<T>(this T context, string functionName)
        {
            DbContext db = context as DbContext;

            DbCommand cmd = db.Database.Connection.CreateCommand();

            cmd.CommandText = functionName;

            cmd.CommandType = System.Data.CommandType.Text;

            return cmd;
        }

        public static DbCommand LoadStoredProc<T>(this T context, string connectionString, string storedProcName)
        {
            using (DbContext db = new DbContext(connectionString))
            {
                DbCommand cmd = db.Database.Connection.CreateCommand();

                cmd.Connection.ConnectionString = connectionString;

                cmd.CommandText = storedProcName;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                return cmd;
            }
        }

        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText))
            {
                throw new InvalidOperationException("Call LoadStoredProc before using this method");
            }

            DbParameter param = cmd.CreateParameter();

            param.ParameterName = paramName;

            param.Value = paramValue;

            cmd.Parameters.Add(param);

            return cmd;
        }

        private static List<T> MapToList<T>(this DbDataReader dr)
        {
            List<T> objList = new List<T>();

            IEnumerable<PropertyInfo> props = typeof(T).GetRuntimeProperties();

            Dictionary<string, DbColumn> colMapping = dr.GetColumnSchema().Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower())).ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();

                    foreach (PropertyInfo prop in props)
                    {
                        object val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);

                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }

                    objList.Add(obj);
                }
            }

            return objList;
        }

        private static T MapToObject<T>(this DbDataReader dr)
        {
            T obj = Activator.CreateInstance<T>();

            IEnumerable<PropertyInfo> props = typeof(T).GetRuntimeProperties();

            Dictionary<string, DbColumn> colMapping = dr.GetColumnSchema().Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower())).ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    foreach (PropertyInfo prop in props)
                    {
                        object val = dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);

                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                }
            }

            return obj;
        }

        public static async Task<T> ExecuScalarAsync<T>(this DbCommand command, string connectionString)
        {
            using (command)
            {
                if (command.Connection.State == System.Data.ConnectionState.Closed)
                {
                    command.Connection.ConnectionString = connectionString;

                    command.Connection.Open();
                }

                try
                {
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        return reader.MapToObject<T>();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }
    }
}
