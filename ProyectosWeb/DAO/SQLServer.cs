using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Datos
{
    /// <summary>
    /// Summary description for SQLServer
    /// </summary>
    public class SQLServer : Database
    {

        private SqlConnection conn;
        private string connectionString;

        public SQLServer(string connectionString)
        {
            this.connectionString = connectionString;
            this.conn = new SqlConnection(this.connectionString);
        }

        public override object ExecuteScalar(string query, CommandType cmdType, ref ArrayList parameters)
        {
            using (SqlConnection _cnn = this.conn)
            {
                using (SqlCommand cmd = new SqlCommand(query, _cnn))
                {
                    Object o = null;
                    if (parameters != null)
                    {
                        foreach (SqlParameter prm in parameters)
                        {
                            cmd.Parameters.Add(prm);
                        }
                    }

                    cmd.CommandType = cmdType;
                    cmd.Connection.Open();
                    o = cmd.ExecuteScalar();

                    return o;
                }
            }
        }

        public override object ExecuteScalar(string query, CommandType cmdType)
        {
            using (SqlConnection _cnn = this.conn)
            {
                using (SqlCommand cmd = new SqlCommand(query, _cnn))
                {
                    Object o = null;

                    cmd.CommandType = cmdType;
                    cmd.Connection.Open();
                    o = cmd.ExecuteScalar();

                    return o;
                }
            }
        }

        public override DataTable ExecuteReader(string query, CommandType cmdType, ref ArrayList parameters)
        {
            using (SqlConnection _cnn = this.conn)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (SqlParameter prm in parameters)
                        {
                            cmd.Parameters.Add(prm);
                        }
                    }

                    SqlDataReader dr;
                    DataTable dt = new DataTable();

                    cmd.CommandType = cmdType;
                    cmd.Connection.Open();
                    dr = cmd.ExecuteReader();

                    dt.Load(dr);

                    return dt;
                }
            }
        }

        public override DataTable ExecuteReader(string query, CommandType cmdType)
        {
            using (SqlConnection _cnn = this.conn)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader dr;
                    DataTable dt = new DataTable();

                    cmd.CommandType = cmdType;
                    cmd.Connection.Open();
                    dr = cmd.ExecuteReader();

                    dt.Load(dr);

                    return dt;
                }
            }
        }

        public override int ExecuteNonQuery(string query, CommandType cmdType, ref ArrayList parameters)
        {
            Int32 result = 0;
            using (SqlConnection _cnn = this.conn)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (SqlParameter prm in parameters)
                        {
                            cmd.Parameters.Add(prm);
                        }
                    }

                    cmd.CommandType = cmdType;
                    cmd.Connection.Open();
                    result = (Int32)cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public override int ExecuteNonQuery(string query, CommandType cmdType)
        {
            Int32 result = 0;
            using (SqlConnection _cnn = this.conn)
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = cmdType;
                    cmd.Connection.Open();
                    result = (Int32)cmd.ExecuteNonQuery();
                }
            }
            return result;
        }


    }

}