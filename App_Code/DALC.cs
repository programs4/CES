using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;


public class DALC
{
    public static SqlConnection SqlConn
    {
        get
        {
            return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString);
        }
    }

    public class UsersClass
    {
        public int ID;
        public int OrganizationsID;
        public string Organizations;
        public int OrganizationsParentID;
        public string Fullname;
        public string UsersPermissionsModules;
        public string PermissionsIP;
        public string Key;
    }

    public static UsersClass _GetUsersLogin
    {
        get
        {
            if (HttpContext.Current.Session["UsersLogin"] != null)
            {
                UsersClass UsersClass = new UsersClass();
                UsersClass = (UsersClass)HttpContext.Current.Session["UsersLogin"];
                return UsersClass;
            }
            else
            {
                return null;
            }
        }
    }

    public static int SetByUsersInfo(string Login, string Password)
    {
        DataTable Dt = GetDataTableBySqlCommand(string.Format(@"Select * From {0}
                                                                 Where Login=@Login and 
                                                                 Password=@Password and 
                                                                 IsActive=1 and UsersStatusID not in(90,95)", Tools.Table.V_UsersAccounts),
                                                                 new string[] { "Login", "Password" },
                                                                 new object[] { Login, Password });
        if (Dt == null)
        {
            return -1;
        }

        if (Dt.Rows.Count < 1)
        {
            return 0;
        }

        //Success
        UsersClass UsersLoginInformation = new UsersClass();
        UsersLoginInformation.ID = Dt._RowsObject("ID")._ToInt16();
        UsersLoginInformation.OrganizationsID = int.Parse(Dt._Rows("OrganizationsID"));
        UsersLoginInformation.Organizations = Dt._Rows("Organizations");
        UsersLoginInformation.OrganizationsParentID = int.Parse(Dt._Rows("OrganizationsParentID"));
        UsersLoginInformation.Fullname = Dt._Rows("Fullname");
        UsersLoginInformation.UsersPermissionsModules = Dt._Rows("UsersPermissionsModules");
        UsersLoginInformation.PermissionsIP = Dt._Rows("PermissionsIP");
        UsersLoginInformation.Key = Config.Key(20);

        HttpContext.Current.Session["UsersLogin"] = UsersLoginInformation;

        return 1;
    }

    #region Rahatlaşdıran metodlarımız

    public class DataTableResult
    {
        public int Count;
        public DataTable Dt = new DataTable();
    }

    public class Transaction
    {
        public SqlTransaction SqlTransaction = null;
        public SqlCommand Com = new SqlCommand();
    }

    #region Single
    //Bazadan tək dəyər alaq:
    public static string GetSingleValues(string Columns, Tools.Table TableName, string WhereAndOrderBy = "", string CatchValue = "-1")
    {
        using (SqlCommand com = new SqlCommand(string.Format("Select {0} From {1} {2}", Columns, TableName, WhereAndOrderBy), SqlConn))
        {
            try
            {
                com.Connection.Open();
                return com.ExecuteScalar()._ToString();
            }
            catch (Exception er)
            {
                ErrorLogs(string.Format("DALC.GetDbSingleValues [{0}] catch error: {1}", TableName, er.Message));
                return CatchValue;
            }
            finally
            {
                com.Connection.Close();
                com.Connection.Dispose();
            }
        }
    }

    //Bazadan tək dəyər alaq:
    public static string GetSingleValues(string Columns, Tools.Table TableName, string ParamsColumns, object ParamsValue, string OrderBy, string CatchValue = "-1")
    {
        using (SqlCommand com = new SqlCommand(string.Format("Select {0} From {1} Where {2}=@ParamsValue {3}", Columns, TableName, ParamsColumns, OrderBy), SqlConn))
        {
            try
            {
                com.Connection.Open();
                com.Parameters.AddWithValue("@ParamsValue", ParamsValue);
                return com.ExecuteScalar()._ToString();
            }
            catch (Exception er)
            {
                ErrorLogs(string.Format("DALC.GetSingleValues [{0}] catch error: {1}", TableName, er.Message));
                return CatchValue;
            }
            finally
            {
                com.Connection.Close();
                com.Connection.Dispose();
            }
        }
    }

    //Bazadan tək dəyər alaq - istənilən saytda parametr ilə:
    public static string GetSingleValues(string Columns, Tools.Table TableName, string ParamsKeys, object[] ParamsValues, string OrderBy = "", string CatchValue = "-1")
    {
        //SqlMulti params command
        SqlCommand com = new SqlCommand();

        try
        {
            string[] ParamsKeysArray = ParamsKeys.Split(',');

            //Mütləq value və parametr adları eyni olmalıdır.
            if (ParamsKeysArray.Length != ParamsValues.Length)
            {
                throw new Exception("ParamsKeys and ParamsValues not same size.");
            }

            StringBuilder WhereList = new StringBuilder("1=1");
            for (int i = 0; i < ParamsKeysArray.Length; i++)
            {
                if (ParamsKeysArray[i].Length < 1)
                    continue;

                WhereList.Append(" and " + ParamsKeysArray[i] + "=@" + ParamsKeysArray[i]);
                com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
            }

            com.CommandText = string.Format("Select {0} From {1} Where {2} {3}", Columns, TableName, WhereList, OrderBy);
            com.Connection = SqlConn;


            com.Connection.Open();
            return com.ExecuteScalar()._ToString();
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.GetSingleValues [{0}] catch error: {1}", TableName, er.Message));
            return CatchValue;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
        }
    }

    public static string GetSingleValuesBySqlCommand(string SqlCommand, string ParamsKeys = "", object[] ParamsValues = null, CommandType CommandType = CommandType.Text)
    {
        string[] ParamsKeysArray = ParamsKeys.Split(',');

        using (SqlCommand com = new SqlCommand(SqlCommand, SqlConn))
        {
            try
            {
                com.CommandType = CommandType;
                if (ParamsValues != null)
                {
                    if (ParamsKeysArray.Length != ParamsValues.Length)
                    {
                        throw new Exception("ParamsKeys and ParamsValues not same size.");
                    }

                    for (int i = 0; i < ParamsKeysArray.Length; i++)
                    {
                        com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
                    }
                }
                com.Connection.Open();
                return com.ExecuteScalar()._ToString();

            }
            catch (Exception er)
            {
                ErrorLogs(string.Format("DALC.GetDataTableBySqlCommand catch error: {0}", er.Message));
                return "-1";
            }
            finally
            {
                com.Connection.Close();
                com.Connection.Dispose();
            }
        }
    }

    #endregion

    #region Table

    //Bazadan table alaq
    public static DataTable GetDataTable(string Columns, Tools.Table TableName, string WhereAndOrderBy = "")
    {
        DataTable Dt = new DataTable();
        try
        {
            using (SqlDataAdapter Da = new SqlDataAdapter(String.Format("Select {0} From {1} {2}", Columns, TableName, WhereAndOrderBy), SqlConn))
            {
                Da.Fill(Dt);
                return Dt;
            }
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.GetDataTable [{0}] catch error: {1}", TableName, er.Message));
            return null;
        }
    }

    //Konkret 1 ədəd paramteri olanda.
    public static DataTable GetDataTable(string Columns, Tools.Table TableName, string ParamsColumns, object ParamsValue, string OrderBy = "")
    {
        DataTable Dt = new DataTable();
        try
        {
            using (SqlDataAdapter Da = new SqlDataAdapter(String.Format("Select {0} From {1} Where {2}=@ParamsValue {3}", Columns, TableName, ParamsColumns, OrderBy), SqlConn))
            {
                Da.SelectCommand.Parameters.AddWithValue("@ParamsValue", ParamsValue);
                Da.Fill(Dt);
                return Dt;
            }
        }

        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.GetDataTable [{0}] catch error: {1}", TableName, er.Message));
            return null;
        }
    }

    //Konkret 1 ədəd paramteri olanda.
    public static DataTable GetDataTable(string Columns, Tools.Table TableName, string ParamsKeys, object[] ParamsValues, string OrderBy = "")
    {
        try
        {
            string[] ParamsKeysArray = ParamsKeys.Split(',');

            //Mütləq value və parametr adları eyni olmalıdır.
            if (ParamsKeysArray.Length != ParamsValues.Length)
            {
                throw new Exception("ParamsKeys and ParamsValues not same size.");
            }
            //SqlMulti params command
            using (SqlCommand com = new SqlCommand())
            {
                StringBuilder WhereList = new StringBuilder("1=1");
                const string format = " and {0}=@{0}";

                for (int i = 0; i < ParamsKeysArray.Length; i++)
                {
                    if (ParamsKeysArray[i].Length < 1)
                        continue;

                    WhereList.Append(string.Format(format, ParamsKeysArray[i]));
                    com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
                }

                com.CommandText = String.Format("Select {0} From {1} Where {2} {3}", Columns, TableName, WhereList, OrderBy);
                com.Connection = SqlConn;

                DataTable Dt = new DataTable();
                SqlDataAdapter Da = new SqlDataAdapter(com);
                Da.Fill(Dt);

                return Dt;
            }
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.GetDataTable [{0}] catch error: {1}", TableName, er.Message));
            return null;
        }
    }

    //Ümumi sql commanda görə datatable qaytarır.   
    public static DataTable GetDataTableBySqlCommand(string SqlCommand, string[] ParamsKeys = null, object[] ParamsValues = null, CommandType CommandType = CommandType.Text)
    {
        try
        {
            DataTable Dt = new DataTable();
            using (SqlCommand com = new SqlCommand(SqlCommand, SqlConn))
            {
                com.CommandType = CommandType;

                if (ParamsValues != null)
                {
                    if (ParamsKeys.Length != ParamsValues.Length)
                    {
                        throw new Exception("ParamsKeys and ParamsValues not same size.");
                    }

                    for (int i = 0; i < ParamsKeys.Length; i++)
                    {
                        com.Parameters.AddWithValue("@" + ParamsKeys[i], ParamsValues[i]);
                    }
                }
                com.Connection.Open();
                using (SqlDataReader Reader = com.ExecuteReader())
                {
                    Dt.Load(Reader);
                }
                com.Connection.Close();
                return Dt;
            }
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.GetDataTableBySqlCommand catch error: {0}", er.Message));
            return null;
        }
    }

    #endregion

    #region Insert

    public static int InsertDatabase(Tools.Table TableName, Dictionary<string, object> Dictionary, bool IsErrorLog = true)
    {
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();
        SqlCommand com = new SqlCommand();

        foreach (var Item in Dictionary)
        {
            Columns.Append(Item.Key + ",");
            Params.Append("@" + Item.Key + ",");
            com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
        }

        try
        {
            com.Connection = SqlConn;
            com.CommandText = string.Format("Insert Into {0}({1}) Values({2}); Select SCOPE_IDENTITY();", TableName, Columns.ToString().Trim(','), Params.ToString().Trim(','));
            com.Connection.Open();
            return com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            //Əgər log eliyərkən xəta veribsə sadəcə email göndərək LOOP eləməsin.
            //Çünki log da xəta veribsə yəqin ki, SQL dayanıb.
            if (IsErrorLog)
            {
                ErrorLogs(string.Format("DALC.InsertDatabase [{0}] catch error: {1}", TableName, er.Message));
            }
            else
            {
                SendAdminMail(TableName._ToString(), string.Format("ErrorLogs-de xəta baş verdi: {0}", er.Message));
            }
            return -1;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    public static int InsertDatabase(Tools.Table TableName, string[] Key, object[] Values)
    {
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();
        SqlCommand com = new SqlCommand();

        for (int i = 0; i < Key.Length; i++)
        {
            Columns.Append(Key[i] + ", ");
            Params.Append("@" + Key[i] + ", ");
            com.Parameters.AddWithValue("@" + Key[i], Values[i]);
        }

        try
        {
            com.Connection = SqlConn;
            com.CommandText = string.Format("Insert Into {0}({1}) Values({2}); Select SCOPE_IDENTITY();", TableName, Columns.ToString().Trim().Trim(','), Params.ToString().Trim().Trim(','));
            com.Connection.Open();
            return com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.InsertDatabase [{0}] catch error: {1}", TableName, er.Message));
            return 0;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    public static int InsertDatabase(Tools.Table TableName, Dictionary<string, object> Dictionary, Transaction Transaction, bool IsCommit = false)
    {
        int result = -1;
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();

        // Parametri təmizləyəkki ikinci dəfə eyni parametrlə insertə gəldikdə xəta verməsin
        Transaction.Com.Parameters.Clear();
        foreach (var Item in Dictionary)
        {
            Columns.Append(Item.Key + ",");
            Params.Append("@" + Item.Key + ",");
            Transaction.Com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
        }

        try
        {
            if (Transaction.SqlTransaction == null)
            {
                Transaction.Com.Connection = SqlConn;
                Transaction.Com.Connection.Open();
                Transaction.SqlTransaction = Transaction.Com.Connection.BeginTransaction();
            }

            Transaction.Com.CommandText = string.Format("Insert Into {0}({1}) Values({2}); Select SCOPE_IDENTITY();", TableName, Columns.ToString().Trim(','), Params.ToString().Trim(','));
            Transaction.Com.Transaction = Transaction.SqlTransaction;
            result = Convert.ToInt32(Transaction.Com.ExecuteScalar());

            if (IsCommit)
            {
                Transaction.SqlTransaction.Commit();
                Transaction.SqlTransaction.Dispose();
            }

            return result;
        }
        catch (Exception er)
        {
            Transaction.SqlTransaction.Rollback();
            IsCommit = true;
            ErrorLogs(string.Format("DALC.InsertDatabase [{0}] catch error: {1}", TableName, er.Message));
            return -1;
        }

        finally
        {
            if (IsCommit)
            {
                Transaction.Com.Connection.Close();
                Transaction.Com.Connection.Dispose();
                Transaction.Com.Dispose();
            }
        }
    }

    //Toplu insert
    public static int InsertBulk(Tools.Table TableName, DataTable Dt)
    {
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();
        SqlCommand com = new SqlCommand();
        try
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Params.Append("(");
                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    if (i == 0)
                        Columns.Append(Dt.Columns[j].ColumnName + ",");

                    Params.Append("@P" + i.ToString() + j.ToString() + (j < Dt.Columns.Count - 1 ? "," : ""));
                    com.Parameters.AddWithValue("@P" + i.ToString() + j.ToString(), Dt.Rows[i][j]);
                }

                Params.Append("),");
            }
            com.Connection = SqlConn;
            com.CommandText = String.Format("Insert Into {0}({1}) Values{2}", TableName, Columns.ToString().Trim(','), Params.ToString().Trim(','));

            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.InsertBulk [{0}] catch error: {1}", TableName, er.Message));
            return -1;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    //Toplu insert
    public static int InsertBulk(Tools.Table TableName, DataTable Dt, Transaction Transaction, bool IsCommit = false)
    {
        int result = -1;
        StringBuilder Columns = new StringBuilder();
        StringBuilder Params = new StringBuilder();

        // Parametri təmizləyəkki ikinci dəfə eyni parametrlə insertə gəldikdə xəta verməsin
        Transaction.Com.Parameters.Clear();
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            Params.Append("(");
            for (int j = 0; j < Dt.Columns.Count; j++)
            {
                if (i == 0)
                    Columns.Append(Dt.Columns[j].ColumnName + ",");

                Params.Append("@P" + i.ToString() + j.ToString() + (j < Dt.Columns.Count - 1 ? "," : ""));
                Transaction.Com.Parameters.AddWithValue("@P" + i.ToString() + j.ToString(), Dt.Rows[i][j]);
            }

            Params.Append("),");
        }

        try
        {
            if (Transaction.SqlTransaction == null)
            {
                Transaction.Com.Connection = SqlConn;
                Transaction.Com.Connection.Open();
                Transaction.SqlTransaction = Transaction.Com.Connection.BeginTransaction();
            }

            Transaction.Com.CommandText = String.Format("Insert Into {0}({1}) Values{2}", TableName, Columns.ToString().Trim(','), Params.ToString().Trim(','));
            Transaction.Com.Transaction = Transaction.SqlTransaction;
            result = Transaction.Com.ExecuteNonQuery();

            if (IsCommit)
            {
                Transaction.SqlTransaction.Commit();
                Transaction.SqlTransaction.Dispose();
            }

            return result;
        }
        catch (Exception er)
        {
            Transaction.SqlTransaction.Rollback();
            IsCommit = true;
            ErrorLogs(string.Format("DALC.InsertBulk [{0}] catch error: {1}", TableName, er.Message));
            return -1;
        }
        finally
        {
            if (IsCommit)
            {
                Transaction.Com.Connection.Close();
                Transaction.Com.Connection.Dispose();
                Transaction.Com.Dispose();
            }
        }
    }

    #endregion

    #region Update
    /// <summary>
    /// Update Numunə: Dictionary.Add("FullName", "Tural Xasiyev");
    /// Where Numunə:  Dictionary.Add("WhereID", "1");
    /// </summary>
    public static int UpdateDatabase(Tools.Table TableName, Dictionary<string, object> Dictionary)
    {
        SqlCommand com = new SqlCommand();
        StringBuilder Columns = new StringBuilder();
        StringBuilder Where = new StringBuilder();
        string WhereColumnsName = "";
        try
        {
            foreach (var Item in Dictionary)
            {
                if (!Item.Key.ToUpper().Contains("WHERE"))
                {
                    Columns.Append(Item.Key + "=@" + Item.Key + " ,");
                    com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
                }
                else
                {
                    if (Where.Length < 1)
                        Where.Append("1=1");

                    if (Item.Key.ToUpper().Contains("INWHERE"))
                    {
                        WhereColumnsName = Item.Key.Substring(7);
                        Where.Append(string.Format(" and {0} in(Select item from FNSplitString(@wIn{0},','))", WhereColumnsName));
                        com.Parameters.AddWithValue("@wIn" + WhereColumnsName, Item.Value);
                    }
                    else
                    {
                        WhereColumnsName = Item.Key.Substring(5);
                        Where.Append(" and " + WhereColumnsName + "=@w" + WhereColumnsName);
                        com.Parameters.AddWithValue("@w" + WhereColumnsName, Item.Value);
                    }
                }
            }

            com.Connection = SqlConn;
            com.CommandText = string.Format("Update {0} SET {1} Where {2}", TableName, Columns.ToString().Trim(','), Where);
            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.UpdateDatabase [{0}] catch error: {1}", TableName, er.Message));
            return -1;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    /// <summary>
    /// Nümunə: UpdateDatabase("Persons", new string[] { "Soyad", "Ad", "WhereID" }, new object[] { "Novruzov", "Emin", 1 })
    /// </summary>
    public static int UpdateDatabase(Tools.Table TableName, string[] Key, object[] Values)
    {
        SqlCommand com = new SqlCommand();
        StringBuilder Columns = new StringBuilder();
        StringBuilder Where = new StringBuilder();
        string WhereColumnsName = "";
        try
        {
            for (int i = 0; i < Key.Length; i++)
            {
                if (!Key[i].ToUpper().Contains("WHERE"))
                {
                    Columns.Append(Key[i] + "=@" + Key[i] + " ,");
                    com.Parameters.AddWithValue("@" + Key[i], Values[i]);
                }
                else
                {
                    if (Where.Length < 1)
                        Where.Append("1=1");

                    if (Key[i].ToUpper().Contains("INWHERE"))
                    {
                        WhereColumnsName = Key[i].Substring(7);
                        Where.Append(string.Format(" and {0} in(Select item from FNSplitString(@wIn{0},','))", WhereColumnsName));
                        com.Parameters.AddWithValue("@wIn" + WhereColumnsName, Values[i]);
                    }
                    else
                    {

                        WhereColumnsName = Key[i].Substring(5);
                        Where.Append(" and " + WhereColumnsName + "=@w" + WhereColumnsName);
                        com.Parameters.AddWithValue("@w" + WhereColumnsName, Values[i]);
                    }
                }
            }

            com.Connection = SqlConn;
            com.CommandText = string.Format("Update {0} SET {1} Where {2}", TableName, Columns.ToString().Trim(','), Where);
            com.Connection.Open();
            return com.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.UpdateDatabase [{0}] catch error: {1}", TableName, er.Message));
            return -1;
        }
        finally
        {
            com.Connection.Close();
            com.Connection.Dispose();
            com.Dispose();
        }
    }

    public static int UpdateDatabase(Tools.Table TableName, Dictionary<string, object> Dictionary, Transaction Transaction, bool IsCommit = false)
    {
        int result = -1;
        StringBuilder Columns = new StringBuilder();
        StringBuilder Where = new StringBuilder();
        string WhereColumnsName = "";

        // Parametri təmizləyəkki ikinci dəfə eyni parametrlə insertə gəldikdə xəta verməsin
        Transaction.Com.Parameters.Clear();
        foreach (var Item in Dictionary)
        {
            if (!Item.Key.ToUpper().Contains("WHERE"))
            {
                Columns.Append(Item.Key + "=@" + Item.Key + " ,");
                Transaction.Com.Parameters.AddWithValue("@" + Item.Key, Item.Value);
            }
            else
            {
                if (Where.Length < 1)
                    Where.Append("1=1");


                if (Item.Key.ToUpper().Contains("INWHERE"))
                {
                    WhereColumnsName = Item.Key.Substring(7);
                    Where.Append(string.Format(" and {0} in(Select item from FNSplitString(@wIn{0},','))", WhereColumnsName));
                    Transaction.Com.Parameters.AddWithValue("@wIn" + WhereColumnsName, Item.Value);
                }
                else
                {

                    WhereColumnsName = Item.Key.Substring(5);
                    Where.Append(" and " + WhereColumnsName + "=@w" + WhereColumnsName);
                    Transaction.Com.Parameters.AddWithValue("@w" + WhereColumnsName, Item.Value);
                }
            }
        }

        try
        {
            if (Transaction.SqlTransaction == null)
            {
                Transaction.Com.Connection = SqlConn;
                Transaction.Com.Connection.Open();
                Transaction.SqlTransaction = Transaction.Com.Connection.BeginTransaction();
            }

            Transaction.Com.CommandText = String.Format("Update {0} SET {1} Where {2}", TableName, Columns.ToString().Trim(','), Where);
            Transaction.Com.Transaction = Transaction.SqlTransaction;
            result = Transaction.Com.ExecuteNonQuery();
            if (IsCommit)
            {
                Transaction.SqlTransaction.Commit();
            }

            return result;
        }
        catch (Exception er)
        {

            Transaction.SqlTransaction.Rollback();
            IsCommit = true;
            ErrorLogs(string.Format("DALC.UpdateDatabase [{0}] catch error: {1}", TableName, er.Message));
            return -1;
        }
        finally
        {
            if (IsCommit)
            {
                Transaction.Com.Connection.Close();
                Transaction.Com.Connection.Dispose();
                Transaction.Com.Dispose();
            }
        }
    }
    #endregion

    public static int ExecuteProcedure(string SqlCommand, string ParamsKeys = "", object[] ParamsValues = null)
    {
        string[] ParamsKeysArray = ParamsKeys.Split(',');

        DataTable Dt = new DataTable();
        using (SqlCommand com = new SqlCommand())
        {
            try
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Connection = SqlConn;
                com.Connection.Open();

                if (ParamsValues != null)
                {
                    if (ParamsKeysArray.Length != ParamsValues.Length)
                    {
                        throw new Exception("ParamsKeys and ParamsValues not same size.");
                    }

                    for (int i = 0; i < ParamsKeysArray.Length; i++)
                    {
                        com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
                    }
                }
                com.CommandText = SqlCommand;
                return com.ExecuteScalar()._ToInt32();

            }
            catch (Exception er)
            {

                ErrorLogs(string.Format("DALC.ExecuteProcedure {0} catch error: {1}", SqlCommand, er.Message));
                return -1;
            }
            finally
            {
                com.Connection.Close();
                com.Connection.Dispose();
                com.Dispose();
            }
        }
    }

    public static int ExecuteProcedure(string SqlCommand, Transaction Transaction, bool IsCommit = false, string ParamsKeys = "", object[] ParamsValues = null)
    {
        string[] ParamsKeysArray = ParamsKeys.Split(',');
        Transaction.Com.Parameters.Clear();
        DataTable Dt = new DataTable();
        using (SqlCommand com = new SqlCommand())
        {
            Transaction.Com.CommandType = CommandType.StoredProcedure;

            try
            {
                if (ParamsValues != null)
                {
                    if (ParamsKeysArray.Length != ParamsValues.Length)
                    {
                        throw new Exception("ParamsKeys and ParamsValues not same size.");
                    }

                    for (int i = 0; i < ParamsKeysArray.Length; i++)
                    {
                        Transaction.Com.Parameters.AddWithValue("@" + ParamsKeysArray[i], ParamsValues[i]);
                    }
                }

                if (Transaction.SqlTransaction == null)
                {
                    Transaction.Com.Connection = SqlConn;
                    Transaction.Com.Connection.Open();
                    Transaction.SqlTransaction = Transaction.Com.Connection.BeginTransaction();
                }
                Transaction.Com.CommandText = SqlCommand;
                Transaction.Com.Transaction = Transaction.SqlTransaction;
                int result = Transaction.Com.ExecuteNonQuery();

                if (IsCommit)
                {
                    Transaction.SqlTransaction.Commit();
                }
                return result;
            }
            catch (Exception er)
            {
                IsCommit = true;

                try
                {
                    Transaction.SqlTransaction.Rollback();
                }
                catch (Exception erRollback)
                {
                    ErrorLogs(string.Format("DALC.ExecuteProcedure {0} Rollback() catch error: {1}", SqlCommand, erRollback.Message));
                }

                ErrorLogs(string.Format("DALC.ExecuteProcedure {0} catch error: {1}", SqlCommand, er.Message));
                return -1;
            }
            finally
            {
                if (IsCommit)
                {
                    Transaction.SqlTransaction.Dispose();
                    Transaction.Com.Connection.Close();
                    Transaction.Com.Connection.Dispose();
                    Transaction.Com.Dispose();
                }
            }
        }
    }


    #endregion

    public static DataTable GetList(Tools.Table TableName, string Where = "Where IsActive=1 Order By Priority asc")
    {
        return GetDataTable("*", TableName, Where);
    }

    public static DataTable GetUsersPermissionsModules()
    {
        return GetDataTableBySqlCommand(string.Format("Select * From {0} Where (ID in(Select item from FNSplitString(@UsersPermissionsModules,',')) or IsPermission=0) and IsActive=1", Tools.Table.UsersPermissionsModules),
                                        new string[] { "UsersPermissionsModules" }, new object[] { _GetUsersLogin.UsersPermissionsModules });
    }

    public static bool IsPermission(Tools.UsersPermissionsModules UsersPermissionsModules)
    {
        return _GetUsersLogin.UsersPermissionsModules.IndexOf(UsersPermissionsModules.ToString("d")) > -1;
    }

    public static int CheckServicesCount(int ApplicationsPersonsID, int ServicesID)
    {
        return int.Parse(GetSingleValues("COUNT(*)", Tools.Table.ApplicationsPersonsServices, "ApplicationsPersonsID,ServicesID,IsActive", new object[] { ApplicationsPersonsID, ServicesID, true }));
    }

    public static DataTableResult GetUsersList(int Top, Dictionary<string, object> Dictionary)
    {
        DataTableResult GetUsersList = new DataTableResult();
        SqlCommand com = new SqlCommand();

        StringBuilder AddWhere = new StringBuilder("Where UsersStatusID!=@UsersStatusID");
        com.Parameters.Add("@UsersStatusID", SqlDbType.Int).Value = Tools.UsersStatus.Silinib;
        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                i++;
                // --Eger tarix araligi lazim olarsa--
                if (Item.Key.ToUpper().Contains("BETWEEN"))
                {
                    AddWhere.AppendFormat(" and " + Item.Key.ToUpper().Replace("BETWEEN", "") + " between @PDt1" + i.ToString() + " and @PDt2" + i.ToString());
                    com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Item.Value._ToString().Split('&')[0];
                    com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Item.Value._ToString().Split('&')[1];
                    continue;
                }
                else if (Item.Key.Contains("LIKE"))
                {
                    AddWhere.AppendFormat(" and " + Item.Key.Replace("LIKE", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                    com.Parameters.AddWithValue("@P" + i.ToString(), Item.Value);
                }
                else if (Item.Key.ToUpper().Contains("IN_"))
                {
                    AddWhere.Append(string.Format(" and {0} in(Select item from FNSplitString(@wIn{0},','))", Item.Key.Replace("IN_", "")));
                    com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("IN_", ""), Item.Value);
                }
                else if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    AddWhere.AppendFormat(" and {0}=@p{1}", Item.Key, i.ToString());
                    com.Parameters.AddWithValue("@p" + i.ToString(), Item.Value);
                }
            }
        }

        string QueryCommand = @"Select {0} From {1} {2} {3}";
        com.Connection = DALC.SqlConn;
        com.CommandText = string.Format(QueryCommand, "Count(*)", Tools.Table.V_Users, AddWhere, "");
        try
        {
            com.Connection.Open();
            GetUsersList.Count = com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            DALC.ErrorLogs("DALC.GetUsersList count xəta: " + er.Message);
            GetUsersList.Count = -1;
            GetUsersList.Dt = null;
            return GetUsersList;
        }
        finally
        {
            com.Connection.Close();
        }

        com.CommandText = string.Format(QueryCommand, "Top (@Top) *", Tools.Table.V_Users, AddWhere, "Order By ID desc");
        com.Parameters.Add("@Top", SqlDbType.Int).Value = Top;
        try
        {
            new SqlDataAdapter(com).Fill(GetUsersList.Dt);
            return GetUsersList;
        }
        catch (Exception er)
        {
            DALC.ErrorLogs("DALC.GetUsersList xəta: " + er.Message);
            GetUsersList.Count = -1;
            GetUsersList.Dt = null;
            return GetUsersList;
        }
    }

    public static DataTableResult GetUsers(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20)
    {
        DataTableResult UsersList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";

        StringBuilder AddWhere = new StringBuilder("Where 1=1");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from FNSplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(NOT IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} not in(Select item from FNSplitString(@wIn{0},','))", Item.Key.Replace("(NOT IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(NOT IN)", ""), Item.Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }
        AddWhere.AppendFormat(string.Format(" and UsersStatusID != {0}", (int)Tools.UsersStatus.Silinib));
        string QueryCommand = @"Select {0} From (Select Row_Number() over (Order By ID desc) as RowIndex, 
                                 *
                                 From V_Users as A {1} ) as A {2}";
        com.Connection = SqlConn;

        string CacheName = "UsersCount";

        if (HttpContext.Current.Cache[CacheName] == null)
        {
            com.CommandText = string.Format(QueryCommand, "COUNT(A.ID)", AddWhere, "");
            try
            {
                com.Connection.Open();
                UsersList.Count = com.ExecuteScalar()._ToInt32();
                HttpContext.Current.Cache[CacheName] = UsersList.Count;
            }
            catch (Exception er)
            {
                ErrorLogs("DALC.GetUsers count xəta: " + er.Message);
                UsersList.Count = -1;
                UsersList.Dt = null;
                return UsersList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            UsersList.Count = HttpContext.Current.Cache[CacheName]._ToInt32();
        }

        string RowIndexWhere = " Where A.RowIndex BETWEEN @R1 AND @R2";
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "A.*", AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(UsersList.Dt);
            return UsersList;
        }
        catch (Exception er)
        {
            ErrorLogs("DALC.GetUsers xəta: " + er.Message);
            UsersList.Count = -1;
            UsersList.Dt = null;
            return UsersList;
        }
    }

    public static DataTable GetUsersList()
    {
        return GetList(Tools.Table.Users, string.Format("Where UsersStatusID not in({0},{1}) Order By Fullname asc", (int)Tools.UsersStatus.İşdən_ayrılıb, (int)Tools.UsersStatus.Silinib));
    }

    public static DataTable GetUsersByOrganizationsID(int OrganizationsID)
    {
        return GetDataTable("ID,FullName", Tools.Table.Users, "UsersStatusID not in(90,95) and OrganizationsID", OrganizationsID);
    }

    public static DataTable GetUsersInfoByID(int DatatID)
    {
        return GetDataTable("*", Tools.Table.V_Users, "ID", DatatID);
    }

    public static DataTable GetUsersAccountsByID(int DatatID)
    {
        return GetDataTable("*", Tools.Table.V_UsersAccounts, "ID", DatatID);
    }

    public static DataTable CheckLoginExist(string Login)
    {
        return GetDataTable("*", Tools.Table.UsersAccounts, "Login", Login);
    }

    public static DataTable GetOrganizations()
    {
        int OrganizationsID = _GetUsersLogin.OrganizationsID;
        if (_GetUsersLogin.OrganizationsParentID == 0)
        {
            OrganizationsID = -1;
        }

        return GetDataTableBySqlCommand(string.Format("Select * From {0} Where (ID=@OrganizationsID or @OrganizationsID=-1) and IsActive=1", Tools.Table.Organizations),
                                   new string[] { "OrganizationsID" }, new object[] { OrganizationsID });
    }

    public static DataTable GetServices()
    {
        return GetDataTable("*", Tools.Table.Services, "Where IsActive=1 Order By ID asc");
    }

    public static DataTable GetServices(int ApplicationsPersonsID, int ParentID = 0)
    {
        return GetDataTableBySqlCommand(string.Format(@"Select *,(Select Count(ID) From {0} Where ParentID=S.ID) as SubServicesCount 
                                        From {0} as S 
                                        Where ParentID=@ParentID and 
                                        IsActive=@IsActive and
                                        ID not in(Select ServicesID From ApplicationsPersonsServices Where ApplicationsPersonsID=@ApplicationsPersonsID and IsActive=@IsActive)
                                        Order By Name asc",
                                        Tools.Table.Services),
                                        new string[] { "ApplicationsPersonsID", "ParentID", "IsActive" },
                                        new object[] { ApplicationsPersonsID, ParentID, true });
    }

    public static DataTable GetServicesByTypesID(int ServicesTypesID)
    {
        return GetDataTable("ID,Name", Tools.Table.Services, "ServicesTypesID", ServicesTypesID, "Order By Name asc");
    }

    public static DataTable GetServicesByPersonsID(int ApplicationsPersonsID)
    {
        return GetDataTable("*", Tools.Table.V_ApplicationsPersonsServices, "ApplicationsPersonsID", ApplicationsPersonsID, "Order By ID desc");
    }

    public static int GetSubServicesCountByID(int ID)
    {
        return int.Parse(GetSingleValues("Count(*)", Tools.Table.Services, "ParentID", ID, ""));
    }

    public static DataTable GetServicesPlans(int ServicesID)
    {
        return GetDataTable("*", Tools.Table.V_ServicesPlans, "IsActive=1 and ServicesID", ServicesID, "Order By Priority asc");
    }

    public static DataTable GetServicesCoursesPlans(int ServicesCoursesID, int ServicesID)
    {
        return GetDataTableBySqlCommand("GetServicesCoursesPlans", new string[] { "ServicesCoursesID", "ServicesID" }, new object[] { ServicesCoursesID, ServicesID }, CommandType.StoredProcedure);
    }

    public static DataTable GetServicesCoursesPlans(int ServicesCoursesID)
    {
        return GetDataTable("ID,Name", Tools.Table.V_ServicesCoursesPlans, "ServicesCoursesID", ServicesCoursesID);
    }

    public static DataTable GetServicesPlansByID(int ServicesPlansID)
    {
        return GetDataTable("*", Tools.Table.ServicesPlans, "ID", ServicesPlansID);
    }

    public static string GetSercicesPlansMaxPriority(int ServicesID)
    {
        return GetSingleValues("MAX(Priority)", Tools.Table.ServicesPlans, "IsActive=1 and ServicesID", ServicesID, "");
    }

    public static DataTable GetServicesForServicesCourses()
    {
        return GetDataTable("*", Tools.Table.V_ServicesForServicesCourses);
    }

    public static DataTable GetServicesCoursesByID(int ServicesCoursesID)
    {
        return GetDataTable("*", Tools.Table.ServicesCourses, "ID", ServicesCoursesID);
    }

    public static DataTable GetPersonsForServicesCourses(int OrganizationsID, int ServicesID, int ServicesCoursesTypesID)
    {
        return GetDataTableBySqlCommand("GetApplicationsPersonsServicesForServicesCourses", new string[] { "OrganizationsID", "ServicesID", "ServicesCoursesTypesID" }, new object[] { OrganizationsID, ServicesID, ServicesCoursesTypesID }, CommandType.StoredProcedure);
    }

    public static DataTable GetServicesCoursesPersons(int ServicesCoursesID)
    {
        return GetDataTable("*", Tools.Table.V_ServicesCoursesPersons, "ServicesCoursesID,IsActive", new object[] { ServicesCoursesID, true });
    }

    public static DataTable GetServicesCoursesLessons(int ServicesCoursesID)
    {
        return GetDataTable("*", Tools.Table.V_ServicesCoursesLessons, "ServicesCoursesID", ServicesCoursesID);
    }

    public static int GetLessonsIsComplated(int ServicesCoursesLessonsID)
    {
        return int.Parse(GetSingleValues("CAST(IsComplated as tinyint)", Tools.Table.ServicesCoursesLessons, "ID", ServicesCoursesLessonsID, ""));
    }

    public static int CheckServicesCoursesLessons(int ServicesCoursesID, string Create_Dt)
    {
        return int.Parse(GetSingleValues("Count(*)", Tools.Table.ServicesCoursesLessons, "ServicesCoursesID,Create_Dt", new object[] { ServicesCoursesID, Create_Dt }));
    }

    public static DataTableResult GetApplications(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20)
    {
        DataTableResult ApplicationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";
        string[] Query = { "IS NULL", "IS NOT NULL" };

        StringBuilder AddWhere = new StringBuilder("Where 1=1");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from FNSplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(NOT IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} not in(Select item from FNSplitString(@wIn{0},','))", Item.Key.Replace("(NOT IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(NOT IN)", ""), Item.Value);
                    }
                    else if (Array.IndexOf(Query, Value._ToString().ToUpper()) > -1)
                    {
                        AddWhere.AppendFormat(" and {0} {1}", Key, Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() over (Order By ID desc) as RowIndex, 
                                 *
                                 From {1} as A {2} ) as A {3}";
        com.Connection = SqlConn;

        string CacheName = "ApplicationsCount";

        if (HttpContext.Current.Cache[CacheName] == null)
        {
            com.CommandText = string.Format(QueryCommand, "COUNT(A.ID)", Tools.Table.V_Applications, AddWhere, "");
            try
            {
                com.Connection.Open();
                ApplicationsList.Count = com.ExecuteScalar()._ToInt32();
                HttpContext.Current.Cache[CacheName] = ApplicationsList.Count;
            }
            catch (Exception er)
            {
                ErrorLogs("DALC.GetApplications count xəta: " + er.Message);
                ApplicationsList.Count = -1;
                ApplicationsList.Dt = null;
                return ApplicationsList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            ApplicationsList.Count = HttpContext.Current.Cache[CacheName]._ToInt32();
        }

        string RowIndexWhere = " Where A.RowIndex BETWEEN @R1 AND @R2";
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "A.*", Tools.Table.V_Applications, AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(ApplicationsList.Dt);
            return ApplicationsList;
        }
        catch (Exception er)
        {
            ErrorLogs("DALC.GetApplications xəta: " + er.Message);
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
    }

    public static DataTableResult GetApplicationsPersons(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20)
    {
        DataTableResult ApplicationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";
        string[] Query = { "IS NULL", "IS NOT NULL" };

        StringBuilder AddWhere = new StringBuilder("Where 1=1");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from FNSplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else if (Array.IndexOf(Query, Value._ToString().ToUpper()) > -1)
                    {
                        AddWhere.AppendFormat(" and {0} {1}", Key, Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() over (Order By ApplicationsID desc, ID asc) as RowIndex, 
                                 *
                                 From {1} as A {2} ) as A {3}";
        com.Connection = SqlConn;

        string CacheName = "ApplicationsPersonsCount";

        if (HttpContext.Current.Cache[CacheName] == null)
        {
            com.CommandText = string.Format(QueryCommand, "COUNT(A.ID)", Tools.Table.V_ApplicationsPersons, AddWhere, "");
            try
            {
                com.Connection.Open();
                ApplicationsList.Count = com.ExecuteScalar()._ToInt32();
                HttpContext.Current.Cache[CacheName] = ApplicationsList.Count;
            }
            catch (Exception er)
            {
                ErrorLogs("DALC.GetApplications count xəta: " + er.Message);
                ApplicationsList.Count = -1;
                ApplicationsList.Dt = null;
                return ApplicationsList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            ApplicationsList.Count = HttpContext.Current.Cache[CacheName]._ToInt32();
        }

        string RowIndexWhere = " Where A.RowIndex BETWEEN @R1 AND @R2";
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "A.*", Tools.Table.V_ApplicationsPersons, AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(ApplicationsList.Dt);
            return ApplicationsList;
        }
        catch (Exception er)
        {
            ErrorLogs("DALC.GetApplications xəta: " + er.Message);
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
    }

    public static DataTableResult GetFilterList(Tools.Table TableName, Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20, string OrderBy = "Order By ID desc")
    {
        DataTableResult ApplicationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";
        string[] Query = { "IS NULL", "IS NOT NULL" };

        StringBuilder AddWhere = new StringBuilder("Where 1=1");
        StringBuilder ORWhere = new StringBuilder();
        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Value)) && Convert.ToString(Value) != "-1")
                {

                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        if (Key.ToUpper().Contains("(OR)"))
                        {
                            ORWhere.AppendFormat(" ({0} Between @PDt1{1} and @PDt2{1}) or", Key._Remove('('), i.ToString());
                        }
                        else
                        {
                            AddWhere.AppendFormat(" and ({0} Between @PDt1{1} and @PDt2{1})", Key._Remove('('), i.ToString());
                        }

                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        if (Key.ToUpper().Contains("(OR)"))
                        {
                            ORWhere.AppendFormat(" {0} Like '%' + @P{1} + '%' or", Key._Remove('('), i.ToString());
                        }
                        else
                        {
                            AddWhere.AppendFormat(" and {0} Like '%' + @P{1} + '%'", Key._Remove('('), i.ToString());
                        }
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Key.ToUpper().Contains("(IN)"))
                    {
                        if (Key.ToUpper().Contains("(OR)"))
                        {
                            ORWhere.AppendFormat(" {0} in(Select item from FNSplitString(@wIn{0},',')) or", Key._Remove('('));
                        }
                        else
                        {
                            AddWhere.AppendFormat(" and {0} in(Select item from FNSplitString(@wIn{0},','))", Key._Remove('('));
                        }
                        com.Parameters.AddWithValue("@wIn" + Key._Remove('('), Value);
                    }
                    else if (Key.Contains("(>=)"))
                    {
                        if (Key.ToUpper().Contains("(OR)"))
                        {
                            ORWhere.AppendFormat(" {0}>=@P{1} or", Key._Remove('('), i.ToString());
                        }
                        else
                        {
                            AddWhere.AppendFormat(" and {0}>=@P{1}", Key._Remove('('), i.ToString());
                            com.Parameters.AddWithValue(string.Format("@P{0}", i.ToString()), Value);
                        }

                    }
                    else if (Key.Contains("(<=)"))
                    {
                        if (Key.ToUpper().Contains("(OR)"))
                        {
                            ORWhere.AppendFormat(" {0}<=@P{1} or", Key._Remove('('), i.ToString());
                        }
                        else
                        {
                            AddWhere.Append(string.Format(" and {0}<=@P{1}", Key._Remove('('), i.ToString()));
                            com.Parameters.AddWithValue(string.Format("@P{0}", i.ToString()), Value);
                        }
                    }
                    else if (Key.Contains("(!=)"))
                    {
                        if (Key.ToUpper().Contains("(OR)"))
                        {
                            ORWhere.AppendFormat(" {0}!=@P{1} or", Key._Remove('('), i.ToString());
                        }
                        else
                        {
                            AddWhere.Append(string.Format(" and {0}!=@P{1}", Key._Remove('('), i.ToString()));
                            com.Parameters.AddWithValue(string.Format("@P{0}", i.ToString()), Value);
                        }
                    }
                    else if (Array.IndexOf(Query, Value._ToString().ToUpper()) > -1)
                    {
                        if (Key.ToUpper().Contains("(OR)"))
                        {
                            ORWhere.AppendFormat(" {0} {1} or", Key._Remove('('), Value);
                        }
                        else
                        {
                            AddWhere.AppendFormat(" and {0} {1}", Key, Value);
                        }
                    }
                    else
                    {
                        if (Key.ToUpper().Contains("(OR)"))
                        {
                            ORWhere.AppendFormat(" {0}=@P{1} or", Key._Remove('('), i.ToString());
                        }
                        else
                        {
                            AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        }
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }

            if (ORWhere.Length > 0)
            {
                AddWhere.AppendFormat(" and ({0})", ORWhere.Remove(ORWhere.ToString().Length - 2, 2));
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() over ({1}) as RowIndex, 
                                 *
                                 From {2} as T {3} ) as T {4}";
        com.Connection = SqlConn;


        com.CommandText = string.Format(QueryCommand, "COUNT(T.ID)", OrderBy, TableName, AddWhere, "");
        try
        {
            com.Connection.Open();
            ApplicationsList.Count = com.ExecuteScalar()._ToInt32();
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.GetFilterList TableName: {0} count xəta: {1}", TableName, er.Message));
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
        finally
        {
            com.Connection.Close();
        }

        string RowIndexWhere = " Where T.RowIndex BETWEEN @R1 AND @R2";
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "T.*", OrderBy, TableName, AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(ApplicationsList.Dt);
            return ApplicationsList;
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.GetFilterList TableName: {0} xəta: {1}", TableName, er.Message));
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
    }

    public static DataTable GetApplicationsFamilyByID(int ApplicationsFamilyID)
    {
        return DALC.GetDataTable("*", Tools.Table.ApplicationsFamily, "ID", ApplicationsFamilyID);
    }

    public static DataTable GetApplicationsFamilyUsers(int ApplicationsFamilyID)
    {
        return DALC.GetDataTable("ID,ApplicationsFamilyID,UsersID", Tools.Table.ApplicationsFamilyUsers, "IsDeleted=0 and ApplicationsFamilyID", ApplicationsFamilyID);
    }

    public static DataTable GetApplicationsFamilyPartners(int ApplicationsFamilyID)
    {
        return DALC.GetDataTable("ID,ApplicationsFamilyID,ApplicationsFamilyPartnersTypesID,PersonsFullname", Tools.Table.ApplicationsFamilyPartners, "IsDeleted=0 and ApplicationsFamilyID", ApplicationsFamilyID);
    }

    public static DataTable GetApplicantByApplicationsID(int ApplicationsID)
    {
        return GetDataTable("ApplicationsPersonsTypes,(Surname + ' ' + Name + ' ' + Patronymic) as FullName",
                            Tools.Table.V_ApplicationsPersons, "IsApplicant=1 and ApplicationsID", ApplicationsID);
    }

    public static DataTable GetApplicationsByID(int ApplicationsID, Tools.Table TabelName = Tools.Table.Applications)
    {
        return GetDataTableBySqlCommand(string.Format("Select * From {0} Where ID=@ApplicationsID", TabelName),
                                        new string[] { "ApplicationsID" }, new object[] { ApplicationsID });
    }

    public static DataTable GetApplicationsPersonsByID(int ApplicationsPersonsID, Tools.Table TabelName = Tools.Table.ApplicationsPersons)
    {
        return GetDataTableBySqlCommand(string.Format("Select * From {0} Where ID=@ApplicationsPersonsID and IsActive=1", TabelName),
                                        new string[] { "ApplicationsPersonsID" }, new object[] { ApplicationsPersonsID });
    }

    public static string GetApplicantPersons(int ApplicationsID)
    {
        return GetSingleValues("Concat(Surname,' ',Name,' ',Patronymic) as FullName", Tools.Table.ApplicationsPersons, "IsApplicant=1 and ApplicationsID", ApplicationsID, "", "");
    }

    public static string GetApplicationsPersonsFullName(int ApplicationsPersonsID)
    {
        return GetSingleValues("Concat(Surname,' ',Name,' ',Patronymic) as FullName", Tools.Table.ApplicationsPersons, "ID", ApplicationsPersonsID, "");
    }

    public static DataTable GetApplicationsPersonsServices(int ApplicationsID)
    {
        return GetDataTable("*", Tools.Table.V_ApplicationsPersonsServices, "IsActive=1 and ApplicationsID", ApplicationsID, "Order By ID desc");
    }

    public static DataTable GetApplicationsCaseByID(int ApplicationsPersonsID)
    {
        return GetDataTable("*", Tools.Table.ApplicationsCase, "ApplicationsPersonsID", ApplicationsPersonsID);
    }

    public static DataTable GetEvaluationsQuestions(int ParentID, int EvaluationsID)
    {
        return GetDataTableBySqlCommand(@"Select EQ.*,
                                                 EP.ID as EvaluationsPointsID,
                                                 EP.Point as PersonsPoint,
                                                 EP.Description
                                                 From EvaluationsQuestions as EQ Left Join
                                                 (Select * From EvaluationsPoints Where EvaluationsID=@EvaluationsID) as EP on EQ.ParentID = EP.EvaluationsQuestionsID 
                                                 Where ParentID=@ParentID",
                                                 new string[] { "ParentID", "EvaluationsID" },
                                                 new object[] { ParentID, EvaluationsID });
    }

    public static int GetEvaluationsCountByPersonsID(int ApplicationsPersonsID)
    {
        return int.Parse(GetSingleValues("Count(ID)", Tools.Table.Evaluations, "IsActive=1 and ApplicationsPersonsID", ApplicationsPersonsID, ""));
    }

    public static DataTable GetEvaluationsByPersonsID(int ApplicationsPersonsID)
    {
        return GetDataTable("*", Tools.Table.V_Evaluations, "IsActive=1 and ApplicationsPersonsID", ApplicationsPersonsID, "Order By ID desc");
    }

    public static DataTable GetEvaluationsByApplicationsID(int ApplicationsID)
    {
        return GetDataTable("*", Tools.Table.V_Evaluations, "ApplicationsID", ApplicationsID);
    }

    public static string CheckEvaluationsPoints(int EvaluationsID, int EvaluationsQuestionsID)
    {
        return GetSingleValues("ID", Tools.Table.EvaluationsPoints, "EvaluationsID,EvaluationsQuestionsID", new object[] { EvaluationsID, EvaluationsQuestionsID })._ToString();
    }

    public static bool CheckEvaluationsIsCompleted(int EvaluationsID)
    {
        int Count = GetSingleValues("COUNT(ID)", Tools.Table.Evaluations, "ID,IsCompleted", new object[] { EvaluationsID, true })._ToInt16();
        return Count > 0;
    }

    public static DataTable GetEvaluationsSkillByPersonsID(int ApplicationsPersonsID)
    {
        return GetDataTable("*", Tools.Table.V_EvaluationsSkill, "IsActive=1 and ApplicationsPersonsID", ApplicationsPersonsID, "Order By ID desc");
    }

    public static int CheckEvaluationsSkillByPersonsID(int ApplicationsPersonsID, string Create_Dt)
    {
        return int.Parse(GetSingleValues("COUNT(*)",
            Tools.Table.EvaluationsSkill,
            "ApplicationsPersonsID,Create_Dt,IsActive",
            new object[] { ApplicationsPersonsID, Create_Dt, true }
            ));
    }

    public static string CheckEvaluationsSkillIsCompleted(int ApplicationsPersonsID)
    {
        return GetSingleValues("ID", Tools.Table.EvaluationsSkill,
                "ApplicationsPersonsID,IsCompleted,IsActive",
                new object[] { ApplicationsPersonsID, false, true });
    }

    //public static string GetEvaluationsSkillCreateDt(int ApplicationsPersonsID)
    //{
    //    //Eger tamamlanmamish qiymetlendirme varsa
    //    return GetSingleValues("ID",
    //                            Tools.Table.EvaluationsSkill,
    //                            "ApplicationsPersonsID,IsCompleted,IsActive",
    //                            new object[] { ApplicationsPersonsID, false, true }, "Order by ID desc");
    //}

    public static DataTable GetEvaluationsSkillByID(int EvaluationsSkillID)
    {
        return GetDataTable("*", Tools.Table.EvaluationsSkill, "IsActive=1 and ID", EvaluationsSkillID);
    }

    public static int GetEvaluationsSkillCountByPersonsID(int ApplicationsPersonsID)
    {
        return int.Parse(GetSingleValues("Count(ID)", Tools.Table.EvaluationsSkill, "IsActive=1 and ApplicationsPersonsID", ApplicationsPersonsID, ""));
    }

    public static DataTableResult GetEvents(Dictionary<string, object> Dictionary, int PageNumber, int RowNumber = 20)
    {
        DataTableResult ApplicationsList = new DataTableResult();
        SqlCommand com = new SqlCommand();
        string Key = "";
        object Value = "";
        string[] Query = { "IS NULL", "IS NOT NULL" };

        StringBuilder AddWhere = new StringBuilder("Where 1=1");

        if (Dictionary != null)
        {
            int i = 0;
            foreach (var Item in Dictionary)
            {
                Key = Item.Key.ToUpper().Replace("İ", "I");
                Value = Item.Value;

                i++;
                if (!string.IsNullOrEmpty(Convert.ToString(Item.Value)) && Convert.ToString(Item.Value) != "-1")
                {
                    // --Eger tarix araligi lazim olarsa--
                    if (Key.Contains("(BETWEEN)"))
                    {
                        AddWhere.AppendFormat(" and (" + Key.Replace("(BETWEEN)", "") + " Between @PDt1" + i.ToString() + " and @PDt2" + i.ToString() + ")");
                        com.Parameters.Add("@PDt1" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[0];
                        com.Parameters.Add("@PDt2" + i.ToString(), SqlDbType.NVarChar).Value = Value._ToString().Split('&')[1] + " 23:59:59";
                    }
                    else if (Key.Contains("(LIKE)"))
                    {
                        AddWhere.AppendFormat(" and " + Key.Replace("(LIKE)", "") + " Like '%'+@P" + i.ToString() + "+'%'");
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }
                    else if (Item.Key.ToUpper().Contains("(IN)"))
                    {
                        AddWhere.Append(string.Format(" and {0} in(Select item from FNSplitString(@wIn{0},','))", Item.Key.Replace("(IN)", "")));
                        com.Parameters.AddWithValue("@wIn" + Item.Key.Replace("(IN)", ""), Item.Value);
                    }
                    else if (Array.IndexOf(Query, Value._ToString().ToUpper()) > -1)
                    {
                        AddWhere.AppendFormat(" and {0} {1}", Key, Value);
                    }
                    else
                    {
                        AddWhere.AppendFormat(" and {0}=@P{1}", Key, i.ToString());
                        com.Parameters.AddWithValue("@P" + i.ToString(), Value);
                    }

                }
            }
        }

        string QueryCommand = @"Select {0} From (Select Row_Number() over (Order By ID desc) as RowIndex, 
                                 *
                                 From {1} as E {2} ) as E {3}";
        com.Connection = SqlConn;

        string CacheName = "EventsCount";

        if (HttpContext.Current.Cache[CacheName] == null)
        {
            com.CommandText = string.Format(QueryCommand, "COUNT(E.ID)", Tools.Table.V_Events, AddWhere, "");
            try
            {
                com.Connection.Open();
                ApplicationsList.Count = com.ExecuteScalar()._ToInt32();
                HttpContext.Current.Cache[CacheName] = ApplicationsList.Count;
            }
            catch (Exception er)
            {
                ErrorLogs(string.Format("DALC.GetEvents count xəta: {0}", er.Message));
                ApplicationsList.Count = -1;
                ApplicationsList.Dt = null;
                return ApplicationsList;
            }
            finally
            {
                com.Connection.Close();
            }
        }
        else
        {
            ApplicationsList.Count = HttpContext.Current.Cache[CacheName]._ToInt32();
        }

        string RowIndexWhere = " Where E.RowIndex BETWEEN @R1 AND @R2";
        com.Parameters.Add("@R1", SqlDbType.Int).Value = ((PageNumber * RowNumber) - RowNumber) + 1;
        com.Parameters.Add("@R2", SqlDbType.Int).Value = PageNumber * RowNumber;
        com.CommandText = string.Format(QueryCommand, "E.*", Tools.Table.V_Events, AddWhere, RowIndexWhere);
        try
        {
            new SqlDataAdapter(com).Fill(ApplicationsList.Dt);
            return ApplicationsList;
        }
        catch (Exception er)
        {
            ErrorLogs(string.Format("DALC.GetEvents xəta: {0}", er.Message));
            ApplicationsList.Count = -1;
            ApplicationsList.Dt = null;
            return ApplicationsList;
        }
    }

    public static DataTable GetEventsByID(int EventsID)
    {
        return GetDataTable("*", Tools.Table.Events, "ID", EventsID);
    }

    public static DataTable GetApplicationsPersonsServicesUseHistory(int ApplicationsPersonsID, int ServicesID)
    {
        return GetDataTable("*", Tools.Table.V_ApplicationsPersonsServicesUseHistory, "ApplicationsPersonsID,ServicesID,IsActive", new object[] { ApplicationsPersonsID, ServicesID, true }, "Order By ID desc");
    }

    public static DataTable GetApplicationsPersonsServicesUseHistoryByID(int ServicesUseHistoryID)
    {
        return GetDataTable("*", Tools.Table.V_ApplicationsPersonsServicesUseHistory, "ID", new object[] { ServicesUseHistoryID });

    }

    public static DataTable GetDownloadsByID(int ID)
    {
        return GetDataTable("*", Tools.Table.Downloads, "ID", ID);
    }

    public static bool CheckDownloadsByName(string FileName, int DownloadsID)
    {
        return int.Parse(GetSingleValuesBySqlCommand(string.Format(@"Select Count(*) 
                                                     From {0} 
                                                     Where ID not in(@ID) and 
                                                     LOWER(FileName)=@FileName", Tools.Table.Downloads),
                                                     "ID,FileName", new object[] { DownloadsID, FileName.ToLower() })) > 0;
    }

    public static DataTable GetDownloadsByDataID(int DownloadsTypesID, int DataID)
    {
        return GetDataTableBySqlCommand(@"Select *,
                                            (Select COUNT(*) From Downloads Where DownloadsTypesID=@DownloadsTypesID and DataID=@DataID) as [Count] 
                                            From Downloads 
                                            Where DownloadsTypesID=@DownloadsTypesID and 
                                            DataID=@DataID",
                                            new string[] { "DownloadsTypesID", "DataID" },
                                            new object[] { DownloadsTypesID, DataID });
    }

    public static DataTable GetSIBR(int SIBRID)
    {
        return GetDataTable("*", Tools.Table.SIBR, "ID,SIBRStatusID", new object[] { SIBRID, (int)Tools.SIBRStatus.Aktiv });
    }

    public static int GetSIBRCountByApplicationsPersonsID(int ApplicationsPersonsID)
    {
        return int.Parse(GetSingleValues("Count(ID)", Tools.Table.SIBR, "SIBRStatusID,ApplicationsPersonsID", new object[] { (int)Tools.SIBRStatus.Aktiv, ApplicationsPersonsID }));
    }

    public static DataTable GetSIBRByApplicationsPersonsID(int ApplicationsPersonsID)
    {
        return GetDataTable("*", Tools.Table.V_SIBR, "SIBRStatusID,ApplicationsPersonsID",
            new object[] { (int)Tools.SIBRStatus.Aktiv, ApplicationsPersonsID }, "Order By ID desc");
    }

    public static DataTable GetSIBRScoringTypes()
    {
        return GetDataTable("*", Tools.Table.SIBRScoringTypes, "Where SIBRScoringTypesGroupsID not in(5) Order By ID asc");
    }

    public static DataTable GetSIBRScoringTypesGroups()
    {
        return GetDataTable("*", Tools.Table.SIBRScoringTypesGroups, "Order By ID asc");
    }

    public static DataTable GetSIBRScoring(Tools.SIBRTypes SIBRTypes, int ScoringTypesID, int RawScore)
    {
        return GetDataTable("*", Tools.Table.SIBRScoring, "SIBRTypesID,ScoringTypesID,RawScore", new object[] { (int)SIBRTypes, ScoringTypesID, RawScore });
    }

    public static DataTable GetSIBRNormFForFullScale(int AgeYear, int AgeMonth)
    {
        return GetDataTableBySqlCommand(string.Format(@"Select * From {0} 
                                                        Where SIBRNormFTypesID not in(1,2) and 
                                                        AgeYear=@AgeYear and 
                                                        AgeMonth=@AgeMonth", Tools.Table.SIBRNormF),
                                                        new string[] { "AgeYear", "AgeMonth" },
                                                        new object[] { AgeYear, AgeMonth });
    }

    public static DataTable GetSIBRNormFByTypesID(Tools.SIBRNormFTypes SIBRNormFTypesID, int AgeYear, int AgeMonth)
    {
        return GetDataTableBySqlCommand(string.Format(@"Select * From {0} 
                                                        Where SIBRNormFTypesID=@SIBRNormFTypesID and 
                                                        AgeYear=@AgeYear and 
                                                        AgeMonth=@AgeMonth", Tools.Table.SIBRNormF),
                                                        new string[] { "SIBRNormFTypesID", "AgeYear", "AgeMonth" },
                                                        new object[] { (int)SIBRNormFTypesID, AgeYear, AgeMonth });
    }

    public static DataTable GetSIBRNormG(int Columns, int DIFF)
    {

        if (DIFF > 0)
        {
            //Eger gonderilen DIFF Columns-a gore cedveldeki max deyerden boyukdurse ondan kicik axirinci melumati alaq
            //Mes: Columns=1 DIFF=29
            return GetDataTableBySqlCommand(string.Format(@"Select Top 1 * From {0} Where Columns=@Columns and 
                                                            (DIFF=@DIFF or DIFF<@DIFF) order by DIFF desc ",
                                                            Tools.Table.SIBRNormG),
                                                            new string[] { "Columns", "DIFF" }, new object[] { Columns, DIFF });
        }
        else
        {
            //Eger gonderilen DIFF Columns-a gore cedveldeki min deyerden kicikdirse ondan boyuk axirinci melumati alaq
            //Mes: Columns=51 DIFF=-35
            return GetDataTableBySqlCommand(string.Format(@"Select Top 1 * From {0} Where Columns=@Columns and 
                                                            (DIFF=@DIFF or DIFF>@DIFF) order by DIFF asc ",
                                                            Tools.Table.SIBRNormG),
                                                            new string[] { "Columns", "DIFF" }, new object[] { Columns, DIFF });
        }

        //return GetDataTable("*", Tools.Table.SIBRNormG, "Columns,DIFF", new object[] { Columns, DIFF });
    }

    public static DataTable GetSIBRNomrIForBroadIndependence(Tools.SIBRNormITypes SIBRNormITypes, int BI, int GMI)
    {
        //Cedveldeki en boyuk ve kicik reqeme uygun gelen deyerleri tenzimleyek
        if (BI < 394)
        {
            BI = 394;
        }
        else if (BI > 541)
        {
            BI = 541;
        }

        if (GMI > 4)
        {
            GMI = 4;
        }
        else if (GMI < -75)
        {
            GMI = -75;
        }

        return GetDataTableBySqlCommand(string.Format(@"Select Value From {0} Where 
                                        SIBRNormITypesID=@SIBRNormITypesID and
                                        Min<=@BI and Max>=@BI and GMI=@GMI", Tools.Table.SIBRNormI),
                                        new string[] { "SIBRNormITypesID", "BI", "GMI" },
                                        new object[] { (int)SIBRNormITypes, BI, GMI });
    }

    public static DataTable GetSIBRNormJ(int AgeYear, int AgeMonth)
    {
        return GetDataTableBySqlCommand(string.Format(@"Select REFW From {0} 
                                                        Where AgeYear=@AgeYear and 
                                                        AgeMonth=@AgeMonth Order by SIBRScoringTypesID asc",
                                                        Tools.Table.SIBRNormJ),
                                                        new string[] { "AgeYear", "AgeMonth" },
                                                        new object[] { AgeYear, AgeMonth });
    }

    public static DataTable GetSIBRAdaptiveResult(int SIBRID)
    {
        return GetDataTable("*", Tools.Table.V_SIBRAdaptiveResult, "SIBRID", SIBRID, "Order By SIBRScoringTypesGroupsID asc");
    }

    public static DataTable GetSIBRAdaptiveScores(int SIBRID, string Columns = "*")
    {
        return GetDataTable(Columns, Tools.Table.V_SIBRAdaptiveScores, "SIBRID", SIBRID, "Order By SIBRScoringTypesID asc");
    }

    public static DataTable GetSIBRAdaptiveSubscalesScores(int SIBRID)
    {
        return GetDataTable("*", Tools.Table.V_SIBRAdaptiveSubscalesScores, "SIBRID", SIBRID, "Order By SIBRScoringTypesID asc");
    }

    public static int GetSIBRAdaptiveSkillLevels(int DifferenceScores)
    {
        if (DifferenceScores > 13)
        {
            DifferenceScores = 13;
        }
        else if (DifferenceScores < -47)
        {
            DifferenceScores = -47;
        }

        return int.Parse(GetSingleValuesBySqlCommand(string.Format(@"Select ID From {0} Where DifferenceScoresMin<=@DifferenceScores and
                                                                     DifferenceScoresMax>=@DifferenceScores", Tools.Table.SIBRAdaptiveSkillLevels),
                                                                     "DifferenceScores", new object[] { DifferenceScores }));
    }

    public static string GetSIBRMaladaptiveSupportScore(int SupportScore)
    {
        if (SupportScore > 100)
        {
            SupportScore = 100;
        }
        else if (SupportScore < 1)
        {
            SupportScore = 1;
        }

        return GetSingleValuesBySqlCommand(string.Format(@"Select SupportLevel From {0} Where SupportScoreMin<=@SupportScore and 
                                                           SupportScoreMax>=@SupportScore", Tools.Table.SIBRMaladaptiveSupportScore),
                                                           "SupportScore", new object[] { SupportScore });
    }

    public static DataTable GetSIBRMaladaptiveScores(int SIBRID)
    {
        return GetDataTable("*", Tools.Table.SIBRMaladaptiveScores, "SIBRID", SIBRID);
    }

    public static DataTable GetSIBRAdaptiveResultForChart(int ApplicationsPersonsID)
    {
        return GetDataTable("SIBRScoringTypesGroupsID,ShortName,SumW,REFW,Create_Dt", Tools.Table.V_SIBRAdaptiveResult, "ApplicationsPersonsID", ApplicationsPersonsID);
    }

    public static DataTable BuildMenu(DataTable Services, DataTable SubServices)
    {
        DataTable NewDt = new DataTable();

        foreach (DataColumn Column in Services.Columns)
        {
            if (Services._Rows(Column.ColumnName).IsNumeric())
                NewDt.Columns.Add(Column.ColumnName, typeof(int));
            else
                NewDt.Columns.Add(Column.ColumnName, typeof(string));

        }

        foreach (DataRow Dr in Services.Rows)
        {
            BuildMenuSubMenu(SubServices, Dr, NewDt, 0);
        }

        return NewDt;

    }

    private static void BuildMenuSubMenu(DataTable SubServices, DataRow Dr, DataTable NewDt, int i)
    {
        DataRow[] DrChild = null;
        if (Dr["Priority"]._ToInt32() == 0)
        {
            DrChild = SubServices.Select("ParentID=" + Dr["ServicesID"]._ToString());
        }

        string nbsp = new string(' ', i);

        object[] NewDtRow = new object[NewDt.Columns.Count];
        int count = 0;
        foreach (DataColumn Column in SubServices.Columns)
        {
            NewDtRow[count] = Dr[Column.ColumnName];
            count++;
        }

        NewDt.Rows.Add(NewDtRow);

        if (DrChild != null)
        {
            foreach (DataRow DrNewChild in DrChild)
            {
                i += 4;
                BuildMenuSubMenu(SubServices, DrNewChild, NewDt, i);
                i -= 4;
            }
        }
    }

    public static DataTable GetDemographicInformationsDetailsTypes()
    {
        return GetDataTable("ID as DemographicInformationsTypesID,Name as DemographicInformationsTypesName,0 as [Count]", Tools.Table.DemographicInformationsTypes);
    }

    public static DataTable GetDemographicInformationsDetailsByID(int DemographicInformationsID)
    {
        return GetDataTable("*", Tools.Table.V_DemographicInformationsDetails, "ID", DemographicInformationsID);
    }

    public static DataTable GetApplicationsSocialStatusByApplicationsID(int ApplicationsID)
    {
        return GetDataTable("*", Tools.Table.V_ApplicationsSocialStatus, "ApplicationsID,IsDeleted", new object[] { ApplicationsID, false }, "Order By ID asc");
    }

    public static DataTable GetApplicationsSocialStatusNotIn(int ApplicationsID)
    {
        return GetDataTableBySqlCommand("Select * From SocialStatus " +
            "Where ID not in(Select SocialStatusID From ApplicationsSocialStatus Where ApplicationsID=@ApplicationsID and IsDeleted=0) " +
            "Order By Priority,Name",
            new string[] { "ApplicationsID" }, new object[] { ApplicationsID });
    }

    #region Reports

    public static DataTable GetYearsByTableName()
    {
        return GetDataTableBySqlCommand("GetApplicationsYears", null, null, CommandType.StoredProcedure);
    }

    public static DataTable GetMonthsByTableName(int Year)
    {
        return GetDataTableBySqlCommand("GetApplicationsMonths", new string[] { "Year" }, new object[] { Year }, CommandType.StoredProcedure);
    }

    public static DataTable GetReportsMonthly(string ProcedureName, string OrganizationsID, int Year, string Months)
    {
        return GetDataTableBySqlCommand(ProcedureName, new string[] { "OrganizationsID", "Year", "Months" }, new object[] { OrganizationsID, Year, Months }, CommandType.StoredProcedure);
    }

    public static DataTable GetReportsYearly(string ProcedureName, string OrganizationsID, string Years)
    {
        return GetDataTableBySqlCommand(ProcedureName, new string[] { "OrganizationsID", "Years" }, new object[] { OrganizationsID, Years }, CommandType.StoredProcedure);
    }

    public static DataTable GetReportsYearlyApplicationsPersonsServices(int ParentID, string OrganizationsID, string Year)
    {
        return GetDataTableBySqlCommand("ReportsYearlyApplicationsPersonsServices", new string[] { "ParentID", "OrganizationsID", "Years" }, new object[] { ParentID, OrganizationsID, Year }, CommandType.StoredProcedure);
    }

    public static DataTable GetReportsMonthlyApplicationsPersonsServices(int ParentID, string OrganizationsID, int Year, string Months)
    {
        return GetDataTableBySqlCommand("ReportsMonthlyApplicationsPersonsServices", new string[] { "ParentID", "OrganizationsID", "Year", "Months" }, new object[] { ParentID, OrganizationsID, Year, Months }, CommandType.StoredProcedure);
    }

    public static DataTable GetReportsYearlyApplicationsForOrganizations(string Years)
    {
        return GetDataTableBySqlCommand("ReportsYearlyApplicationsForOrganizations", new string[] { "Years" }, new object[] { Years }, CommandType.StoredProcedure);
    }

    public static DataTable GetReportsMonthlyApplicationsForOrganizations(string OrganizationsID, int Year, string Months)
    {
        return GetDataTableBySqlCommand("ReportsMonthlyApplicationsForOrganizations", new string[] { "OrganizationsID", "Year", "Months" }, new object[] { OrganizationsID, Year, Months }, CommandType.StoredProcedure);
    }

    public static DataTable GetReportsYearlySosialStatus(string Years)
    {
        return GetDataTableBySqlCommand("ReportsYearlySosialStatus", new string[] { "Years" }, new object[] { Years }, CommandType.StoredProcedure);
    }

    public static DataTable GetReportsMonthlySosialStatus(string Months)
    {
        return GetDataTableBySqlCommand("ReportsMonthlySosialStatus", new string[] { "Months" }, new object[] { Months }, CommandType.StoredProcedure);
    }

    #endregion

    public static int InsertOrUpdateApplicationsFamilyUsersOrPartners(Tools.Table TableName, Dictionary<string, object> Dictionary, bool IsUpdate, Transaction Transaction)
    {
        int Result = 0;

        if (IsUpdate)
        {
            List<string> Key = new List<string>(Dictionary.Keys);
            Dictionary.Add(string.Format("Where{0}", Key[0]), Dictionary[Key[0]]._ToInt32());
            Dictionary.Add(string.Format("Where{0}", Key[1]), Dictionary[Key[1]]._ToInt32());
            Result = UpdateDatabase(TableName, Dictionary, Transaction);
        }
        else
        {
            Result = InsertDatabase(TableName, Dictionary, Transaction);
        }

        if (Result < 1)
        {
            return -1;
        }
        return 1;
    }

    //Error log insert
    public static void ErrorLogs(string LogText, bool IsSendMail = false)
    {
        Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        Dictionary.Add("LogText", Config.SizeLimit(LogText, 990, "..."));
        Dictionary.Add("URL", Config.SizeLimit(HttpContext.Current.Request.Url.ToString(), 90, "..."));
        Dictionary.Add("Add_Dt", DateTime.Now);
        Dictionary.Add("Add_Ip", HttpContext.Current.Request.UserHostAddress.IPToInteger());

        InsertDatabase(Tools.Table.ErrorLogs, Dictionary, false);

        //Adminə həmdə mail göndərmək lazım olduqda getsin.
        if (IsSendMail)
        {
            DALC.SendAdminMail("CES", "[ErrorLogs] " + LogText);
        }
    }

    //Send admin mail sender
    public static void SendAdminMail(string Title, string Messages)
    {
        DALC.SendMail(Config._GetAppSettings("ErrorMailList"), Title, Messages + " Ip: " + HttpContext.Current.Request.UserHostAddress + " DateTime: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + " Url: " + HttpContext.Current.Request.Url.ToString());
    }

    //Mail sender.
    public static bool SendMail(string MailList, string Title, string Messages)
    {
        try
        {
            return true;
            //Mail gonder
            MailMessage MailServer = new MailMessage(Config._GetAppSettings("MailLogin"), MailList, Title, Messages);

            SmtpClient SmtpMail = new SmtpClient(Config._GetAppSettings("MailServer"));
            SmtpMail.Credentials = new NetworkCredential(Config._GetAppSettings("MailLogin"), Config._GetAppSettings("MailPassword").Decrypt());
            SmtpMail.EnableSsl = true;
            SmtpMail.Port = 587;

            MailServer.BodyEncoding = System.Text.Encoding.UTF8;
            MailServer.Priority = System.Net.Mail.MailPriority.Normal;
            MailServer.IsBodyHtml = true;

            SmtpMail.Send(MailServer);
            return true;
        }
        catch (Exception er)
        {
            //Əgər log da error veribsə mail göndərirsə təkrar log metoduna qaytarmıyaq.
            if (Messages.IndexOf("[ErrorLogs]") < 0)
                DALC.ErrorLogs("DALC.SendMail catch error: " + er.Message);

            return false;
        }
    }

}