using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public static class Config
{
    public static string _DefaultErrorMessages
    {
        get
        {
            return "Sistemdə yüklənmə var. Daha sonra cəhd edin.";
        }
    }

    public static string _DefaultSuccessMessages
    {
        get
        {
            return "Əməliyyat uğurla yerinə yetirildi.";
        }
    }

    public static string GetExtension(this object Path)
    {
        return System.IO.Path.GetExtension(Path._ToString()).Trim('.').ToLower();
    }

    //Get WebConfig.config App Key
    public static string _Route(string KeyName, string CatchValue = "")
    {
        try
        {
            Page P = (Page)HttpContext.Current.Handler;
            return P.RouteData.Values[KeyName].ToString().ToLower();
        }
        catch
        {
            return CatchValue;
        }
    }

    //Get WebConfig.config App Key
    public static string _GetAppSettings(string KeyName)
    {
        return ConfigurationManager.AppSettings[KeyName];
    }

    //Get Querystring
    public static string _GetQueryString(string KeyName)
    {
        return HttpContext.Current.Request.QueryString[KeyName]._ToString();
    }

    //ConvertString.
    public static string _ToString(this object Value)
    {
        return Convert.ToString(Value);
    }

    //ConvertString.
    public static int _ToInt16(this object Value)
    {
        return Convert.ToInt16(Value);
    }

    //ConvertString.
    public static int _ToInt32(this object Value)
    {
        return Convert.ToInt32(Value);
    }

    //ConvertString.
    public static long _ToInt64(this object Value)
    {
        return Convert.ToInt64(Value);
    }

    public static string MoreButton(this string Text)
    {
        return string.Format("<img src=\"/images/more.png\" alt=\"More button icon\"/> {0}", Text);
    }

    //ID file name encrypt
    public static string EncryptFilename(this string ID)
    {
        ID = ID + "_" + SHA1Special(ID + Config._GetAppSettings("FileEncryptKey").Decrypt());
        ID = ID.Replace(" ", "-");
        ID = ID.Replace(",", "-");
        ID = ID.Replace("\"", "");
        ID = ID.Replace("/", "");
        ID = ID.Replace("\\", "");
        ID = ID.Replace("+", "");
        ID = ID.Replace("-", "");
        ID = ID.Replace("$", "");
        ID = ID.Replace("#", "");
        ID = ID.Replace(".", "");
        ID = ID.Replace("=", "");
        return ID;
    }

    public static string UploadsImagePath(string FolderName, object FolderOfDate, object FileName, object FileType)
    {
        return string.Format("/uploads/{0}/{1}/{2}/{3}{4}", FolderName, ((DateTime)FolderOfDate).ToString("yyyy"), ((DateTime)FolderOfDate).ToString("MM"), FileName, FileType);
    }

    //Email validator
    public static bool IsEmail(this string Mail)
    {
        const string StrRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,6}|[0-9]{1,3})(\]?)$";

        return (new System.Text.RegularExpressions.Regex(StrRegex)).IsMatch(Mail);
    }

    public static string DateTimeClear(string Data, string ReplaceChar)
    {
        Data = Data.Replace(" ", ReplaceChar);
        Data = Data.Replace(",", ReplaceChar);
        Data = Data.Replace("\"", ReplaceChar);
        Data = Data.Replace("/", ReplaceChar);
        Data = Data.Replace("+", ReplaceChar);
        Data = Data.Replace("-", ReplaceChar);
        Data = Data.Replace("$", ReplaceChar);
        Data = Data.Replace("#", ReplaceChar);
        Data = Data.Replace("=", ReplaceChar);
        Data = Data.Replace("*", ReplaceChar);
        Data = Data.Replace(":", ReplaceChar);
        Data = Data.Replace(".", ReplaceChar);
        return Data;
    }

    //Tarix təmizləyən
    public static object DateTimeFormat(this string Date)
    {
        //Clear
        Date = Date.Trim();
        Date = Date.Replace(",", ".");
        Date = Date.Replace("+", ".");
        Date = Date.Replace("/", ".");
        Date = Date.Replace("-", ".");
        Date = Date.Replace("*", ".");
        Date = Date.Replace("\\", ".");
        Date = Date.Replace(" ", ".");

        if (!IsNumeric(Date.Replace(".", "")))
            return null;

        string[] DtSplit = Date.Split('.');

        if (DtSplit.Length != 3)
            return null;

        //Əgər 2050 keçərsə 1900 yazaq.
        try
        {
            //İli 2 simvol olsa yanına 20 artıq. 3 minici ilə qədər gedər :)
            if (DtSplit[2].Length == 2)
                DtSplit[2] = "20" + DtSplit[2];

            if (DtSplit[2].Length == 1)
                DtSplit[2] = "200" + DtSplit[2];

            if (DtSplit[2]._ToInt16() > 2050)
                DtSplit[2] = (DtSplit[2]._ToInt16() - 100).ToString();

            DateTime Dt = new DateTime(
                int.Parse(DtSplit[2]),
                int.Parse(DtSplit[1]),
                int.Parse(DtSplit[0])
                );

            return Dt;
        }
        catch
        {
            return null;
        }
    }


    //Get WebConfig.config App Key
    public static string Split(string Value, char Char, int Index, string CatchValue = "0")
    {
        try
        {
            return Value.Split(Char)[Index];
        }
        catch
        {
            return CatchValue;
        }
    }

    //Açar yaradaq.
    public static string Key(int say)
    {
        Random Rnd = new Random();
        string Bind = "aqwertyuipasdfghjkzxcvbnmQAZWSXEDCRFVTGBYHNUJMKP23456789";
        string Key = "";
        for (int i = 1; i <= say; i++)
        {
            Key += Bind.Substring(Rnd.Next(Bind.Length - 1), 1);
        }
        return Key.Trim();
    }

    //Cümlə çox uzun olanda üç nöqtə qoyaq.
    public static string SizeLimit(object Text, int Length, string More)
    {
        if (Text._ToString().Length > Length)
            Text = Text._ToString().Substring(0, Length) + More;
        return Text._ToString();
    }

    //Numaric testi.
    public static bool IsNumeric(this object Value)
    {
        if (string.IsNullOrEmpty(Value._ToString()))
            return false;

        for (int i = 0; i < Value._ToString().Length; i++)
        {
            if ("0123456789".IndexOf(Value._ToString().Substring(i, 1)) < 0)
            {
                return false;
            }
        }
        return true;
    }

    //Ajax error message
    public static void MsgBoxAjax(string Text, string Href = "")
    {
        if (!string.IsNullOrEmpty(Href))
        {
            Href = "location.href = '" + Href + "';";
        }

        Page P = (Page)HttpContext.Current.Handler;
        ScriptManager.RegisterStartupScript(P, P.Page.GetType(), "PopupScript", string.Format("window.focus(); alert('{0}');{1}", Text, Href), true);
    }

    //Sha1 - özəl
    public static string SHA1Special(this string Value)
    {
        byte[] result;
        System.Security.Cryptography.SHA1 ShaEncrp = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        Value = String.Format("{0}{1}{0}", "CSAASADM", Value);
        byte[] buffer = new byte[Value.Length];
        buffer = System.Text.Encoding.UTF8.GetBytes(Value);
        result = ShaEncrp.ComputeHash(buffer);
        return Convert.ToBase64String(result);
    }

    public static string SubString(object Value, int Start, int Length, string CatchValue = "-1")
    {
        try
        {
            return Value._ToString().Substring(Start, Length);
        }
        catch
        {
            return CatchValue;
        }
    }

    //Səhifəni yönləndirək:
    public static void RedirectURL(string GetUrl)
    {
        HttpContext.Current.Response.Redirect(GetUrl, false);
        HttpContext.Current.Response.End();
    }

    public static void RedirectLogin(bool IsReturnUrl = true)
    {
        HttpContext.Current.Response.Redirect(string.Format("~/?return={0}", (IsReturnUrl ? HttpContext.Current.Request.Path : "")), false);
        HttpContext.Current.Response.End();
    }

    public static void RedirectError()
    {
        HttpContext.Current.Response.Redirect("~/error", false);
        HttpContext.Current.Response.End();
    }

    //Tarixləri dolduraq:
    public static System.Data.DataTable NumericList(int From, int To, string BlankInsertValue = "0", string BlankInsertText = "--")
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("Name", typeof(string));

        if (BlankInsertValue.Length > 0)
            dt.Rows.Add(BlankInsertValue, BlankInsertText);

        for (int i = From; i <= To; i++)
        {
            dt.Rows.Add((i < 10 ? "0" + i.ToString() : i.ToString()), (i < 10 ? "0" + i.ToString() : i.ToString()));
        }
        return dt;
    }

    public static string _Rows(this System.Data.DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt.Rows == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogs(string.Format("Rows null or count error.  ColumnsName: {0}", ColumnsName));
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return Dt.Rows[RowsIndex][ColumnsName]._ToString();
        }
    }

    public static decimal _RowsDecimal(this DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogs(string.Format("Rows null or count error.  ColumnsName: {0}", ColumnsName));
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return (decimal)Dt.Rows[RowsIndex][ColumnsName];
        }
    }

    public static DateTime _RowsDatetime(this DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogs(string.Format("Rows null or count error.  ColumnsName: {0}", ColumnsName));
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return (DateTime)Dt.Rows[RowsIndex][ColumnsName];
        }
    }

    public static TimeSpan _RowsTime(this DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogs(string.Format("Rows null or count error.  ColumnsName: {0}", ColumnsName));
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return (TimeSpan)Dt.Rows[RowsIndex][ColumnsName];
        }
    }

    public static int _RowsInt(this DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogs(string.Format("Rows null or count error.  ColumnsName: {0}", ColumnsName));
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return Dt.Rows[RowsIndex][ColumnsName]._ToInt32();
        }
    }

    public static long _RowsInt64(this DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogs(string.Format("Rows null or count error.  ColumnsName: {0}", ColumnsName));
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return Dt.Rows[RowsIndex][ColumnsName]._ToInt64();
        }
    }

    public static bool _RowsBoolean(this DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogs(string.Format("Rows null or count error.  ColumnsName: {0}", ColumnsName));
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return (bool)Dt.Rows[RowsIndex][ColumnsName];
        }
    }

    public static object _RowsObject(this System.Data.DataTable Dt, string ColumnsName, int RowsIndex = 0)
    {
        if (Dt.Rows == null || Dt.Rows.Count < 1)
        {
            DALC.ErrorLogs(string.Format("Rows null or count error.  ColumnsName: {0}", ColumnsName));
            throw new Exception("Rows null or count error.  ColumnsName: " + ColumnsName);
        }
        else
        {
            return Dt.Rows[RowsIndex][ColumnsName];
        }
    }

    //Mobile number control only 9 simvol
    public static string IsMobileNumberControl(string Number, bool IsMobileOperationType)
    {
        try
        {
            Number = Number.Trim('+').TrimStart('0').Replace(" ", "").Replace("-", "").Replace("/", "").Replace(",", "").Trim().TrimStart('0');

            if (Number.Length > 9 && Number.Substring(0, 3) == "994")
                Number = Number.Substring(3);

            if (!Number.IsNumeric() || Number.Length != 9)
                return "-1";

            string Typ = Number.Substring(0, 2);
            if (IsMobileOperationType)
            {
                if (Config._GetAppSettings("MobileOperationTypes").IndexOf(Typ) < 0)
                {
                    return "-1";
                }
            }

            return Number;
        }
        catch
        {
            return "-1";
        }
    }

    public static bool CheckFileContentLength(this HttpPostedFile File, int ValueMB = 10)
    {
        if ((File.ContentLength / 1024) > ValueMB * 1024)
        {
            return false;
        }
        return true;
    }

    public static bool CheckIfFileIsExecutable(this HttpPostedFile File)
    {
        try
        {
            var firstTwoBytes = new byte[2];

            File.InputStream.Read(firstTwoBytes, 0, 2);

            return Encoding.UTF8.GetString(firstTwoBytes) == "MZ";
        }
        catch
        {

        }
        return false;
    }

    public static string TitleUpperCase(this object inText)
    {
        string Text = inText._ToString();

        Text = Text.Replace("i", "İ");
        Text = Text.Replace("ı", "I");

        return Text.ToUpper();
    }

    //Set title to url (clear latin and special simvols)
    public static string ClearTitle(this object inText)
    {
        string Text = inText._ToString();

        Text = Text.ToLower().Trim().Trim('-').Trim('.').Trim();
        Text = Text.Replace("-", " ");
        Text = Text.Replace("–", " ");
        Text = Text.Replace("   ", " ");
        Text = Text.Replace("  ", " ");
        Text = Text.Replace("   ", " ");
        Text = Text.Replace("  ", " ");
        Text = Text.Replace("   ", " ");
        Text = Text.Replace(" ", "-");
        Text = Text.Replace(",", "");
        Text = Text.Replace("\"", "");
        Text = Text.Replace("/", "");
        Text = Text.Replace("\\", "");
        Text = Text.Replace("“", "");
        Text = Text.Replace("”", "");
        Text = Text.Replace("'", "");
        Text = Text.Replace("+", "");
        Text = Text.Replace(":", "");
        Text = Text.Replace(";", "");
        Text = Text.Replace(".", "");
        Text = Text.Replace(",", "");
        Text = Text.Replace("?", "");
        Text = Text.Replace("!", "");
        Text = Text.Replace(">", "");
        Text = Text.Replace("<", "");
        Text = Text.Replace("%", "");
        Text = Text.Replace("$", "");
        Text = Text.Replace("*", "");
        Text = Text.Replace("(", "");
        Text = Text.Replace(")", "");

        Text = Text.Replace("`", "");
        Text = Text.Replace("«", "");
        Text = Text.Replace("»", "");

        Text = Text.Replace("@", "");
        Text = Text.Replace("---", "-");
        Text = Text.Replace("--", "-");
        Text = Text.Replace("#", "");
        Text = Text.Replace("&", "");

        //No Latin
        Text = Text.Replace("ü", "u");
        Text = Text.Replace("ı", "i");
        Text = Text.Replace("ö", "o");
        Text = Text.Replace("ğ", "g");
        Text = Text.Replace("ə", "e");
        Text = Text.Replace("ç", "ch");
        Text = Text.Replace("ş", "sh");

        Text = Text.Replace("\n", " ");
        Text = Text.Replace(((char)13).ToString(), " ");
        Text = Text.Trim();

        return Text;
    }

    public static string ClearSymbols(string input)
    {
        Regex Rgx = new Regex("[^a-zA-Z0-9]");
        return Rgx.Replace(input, "");
    }

    public static int OnlyNumber(this string input)
    {
        return Regex.Matches(input, @"\d+").OfType<Match>().Select(m => int.Parse(m.Value)).ToArray()[0];
    }

    public static string ClearBasic(this string Text)
    {
        Text = Text.Replace(">", "");
        Text = Text.Replace("<", "");
        Text = Text.Replace("“", "");
        Text = Text.Replace("”", "");

        return Text;
    }

    public static int IPToInteger(this string IP)
    {
        return BitConverter.ToInt32(System.Net.IPAddress.Parse(IP).GetAddressBytes(), 0);
    }

    public static string IntegerToIP(this int IntegerIP)
    {
        return new System.Net.IPAddress(BitConverter.GetBytes(IntegerIP)).ToString();
    }

    public static string ToDescriptionString(this Enum Value)
    {
        FieldInfo Fi = Value.GetType().GetField(Value.ToString());

        DescriptionAttribute[] Attributes = (DescriptionAttribute[])Fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (Attributes != null && Attributes.Length > 0)
            return Attributes[0].Description;
        else
            return Value.ToString();
    }

    public static void Modal(string Type = "show", string ModalName = "Modal")
    {
        Page P = (Page)HttpContext.Current.Handler;
        ScriptManager.RegisterStartupScript(P, P.GetType(), "JqueryModal", string.Format("$('#{0}').modal('{1}');", ModalName, Type), true);
    }

    public static void Calendar()
    {
        Page P = (Page)HttpContext.Current.Handler;
        ScriptManager.RegisterClientScriptBlock(P, P.GetType(), "calendar", "$('.form_datetime').datetimepicker({format: 'dd.mm.yyyy',language: 'en',weekStart: 1,todayBtn: 1,autoclose: 1,todayHighlight: 1,startView: 2,minView: 2,forceParse: 0});", true);
    }

    public static string GetMonthName(this int Month)
    {
        switch (Month)
        {
            case 1:
                return "Yanvar";
            case 2:
                return "Fevral";
            case 3:
                return "Mart";
            case 4:
                return "Aprel";
            case 5:
                return "May";
            case 6:
                return "İyun";
            case 7:
                return "İyul";
            case 8:
                return "Avqust";
            case 9:
                return "Sentyabr";
            case 10:
                return "Oktyabr";
            case 11:
                return "Noyabr";
            case 12:
                return "Dekabr";
            default:
                return "";
        }
    }

    public static string GetMonthShortName(int Month)
    {
        switch (Month)
        {
            case 1:
                return "Yan.";
            case 2:
                return "Fev.";
            case 3:
                return "Mar.";
            case 4:
                return "Apr.";
            case 5:
                return "May.";
            case 6:
                return "İyu.";
            case 7:
                return "İyu.";
            case 8:
                return "Avq.";
            case 9:
                return "Sen.";
            case 10:
                return "Okt.";
            case 11:
                return "Noy.";
            case 12:
                return "Dek.";
            default:
                return "";
        }
    }

    ////////////////////////////////////////////

    public static string _Remove(this string Value, char StartCharacter)
    {
        return Value.Remove(Value.IndexOf(StartCharacter));
    }

    public static string DateTimeFilter(string DateStart, string DateEnd)
    {
        string Date = "";

        string Dt1 = "20000101";
        string Dt2 = DateTime.Now.ToString("yyyyMMdd");

        object DateFilter1 = DateTimeFormat(DateStart.Trim());
        object DateFilter2 = DateTimeFormat(DateEnd.Trim());

        if (DateFilter1 == null && DateFilter2 == null)
        {
            Date = "";
        }
        else
        {
            if (DateFilter1 != null)
            {
                Dt1 = ((DateTime)DateFilter1).ToString("yyyyMMdd");
            }

            if (DateFilter2 != null)
            {
                Dt2 = ((DateTime)DateFilter2).ToString("yyyyMMdd");
            }

            Date = Dt1 + "&" + Dt2;
        }

        return Date;
    }

    public static void ClearControls(this Control Panel)
    {
        foreach (Control item in Panel.Controls)
        {
            if (item.HasControls())
            {
                ClearControls(item);
            }

            if (item is TextBox)
            {
                (item as TextBox).Text = "";
            }
            else if (item is DropDownList)
            {
                (item as DropDownList).SelectedIndex = 0;
            }
            else if (item is ListBox)
            {
                ListBox LitBx = item as ListBox;
                for (int i = 0; i < LitBx.Items.Count; i++)
                {
                    LitBx.Items[i].Selected = false;
                }
            }
            else if (item is CheckBox)
            {
                (item as CheckBox).Checked = false;
            }
        }
    }

    public static void BindControls(this Control Panel, Dictionary<string, object> FilterDictionary, Tools.Table TableName, bool ClearSession = false)
    {
        string SessionName = TableName._ToString();
        if (ClearSession)
        {
            HttpContext.Current.Session[SessionName] = null;
        }
        if (HttpContext.Current.Session[SessionName] != null)
        {
            if (FilterDictionary.Count < 1)
            {
                FilterDictionary = (Dictionary<string, object>)HttpContext.Current.Session[SessionName];
            }
            foreach (Control item in Panel.Controls)
            {
                if (item.HasControls())
                {
                    BindControls(item, FilterDictionary, TableName);
                }

                try
                {
                    if (item is TextBox)
                    {
                        TextBox Txt = item as TextBox;
                        Txt.Text = FilterDictionary[Txt.ID]._ToString();
                    }
                    else if (item is DropDownList)
                    {
                        DropDownList DList = item as DropDownList;
                        DList.SelectedValue = FilterDictionary[DList.ID]._ToString();
                    }
                    else if (item is ListBox)
                    {
                        ListBox LstBx = item as ListBox;
                        for (int i = 0; i < LstBx.Items.Count; i++)
                        {
                            ListItem Item = LstBx.Items[i];
                            if (FilterDictionary[LstBx.ID]._ToString().IndexOf(Item.Value) > -1)
                            {
                                Item.Selected = true;
                            }
                        }
                    }
                    else if (item is CheckBox)
                    {
                        CheckBox Check = item as CheckBox;
                        Check.Checked = (bool)FilterDictionary[Check.ID];
                    }
                }
                catch
                {
                    HttpContext.Current.Session[SessionName] = null;
                    FilterDictionary.Clear();
                    BindControls(Panel, FilterDictionary, TableName);
                }
            }
        }
        else
        {
            foreach (Control item in Panel.Controls)
            {
                if (item.HasControls())
                {
                    BindControls(item, FilterDictionary, TableName, true);
                }

                if (item is TextBox)
                {
                    TextBox Txt = item as TextBox;
                    FilterDictionary.Add(Txt.ID, Txt.Text);
                }
                else if (item is DropDownList)
                {
                    DropDownList DList = item as DropDownList;
                    FilterDictionary.Add(DList.ID, DList.SelectedValue);
                }
                else if (item is ListBox)
                {
                    ListBox LstBx = item as ListBox;
                    StringBuilder Values = new StringBuilder();
                    for (int i = 0; i < LstBx.Items.Count; i++)
                    {
                        ListItem Item = LstBx.Items[i];
                        if (Item.Selected)
                        {
                            Values.Append(Item.Value + ",");
                        }
                    }
                    FilterDictionary.Add(LstBx.ID, Values._ToString().Trim(','));
                }
                else if (item is CheckBox)
                {
                    CheckBox Check = item as CheckBox;
                    FilterDictionary.Add(Check.ID, Check.Checked);
                }

            }
            HttpContext.Current.Session[SessionName] = FilterDictionary;
        }

    }

    public static int LineNumber(this Exception e)
    {
        int linenum = 0;
        try
        {
            //linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5));

            //For Localized Visual Studio ... In other languages stack trace  doesn't end with ":Line 12"
            linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(' ')));

        }


        catch
        {
            //Stack trace is not available!
        }
        return linenum;
    }

    public static DataTable GetMonths(int Year)
    {
        DataTable Dt = new DataTable();
        Dt.Columns.Add("ID", typeof(int));
        Dt.Columns.Add("Name", typeof(string));
        int Months;

        if (Year < DateTime.Now.Year)
        {
            Months = 12;
        }
        else
        {
            Months = DateTime.Now.Month;
        }

        for (int i = 1; i <= Months; i++)
        {
            Dt.Rows.Add(i, GetMonthName(i));
        }

        return Dt;
    }

    public static DataTable GetNumber(int StartNumber, int EndNumber)
    {
        DataTable Dt = new DataTable();
        Dt.Columns.Add("ID", typeof(int));
        Dt.Columns.Add("Name", typeof(string));

        for (int i = StartNumber; i <= EndNumber; i++)
        {
            Dt.Rows.Add(i, i.ToString());
        }

        return Dt;
    }
}
