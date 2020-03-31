using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tools_SIB_R_Add_Default : System.Web.UI.Page
{
    int _ApplicationsID = 0;
    int _ApplicationsPersonsID = 0;
    int _SIBRID = 0;
    int _Result = 0;

    private void TransactionRollBack(DALC.Transaction Transaction)
    {
        Transaction.SqlTransaction.Rollback();
        Transaction.Com.Connection.Close();
        Transaction.Com.Connection.Dispose();
        Transaction.Com.Dispose();
    }

    private void CheckQuery()
    {
        string[] Query = Cryptography.Decrypt(Server.UrlDecode(Config._GetQueryString("i")).Replace(' ', '+')).Split('-');

        if (!int.TryParse(Query[0], out _ApplicationsID) || !int.TryParse(Query[1], out _ApplicationsPersonsID) ||
            Query[2] != DALC._GetUsersLogin.Key || !int.TryParse(Config._GetQueryString("id").Decrypt(), out _SIBRID))
        {
            Response.Write("Məlumat tapılmadı.");
            Response.End();
            return;
        }
    }

    private byte GetSIBRTypeID(int Age)
    {

        //return (byte)Tools.SIBRTypes.Full_Scale;
        if (Age > 7)
        {
            return (byte)Tools.SIBRTypes.Full_Scale;
        }
        else
        {
            return (byte)Tools.SIBRTypes.Early_Development_Form;
        }
    }

    private void BindInfo()
    {
        DataTable Dt = DALC.GetApplicationsPersonsByID(_ApplicationsPersonsID, Tools.Table.V_ApplicationsPersons);
        if (Dt == null || Dt.Rows.Count < 1)
        {
            Response.Write(Config._DefaultErrorMessages);
            Response.End();
            return;
        }

        TxtFullName.Text = string.Format("{0} {1} {2}", Dt._Rows("Name"), Dt._Rows("Surname"), Dt._Rows("Patronymic"));
        TxtExaminer.Text = DALC._GetUsersLogin.Fullname;
        TxtSchool.Text = "";
        TxtParent.Text = "";
        TxtAdress.Text = Dt._Rows("CurrentAddress");

        DataTable DtApplicant = DALC.GetApplicantByApplicationsID(_ApplicationsID);

        if (DtApplicant != null && DtApplicant.Rows.Count > 0)
        {
            TxtRespondent.Text = DtApplicant._Rows("FullName");
            TxtRelationship.Text = DtApplicant._Rows("ApplicationsPersonsTypes");
        }

        TxtTestingDateYear.Text = DateTime.Now.ToString("yyyy");
        TxtTestingDateMonth.Text = DateTime.Now.ToString("MM");
        TxtTestingDateDay.Text = DateTime.Now.ToString("dd");

        TxtBirthDateYear.Text = ((DateTime)Dt._RowsObject("BirthDate")).ToString("yyyy");
        TxtBirthDateMonth.Text = ((DateTime)Dt._RowsObject("BirthDate")).ToString("MM");
        TxtBirthDateDay.Text = ((DateTime)Dt._RowsObject("BirthDate")).ToString("dd");
    }

    private void BindSIBRScoringTypes()
    {
        RptAdaptive.DataSource = DALC.GetSIBRScoringTypes();
        RptAdaptive.DataBind();
    }

    private void BindList()
    {
        DListSIBRTypes.DataSource = DALC.GetList(Tools.Table.SIBRTypes);
        DListSIBRTypes.DataBind();
        DListSIBRTypes.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void CalculationForFullScale()
    {
        int Internalized, Asocial, Externalized, General;
        int.TryParse(HdnFinalBox1.Value, out Internalized);
        int.TryParse(HdnFinalBox2.Value, out Asocial);
        int.TryParse(HdnFinalBox3.Value, out Externalized);
        int.TryParse(HdnFinalBox4.Value, out General);


        int AgeYear, AgeMonth;

        int.TryParse(HdnAgeYear.Value, out AgeYear);
        int.TryParse(HdnAgeMonth.Value, out AgeMonth);

        if (AgeYear >= 19)
        {
            AgeMonth = AgeYear;
        }

        if (AgeYear > 81)
        {
            AgeYear = 81;
            AgeMonth = 81;
        }

        DALC.Transaction Transaction = new DALC.Transaction();

        #region InsertSIBRTable

        Dictionary<string, object> Dictionary = new Dictionary<string, object>
        {
            {"SIBRTypesID",(int)Tools.SIBRTypes.Full_Scale},
            {"ApplicationsPersonsID",_ApplicationsPersonsID },
            {"AgeYear",AgeYear},
            {"AgeMonth",AgeMonth},
            {"Description",TxtDescription.Text},
            {"IsCompleted",true},
            {"IsActive",true },
            {"Create_Dt",Config.DateTimeFormat(string.Format("{0}.{1}.{2}",TxtTestingDateDay.Text,TxtTestingDateMonth.Text,TxtTestingDateYear.Text))},
            {"SIBRStatusID",(int)Tools.SIBRStatus.Aktiv },
            {"Add_Dt",DateTime.Now},
            {"Add_Ip",Request.UserHostAddress.IPToInteger()}
        };

        _Result = DALC.InsertDatabase(Tools.Table.SIBR, Dictionary, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        _SIBRID = _Result;

        #endregion

        DataTable DtSIBRAdaptiveScores = new DataTable();
        DtSIBRAdaptiveScores.Columns.Add("SIBRID", typeof(int));
        DtSIBRAdaptiveScores.Columns.Add("SIBRScoringTypesID", typeof(int));
        DtSIBRAdaptiveScores.Columns.Add("SumA", typeof(int));
        DtSIBRAdaptiveScores.Columns.Add("SumB", typeof(int));
        DtSIBRAdaptiveScores.Columns.Add("SumC", typeof(int));
        DtSIBRAdaptiveScores.Columns.Add("W", typeof(int));

        DataTable DtSIBRAdaptiveResult = new DataTable();
        DtSIBRAdaptiveResult.Columns.Add("SIBRID", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("SIBRScoringTypesGroupsID", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("SumW", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("REFW", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("SEMSS", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("COLUMNS_DIFF_P", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("COLUMNS_DIFF_N", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("DIFF", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("RMI", typeof(string));
        DtSIBRAdaptiveResult.Columns.Add("SS", typeof(decimal));
        DtSIBRAdaptiveResult.Columns.Add("SS_SEM_N", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("SS_SEM_P", typeof(int));
        DtSIBRAdaptiveResult.Columns.Add("PR", typeof(decimal));
        DtSIBRAdaptiveResult.Columns.Add("SIBRAdaptiveSkillLevelsID", typeof(int));

        int SumA, SumB, SumC, SIBRScoringTypesID, RawScore, W;
        decimal MS = 0, SC = 0, PL = 0, CL = 0, BI = 0;

        foreach (RepeaterItem item in RptAdaptive.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                int.TryParse(((TextBox)item.FindControl("TxtSumA")).Text, out SumA);
                int.TryParse(((TextBox)item.FindControl("TxtSumB")).Text, out SumB);
                int.TryParse(((TextBox)item.FindControl("TxtSumC")).Text, out SumC);
                int.TryParse(((HiddenField)item.FindControl("HdnSIBRScoringTypes")).Value, out SIBRScoringTypesID);
                int.TryParse(((HiddenField)PnlRawScore.FindControl(string.Format("HdnRawScore{0}", SIBRScoringTypesID))).Value, out RawScore);

                DataTable Dt = DALC.GetSIBRScoring(Tools.SIBRTypes.Full_Scale, SIBRScoringTypesID, RawScore);

                if (Dt == null)
                {
                    TransactionRollBack(Transaction);
                    Config.MsgBoxAjax(Config._DefaultErrorMessages);
                    return;
                }

                if (Dt.Rows.Count < 1)
                {
                    TransactionRollBack(Transaction);
                    Config.MsgBoxAjax(string.Format("{0}: RawScore dəyəri {1} uyğun məlumat tapılmadı.", ((Tools.SIBRScoringTypes)Enum.Parse(typeof(Tools.SIBRScoringTypes), SIBRScoringTypesID.ToString())).ToDescriptionString(), RawScore));
                    return;
                }

                W = int.Parse(Dt._Rows("W"));

                //Burda baxiriqki  SIBRScoringTypesID hansi SIBRScoringTypesGroups-a aiddirse ona uygun emeliyyat apaririq
                if (Tools.SIBRScoringTypesGroups.Motor_Skills.ToDescriptionString().Contains(string.Format(",{0},", SIBRScoringTypesID)))
                {
                    MS += W;
                }
                else if (Tools.SIBRScoringTypesGroups.Social_Interaction_Communication_Skills.ToDescriptionString().Contains(string.Format(",{0},", SIBRScoringTypesID)))
                {
                    SC += W;
                }
                else if (Tools.SIBRScoringTypesGroups.Personal_Living_Skills.ToDescriptionString().Contains(string.Format(",{0},", SIBRScoringTypesID)))
                {
                    PL += W;
                }
                else if (Tools.SIBRScoringTypesGroups.Community_Living_Skills.ToDescriptionString().Contains(string.Format(",{0},", SIBRScoringTypesID)))
                {
                    CL += W;
                }

                DtSIBRAdaptiveScores.Rows.Add(_SIBRID, SIBRScoringTypesID, SumA, SumB, SumC, W);
            }
        }

        MS = Math.Round(MS / 2);
        SC = Math.Round(SC / 3);
        PL = Math.Round(PL / 5);
        CL = Math.Round(CL / 4);
        BI = Math.Round((MS + SC + PL + CL) / 4);


        //Short ve Early Development Form xaric hamisini getirek
        DataTable DtNormF = DALC.GetSIBRNormFForFullScale(AgeYear, AgeMonth);

        if (DtNormF == null || DtNormF.Rows.Count < 1)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax("Norm F cədvəlindən uyğun məlumat tapılmadı.");
            return;
        }


        decimal SumW = 0, REFW = 0, DIFF = 0;
        int SEMSS = 0, COLUMNS_DIFF_P = 0, COLUMNS_DIFF_N = 0;
        int SS = 0, SS_SEM_N = 0, SS_SEM_P = 0;
        int SIBRAdaptiveSkillLevelsID;
        string RMI = "";
        decimal PR = 0;
        int SIBRScoringTypesGroupsID = 0;

        foreach (DataRow Dr in DtNormF.Rows)
        {
            REFW = Dr["REFW"]._ToInt32();
            SEMSS = Dr["SEMSS"]._ToInt32();
            COLUMNS_DIFF_P = Dr["DIFF_P"]._ToInt32();
            COLUMNS_DIFF_N = Dr["DIFF_N"]._ToInt32();

            if (Dr["SIBRNormFTypesID"]._ToInt16() == (int)Tools.SIBRNormFTypes.Motor_Skills)
            {
                SumW = MS;
                DIFF = MS - REFW;
                SIBRScoringTypesGroupsID = (int)Tools.SIBRScoringTypesGroups.Motor_Skills;
            }
            else if (Dr["SIBRNormFTypesID"]._ToInt16() == (int)Tools.SIBRNormFTypes.Social_Interaction_Communication_Skills)
            {
                SumW = SC;
                DIFF = SC - REFW;
                SIBRScoringTypesGroupsID = (int)Tools.SIBRScoringTypesGroups.Social_Interaction_Communication_Skills;
            }
            else if (Dr["SIBRNormFTypesID"]._ToInt16() == (int)Tools.SIBRNormFTypes.Personal_Living_Skills)
            {
                SumW = PL;
                DIFF = PL - REFW;
                SIBRScoringTypesGroupsID = (int)Tools.SIBRScoringTypesGroups.Personal_Living_Skills;
            }
            else if (Dr["SIBRNormFTypesID"]._ToInt16() == (int)Tools.SIBRNormFTypes.Community_Living_Skills)
            {
                SumW = CL;
                DIFF = CL - REFW;
                SIBRScoringTypesGroupsID = (int)Tools.SIBRScoringTypesGroups.Community_Living_Skills;
            }
            else if (Dr["SIBRNormFTypesID"]._ToInt16() == (int)Tools.SIBRNormFTypes.Broad_Independence)
            {
                SumW = BI;
                DIFF = BI - REFW;
                SIBRScoringTypesGroupsID = (int)Tools.SIBRScoringTypesGroups.Broad_Independence_Full_Scale;
            }

            DataTable DtNormG = new DataTable();
            if (DIFF >= 0)
            {
                DtNormG = DALC.GetSIBRNormG(COLUMNS_DIFF_P, DIFF._ToInt32());
            }
            else
            {
                DtNormG = DALC.GetSIBRNormG(COLUMNS_DIFF_N, DIFF._ToInt32());
            }

            if (DtNormG == null || DtNormG.Rows.Count < 1)
            {
                TransactionRollBack(Transaction);
                Config.MsgBoxAjax("Norm G cədvəlindən uyğun məlumat tapılmadı.");
                return;
            }


            RMI = DtNormG._Rows("RMI");
            SS = DtNormG._RowsObject("SS")._ToInt32();
            SS_SEM_N = SS - SEMSS;
            SS_SEM_P = SS + SEMSS;
            PR = Convert.ToDecimal(DtNormG._RowsObject("PR"));

            SIBRAdaptiveSkillLevelsID = DALC.GetSIBRAdaptiveSkillLevels(DIFF._ToInt32());

            if (SIBRAdaptiveSkillLevelsID == -1)
            {
                TransactionRollBack(Transaction);
                Config.MsgBoxAjax("Adaptive Behavior Skill Levels cədvəlindən məlumat tapılmadı.");
                return;
            }
            DtSIBRAdaptiveResult.Rows.Add(_SIBRID,
                                        SIBRScoringTypesGroupsID,
                                        SumW,
                                        REFW,
                                        SEMSS,
                                        COLUMNS_DIFF_P,
                                        COLUMNS_DIFF_N,
                                        DIFF,
                                        RMI,
                                        SS,
                                        SS_SEM_N,
                                        SS_SEM_P,
                                        PR,
                                        SIBRAdaptiveSkillLevelsID);
        }


        _Result = DALC.InsertBulk(Tools.Table.SIBRAdaptiveScores, DtSIBRAdaptiveScores, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        _Result = DALC.InsertBulk(Tools.Table.SIBRAdaptiveResult, DtSIBRAdaptiveResult, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DataTable DtSIBRNormI = DALC.GetSIBRNomrIForBroadIndependence(Tools.SIBRNormITypes.Broad_Independence_W, BI._ToInt32(), General);

        if (DtSIBRNormI == null)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (DtSIBRNormI.Rows.Count < 1)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax("Norm I cədvəlindən uyğun məlumat tapılmadı.");
            return;
        }

        int SupportScore = DtSIBRNormI._Rows("Value")._ToInt32();

        Dictionary<string, object> DicMaladaptive = new Dictionary<string, object>()
        {
            {"SIBRID",_SIBRID },
            {"Internalized",Internalized },
            {"Asocial",Asocial },
            {"Externalized",Externalized },
            {"General",General },
            {"SupportScore",SupportScore }
        };

        _Result = DALC.InsertDatabase(Tools.Table.SIBRMaladaptiveScores, DicMaladaptive, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DataTable DtDetailsTypes = DALC.GetList(Tools.Table.SIBRMaladaptiveScoresDetailsTypes, "");
        if (DtDetailsTypes == null || DtDetailsTypes.Rows.Count < 1)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DataTable DtMaladaptiveScoresDetails = new DataTable();
        DtMaladaptiveScoresDetails.Columns.Add("SIBRMaladaptiveScoresID", typeof(int));
        DtMaladaptiveScoresDetails.Columns.Add("SIBRMaladaptiveScoresDetailsTypesID", typeof(byte));
        DtMaladaptiveScoresDetails.Columns.Add("FrequencyRaiting", typeof(byte));
        DtMaladaptiveScoresDetails.Columns.Add("SeverityRaiting", typeof(byte));


        int x = 1, y = 2;
        byte Frequency, Severity;
        foreach (DataRow Dr in DtDetailsTypes.Rows)
        {
            if (!byte.TryParse(((TextBox)PnlProblemBehavior.FindControl(string.Format("Txt{0}", x))).Text, out Frequency) ||
                !byte.TryParse(((TextBox)PnlProblemBehavior.FindControl(string.Format("Txt{0}", y))).Text, out Severity))
            {
                TransactionRollBack(Transaction);
                Config.MsgBoxAjax("Problem Behavior dəyərlərini düzgün qeyd edin!");
                return;
            }

            DtMaladaptiveScoresDetails.Rows.Add(_Result, Convert.ToByte(Dr["ID"]), Frequency, Severity);

        }

        _Result = DALC.InsertBulk(Tools.Table.SIBRMaladaptiveScoresDetails, DtMaladaptiveScoresDetails, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Dictionary<string, object> AddServicesDictionary = new Dictionary<string, object>();
        AddServicesDictionary.Add("ApplicationsPersonsID", _ApplicationsPersonsID);
        AddServicesDictionary.Add("ServicesID", (int)Tools.Services.SIB_R_tam_miqyaslı_forma);
        AddServicesDictionary.Add("ApplicationsPersonsServicesStatusID", (int)Tools.ApplicationsPersonsServicesStatus.Həll_olunub_təmin_edilib);
        AddServicesDictionary.Add("IsFirstApplication", false);
        AddServicesDictionary.Add("IsActive", true);
        AddServicesDictionary.Add("Add_Dt", DateTime.Now);
        AddServicesDictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

        _Result = DALC.InsertDatabase(Tools.Table.ApplicationsPersonsServices, AddServicesDictionary, Transaction, true);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.RedirectURL(string.Format("/tools/sib-r/result/?i={0}&id={1}", Cryptography.Encrypt(string.Format("{0}-{1}-{2}", _ApplicationsID, _ApplicationsPersonsID, DALC._GetUsersLogin.Key)), Cryptography.Encrypt(_SIBRID._ToString())));
    }

    //davam etmek lazimdir
    private void CalculationByTypes(Tools.SIBRTypes SIBRTypes)
    {
        Tools.SIBRNormFTypes SIBRNormFTypes = Tools.SIBRNormFTypes.Early_Development_Form;
        Tools.SIBRNormITypes SIBRNormITypes = Tools.SIBRNormITypes.Early_Development_Form_W;

        if (SIBRTypes == Tools.SIBRTypes.Short_Form)
        {
            SIBRNormFTypes= Tools.SIBRNormFTypes.Short_Form;
            SIBRNormITypes = Tools.SIBRNormITypes.Short_Form_W;
        }

        int Internalized, Asocial, Externalized, General;
        int.TryParse(HdnFinalBox1.Value, out Internalized);
        int.TryParse(HdnFinalBox2.Value, out Asocial);
        int.TryParse(HdnFinalBox3.Value, out Externalized);
        int.TryParse(HdnFinalBox4.Value, out General);


        int AgeYear, AgeMonth;

        int.TryParse(HdnAgeYear.Value, out AgeYear);
        int.TryParse(HdnAgeMonth.Value, out AgeMonth);

        if (AgeYear >= 19)
        {
            AgeMonth = AgeYear;
        }

        if (AgeYear > 81)
        {
            AgeYear = 81;
            AgeMonth = 81;
        }

        DALC.Transaction Transaction = new DALC.Transaction();

        Dictionary<string, object> DictionarySIBR = new Dictionary<string, object>
        {
            {"SIBRTypesID",(int)SIBRTypes},
            {"ApplicationsPersonsID",_ApplicationsPersonsID },
            {"AgeYear",AgeYear},
            {"AgeMonth",AgeMonth},
            {"Description",TxtDescription.Text},
            {"IsCompleted",true},
            {"IsActive",true },
            {"Create_Dt",Config.DateTimeFormat(string.Format("{0}.{1}.{2}",TxtTestingDateDay.Text,TxtTestingDateMonth.Text,TxtTestingDateYear.Text))},
            {"SIBRStatusID",(int)Tools.SIBRStatus.Aktiv },
            {"Add_Dt",DateTime.Now},
            {"Add_Ip",Request.UserHostAddress.IPToInteger()}
        };

        _Result = DALC.InsertDatabase(Tools.Table.SIBR, DictionarySIBR, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        _SIBRID = _Result;

        int Page2SumA, Page2SumB, Page2SumC, Page3SumA, Page3SumB, Page3SumC, SumA, SumB, SumC, SIBRScoringTypesID, RawScore, W;
        decimal BI = 0;

        int.TryParse(TxtPage2SumA.Text, out Page2SumA);
        int.TryParse(TxtPage2SumB.Text, out Page2SumB);
        int.TryParse(TxtPage2SumC.Text, out Page2SumC);

        int.TryParse(TxtPage3SumA.Text, out Page3SumA);
        int.TryParse(TxtPage3SumB.Text, out Page3SumB);
        int.TryParse(TxtPage3SumC.Text, out Page3SumC);

        SumA = Page2SumA + Page3SumA;
        SumB = Page2SumB + Page3SumB;
        SumC = Page2SumC + Page3SumC;

        SIBRScoringTypesID = (int)Tools.SIBRScoringTypes.Broad_Independence;

        int.TryParse(LblTotalRawScore.Text, out RawScore);

        W = DALC.GetSIBRScoring(SIBRTypes, SIBRScoringTypesID, RawScore)._RowsInt("W");
        BI = W;
        Dictionary<string, object> DicSIBRAdaptiveScores = new Dictionary<string, object>
        {
            {"SIBRID",_SIBRID },
            {"SIBRScoringTypesID", SIBRScoringTypesID },
            {"SumA", SumA},
            {"SumB", SumB},
            {"SumC", SumC},
            {"W", W},
        };

        DataTable DtNormF = DALC.GetSIBRNormFByTypesID(SIBRNormFTypes, AgeYear, AgeMonth);

        if (DtNormF == null || DtNormF.Rows.Count < 1)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax("Norm F cədvəlindən uyğun məlumat tapılmadı.");
            return;
        }

        decimal SumW = 0, REFW = 0, DIFF = 0;
        int SEMSS = 0, COLUMNS_DIFF_P = 0, COLUMNS_DIFF_N = 0;
        int SS = 0, SS_SEM_N = 0, SS_SEM_P = 0;
        int SIBRAdaptiveSkillLevelsID;
        string RMI = "";
        decimal PR = 0;
        int SIBRScoringTypesGroupsID = 0;

        try
        {
            SumW = W;
            REFW = DtNormF._RowsInt("REFW");
            SEMSS = DtNormF._RowsInt("SEMSS");
            COLUMNS_DIFF_P = DtNormF._RowsInt("DIFF_P");
            COLUMNS_DIFF_N = DtNormF._RowsInt("DIFF_N");
            DIFF = W - REFW;
        }
        catch
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }


        SIBRScoringTypesGroupsID = (int)Tools.SIBRScoringTypesGroups.Broad_Independence_Full_Scale;

        DataTable DtNormG = new DataTable();
        if (DIFF >= 0)
        {
            DtNormG = DALC.GetSIBRNormG(COLUMNS_DIFF_P, DIFF._ToInt32());
        }
        else
        {
            DtNormG = DALC.GetSIBRNormG(COLUMNS_DIFF_N, DIFF._ToInt32());
        }

        if (DtNormG == null || DtNormG.Rows.Count < 1)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax("Norm G cədvəlindən uyğun məlumat tapılmadı.");
            return;
        }

        RMI = DtNormG._Rows("RMI");
        SS = DtNormG._RowsObject("SS")._ToInt32();
        SS_SEM_N = SS - SEMSS;
        SS_SEM_P = SS + SEMSS;
        PR = Convert.ToDecimal(DtNormG._RowsObject("PR"));

        SIBRAdaptiveSkillLevelsID = DALC.GetSIBRAdaptiveSkillLevels(DIFF._ToInt32());

        if (SIBRAdaptiveSkillLevelsID == -1)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax("Adaptive Behavior Skill Levels cədvəlindən məlumat tapılmadı.");
            return;
        }

        Dictionary<string, object> DicSIBRAdaptiveResult = new Dictionary<string, object>
        {
            {"SIBRID", _SIBRID},
            {"SIBRScoringTypesGroupsID", SIBRScoringTypesGroupsID},
            {"SumW", SumW},
            {"REFW", REFW},
            {"SEMSS", SEMSS },
            {"COLUMNS_DIFF_P",  COLUMNS_DIFF_P},
            {"COLUMNS_DIFF_N", COLUMNS_DIFF_N},
            {"DIFF",  DIFF},
            {"RMI",  RMI},
            {"SS", SS },
            {"SS_SEM_N",  SS_SEM_N},
            {"SS_SEM_P", SS_SEM_P },
            {"PR",  PR},
            {"SIBRAdaptiveSkillLevelsID",SIBRAdaptiveSkillLevelsID }
        };

        _Result = DALC.InsertDatabase(Tools.Table.SIBRAdaptiveScores, DicSIBRAdaptiveScores, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        _Result = DALC.InsertDatabase(Tools.Table.SIBRAdaptiveResult, DicSIBRAdaptiveResult, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DataTable DtSIBRNormI = DALC.GetSIBRNomrIForBroadIndependence(SIBRNormITypes, W, General);

        if (DtSIBRNormI == null)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        if (DtSIBRNormI.Rows.Count < 1)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax("Norm I cədvəlindən uyğun məlumat tapılmadı.");
            return;
        }

        int SupportScore = DtSIBRNormI._Rows("Value")._ToInt32();

        Dictionary<string, object> DicMaladaptive = new Dictionary<string, object>()
        {
            {"SIBRID",_SIBRID },
            {"Internalized",Internalized },
            {"Asocial",Asocial },
            {"Externalized",Externalized },
            {"General",General },
            {"SupportScore",SupportScore }
        };

        _Result = DALC.InsertDatabase(Tools.Table.SIBRMaladaptiveScores, DicMaladaptive, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DataTable DtDetailsTypes = DALC.GetList(Tools.Table.SIBRMaladaptiveScoresDetailsTypes, "");
        if (DtDetailsTypes == null || DtDetailsTypes.Rows.Count < 1)
        {
            TransactionRollBack(Transaction);
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        DataTable DtMaladaptiveScoresDetails = new DataTable();
        DtMaladaptiveScoresDetails.Columns.Add("SIBRMaladaptiveScoresID", typeof(int));
        DtMaladaptiveScoresDetails.Columns.Add("SIBRMaladaptiveScoresDetailsTypesID", typeof(byte));
        DtMaladaptiveScoresDetails.Columns.Add("FrequencyRaiting", typeof(byte));
        DtMaladaptiveScoresDetails.Columns.Add("SeverityRaiting", typeof(byte));


        int x = 1, y = 2;
        byte Frequency, Severity;
        foreach (DataRow Dr in DtDetailsTypes.Rows)
        {
            if (!byte.TryParse(((TextBox)PnlProblemBehavior.FindControl(string.Format("Txt{0}", x))).Text, out Frequency) ||
                !byte.TryParse(((TextBox)PnlProblemBehavior.FindControl(string.Format("Txt{0}", y))).Text, out Severity))
            {
                TransactionRollBack(Transaction);
                Config.MsgBoxAjax("Problem Behavior dəyərlərini düzgün qeyd edin!");
                return;
            }

            DtMaladaptiveScoresDetails.Rows.Add(_Result, Convert.ToByte(Dr["ID"]), Frequency, Severity);

        }

        _Result = DALC.InsertBulk(Tools.Table.SIBRMaladaptiveScoresDetails, DtMaladaptiveScoresDetails, Transaction);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Dictionary<string, object> AddServicesDictionary = new Dictionary<string, object>();
        AddServicesDictionary.Add("ApplicationsPersonsID", _ApplicationsPersonsID);
        AddServicesDictionary.Add("ServicesID", (int)Tools.Services.SIB_R_tam_miqyaslı_forma);
        AddServicesDictionary.Add("ApplicationsPersonsServicesStatusID", (int)Tools.ApplicationsPersonsServicesStatus.Həll_olunub_təmin_edilib);
        AddServicesDictionary.Add("IsFirstApplication", false);
        AddServicesDictionary.Add("IsActive", true);
        AddServicesDictionary.Add("Add_Dt", DateTime.Now);
        AddServicesDictionary.Add("Add_Ip", Request.UserHostAddress.IPToInteger());

        _Result = DALC.InsertDatabase(Tools.Table.ApplicationsPersonsServices, AddServicesDictionary, Transaction, true);
        if (_Result < 1)
        {
            Config.MsgBoxAjax(Config._DefaultErrorMessages);
            return;
        }

        Config.RedirectURL(string.Format("/tools/sib-r/result/?i={0}&id={1}", Cryptography.Encrypt(string.Format("{0}-{1}-{2}", _ApplicationsID, _ApplicationsPersonsID, DALC._GetUsersLogin.Key)), Cryptography.Encrypt(_SIBRID._ToString())));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DALC._GetUsersLogin == null)
        {
            Config.RedirectURL("/");
            return;
        }

        if (!DALC.IsPermission(Tools.UsersPermissionsModules.SIB_R))
        {
            Config.RedirectURL("/tools");
            return;
        }

        CheckQuery();
        ((Literal)Master.FindControl("LtrTitle")).Text = "SIB-R qiymətləndirmə";
        if (!IsPostBack)
        {
            BindList();
            BindInfo();
            BindSIBRScoringTypes();
        }

        BtnPrevious.Visible = MultiView1.ActiveViewIndex != 0;
    }

    protected void BtnNext_Click(object sender, EventArgs e)
    {
        if (MultiView1.ActiveViewIndex == 0)
        {
            int TestingDateYear, BirthDateYear, TestingDateMonth, TestingDateDay;
            int.TryParse(TxtTestingDateYear.Text, out TestingDateYear);
            int.TryParse(TxtTestingDateMonth.Text, out TestingDateMonth);
            int.TryParse(TxtTestingDateDay.Text, out TestingDateDay);
            int.TryParse(TxtBirthDateYear.Text, out BirthDateYear);

            if (DListSIBRTypes.SelectedValue == "-1")
            {
                Config.MsgBoxAjax("Qitmətləndirmə növünü seçin!");
                return;
            }

            if (TestingDateYear <= BirthDateYear || TestingDateMonth < 1 || TestingDateDay < 1)
            {
                Config.MsgBoxAjax("Qitmətləndirmə tarixini düzgün qeyd edin!");
                return;
            }
            ((Literal)Master.FindControl("LtrTitle")).Text = "Adaptive Behavior";

            //Heleki MultiView1.ActiveViewIndex = 1 edek

            if (byte.Parse(DListSIBRTypes.SelectedValue) == (byte)Tools.SIBRTypes.Full_Scale)
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                MultiView1.ActiveViewIndex = 2;
            }

        }
        else if (MultiView1.ActiveViewIndex == 1 || MultiView1.ActiveViewIndex == 2)
        {
            ((Literal)Master.FindControl("LtrTitle")).Text = "Problem Behavior";
            BtnNext.Text = "H e s a b l a";
            MultiView1.ActiveViewIndex = 3;
            TxtIndividualAge.Text = HdnAgeYear.Value;
        }
        else if (MultiView1.ActiveViewIndex == 3)
        {
            System.Threading.Thread.Sleep(3000); //Loading görsənsin :) suni gecikdirme 
            if (byte.Parse(DListSIBRTypes.SelectedValue) == (byte)Tools.SIBRTypes.Full_Scale)
            {
                CalculationForFullScale();
            }
            else if (byte.Parse(DListSIBRTypes.SelectedValue) == (byte)Tools.SIBRTypes.Early_Development_Form)
            {
                CalculationByTypes(Tools.SIBRTypes.Early_Development_Form);
            }
            else
            {
                CalculationByTypes(Tools.SIBRTypes.Short_Form);
            }

        }

        BtnPrevious.Visible = MultiView1.ActiveViewIndex != 0;
    }

    protected void BtnPrevious_Click(object sender, EventArgs e)
    {
        BtnNext.Text = "Növbəti";
        if (MultiView1.ActiveViewIndex == 3)
        {
            if (GetSIBRTypeID(int.Parse(HdnAgeYear.Value)) == (byte)Tools.SIBRTypes.Full_Scale)
            {
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                MultiView1.ActiveViewIndex = 2;
            }
        }
        else if (MultiView1.ActiveViewIndex == 2 || MultiView1.ActiveViewIndex == 1)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        BtnPrevious.Visible = MultiView1.ActiveViewIndex != 0;
    }

    protected void View3_Activate(object sender, EventArgs e)
    {
        BtnNext.Attributes.Add("onClick", @"
                                document.getElementById('contentFinish').style.display='none';
                                document.getElementById('contentLoading').style.display='';
                                document.getElementById('buttonFinish').style.display='none';");
    }
}