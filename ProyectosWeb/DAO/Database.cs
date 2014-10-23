using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Database
/// </summary>
public abstract class Database
{
	public Database()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public abstract Object ExecuteScalar(string query, CommandType cmdType, ref ArrayList parameters);
    public abstract DataTable ExecuteReader(string query, CommandType cmdType, ref ArrayList parameters);
    public abstract Int32 ExecuteNonQuery(string query, CommandType cmdType, ref ArrayList parameters);
    

    //Overloaded Methods

    public abstract Object ExecuteScalar(string query, CommandType cmdType);
    public abstract DataTable ExecuteReader(string query, CommandType cmdType);
    public abstract Int32 ExecuteNonQuery(string query, CommandType cmdType);
    

}