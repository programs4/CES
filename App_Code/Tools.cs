using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

public static class Tools
{
    public enum Table
    {
        Applications,
        ApplicationsCase,
        ApplicationsCaseCommunities,
        ApplicationsCaseFinancial,
        ApplicationsCaseFinancialIncome,
        ApplicationsCasePlaceTypes,
        ApplicationsCaseStatus,
        ApplicationsCaseTypes,
        ApplicationsCaseWork,
        ApplicationsFamily,
        ApplicationsFamilyPartners,
        ApplicationsFamilyPartnersTypes,
        ApplicationsFamilyStatus,
        ApplicationsFamilyTypes,
        ApplicationsFamilyUsers,
        ApplicationsPersons,
        ApplicationsPersonsServices,
        ApplicationsPersonsServicesStatus,
        ApplicationsPersonsServicesUseHistory,
        ApplicationsPersonsTypes,
        ApplicationsSocialStatus,
        ApplicationsStatus,
        ApplicationsTypes,
        DataLists,
        DemographicInformations,
        DemographicInformationsDetails,
        DemographicInformationsTypes,
        DocumentTypes,
        Downloads,
        DownloadsQualityTypes,
        DownloadsTypes,
        EducationsTypes,
        ErrorLogs,
        Evaluations,
        EvaluationsPoints,
        EvaluationsQuestions,
        EvaluationsSkill,
        EvaluationsSkillAges,
        EvaluationsSkillGroups,
        EvaluationsSkillPoints,
        EvaluationsSkillQuestions,
        EvaluationsSkillValues,
        Events,
        EventsDirectionTypes,
        EventsPolicyTypes,
        EventsTypes,
        Feedback,
        ListOperationTypes,
        ListPersonsStatus,
        MaritalStatus,
        Organizations,
        ReportsTypes,
        Services,
        ServicesCourses,
        ServicesCoursesLessons,
        ServicesCoursesLessonsUseHistory,
        ServicesCoursesLessonsUseHistoryTypes,
        ServicesCoursesPersons,
        ServicesCoursesPlans,
        ServicesCoursesTypes,
        ServicesPlans,
        ServicesPlansTypes,
        SIBR,
        SIBRAdaptiveResult,
        SIBRAdaptiveScores,
        SIBRAdaptiveSkillLevels,
        SIBRAdaptiveSubscalesScores,
        SIBRMaladaptiveScores,
        SIBRMaladaptiveScoresDetails,
        SIBRMaladaptiveScoresDetailsTypes,
        SIBRMaladaptiveSupportScore,
        SIBRNormF,
        SIBRNormFTypes,
        SIBRNormG,
        SIBRNormI,
        SIBRNormITypes,
        SIBRNormJ,
        SIBRScoring,
        SIBRScoringTypes,
        SIBRScoringTypesGroups,
        SIBRStatus,
        SIBRTypes,
        SocialStatus,
        Users,
        UsersAccounts,
        UsersPermissionsModules,
        UsersStatus,
        V_Applications,
        V_ApplicationsFamily,
        V_ApplicationsPersons,
        V_ApplicationsPersonsServices,
        V_ApplicationsPersonsServicesUseHistory,
        V_ApplicationsSocialStatus,
        V_DemographicInformations,
        V_DemographicInformationsDetails,
        V_Downloads,
        V_Evaluations,
        V_EvaluationsSkill,
        V_Events,
        V_ServicesCourses,
        V_ServicesCoursesLessons,
        V_ServicesCoursesPersons,
        V_ServicesCoursesPlans,
        V_ServicesForServicesCourses,
        V_ServicesPlans,
        V_SIBR,
        V_SIBRAdaptiveResult,
        V_SIBRAdaptiveScores,
        V_SIBRAdaptiveSubscalesScores,
        V_Users,
        V_UsersAccounts,
    }

    public enum Pages
    {
        [Description("Müraciətlər")]
        applications,
    }

    public enum Permission
    {
        İstifadəçi_interfeysi = 10
    }

    public enum ApplicationsCaseStatus
    {
        Açıq = 10,
        Bağlı = 90
    }

    public enum ApplicationsCaseTypes
    {
        Ailə = 10,
        Qadın = 20,
        Uşaq = 30,
        Kişi = 40
    }

    public enum ApplicationsPersonsServicesStatus
    {
        İcra_olunur = 10,
        Həll_olunub_təmin_edilib = 20,
        Qismən_təmin_edilib = 30,
        Müvafiq_izahat_verilib = 40,
        Əsassız_sayılıb = 50,
        Baxılması_üçün_aidiyyatı_quruma_göndərilib = 60
    }

    public enum DataLists
    {
        İcmada_iştirakı = 1,
        Ailənin_maddi_təminatı = 2,
        Gəlir = 3,
        Müəssisə_növləri = 4,
        Məşğulluq = 5,
        Ailə_səfərində_iştirak_edən_qurumlar = 6,
        Ailə_səfərinin_məqsədi = 7,
        Qohumluq_dərəcəsi = 8,
        Müraciətin_növü = 9,
        Təhsil_növü = 10,
        Təlim_tədbirdə_iştirak_forması = 11,
        Tədbirin_növü = 12,
        Ailə_vəziyyəti = 13,
        Sosial_statusu = 14
    }

    public enum DocumentTypes
    {
        Şəxsiyyət_vəsiqəsi_0_16_yaş = 10,
        Şəxsiyyət_vəsiqəsi_16_yaş_yuxarı = 15,
        Doğum_şəhadətnaməsi_AZ_I = 20,
        Doğum_şəhadətnaməsi_AZ_II = 22,
        Doğum_şəhadətnaməsi_AZ_III = 25,
        Müvəqqəti_yaşayış_icazəsi = 30,
        Daimi_yaşayış_icazəsi = 40,
        Sənədini_təqdim_etməyib = 80,
        Digər = 90
    }

    public enum EventsTypes
    {
        Tədbir = 10,
        Təlim = 20,
    }

    //Burda lazim olanlari getridim.Choxdu deye digerlrini yigmadim
    public enum Services
    {
        SIB_R_qısa_forma = 88,
        SIB_R_erkən_inkişaf_fomrası = 89,
        SIB_R_tam_miqyaslı_forma = 90,
        Daxili_qiymətləndirmə = 91,
    }

    public enum UsersStatus
    {
        İşləyir = 10,
        Məzuniyyətdə = 20,
        Ezamiyyətdə = 30,
        İşdən_ayrılıb = 90,
        Silinib = 95,
    }

    public enum UsersPermissionsModules
    {
        Müraciətlər = 10,
        Vətəndaşlar = 20,
        Qiymətləndirilənlər = 30,
        SIB_R = 40,
        Xidmətdən_istifadələr = 50,
        CASE_açılanlar = 60,
        Ailə_səfərləri = 70,
        Təlim_və_tədbirlər = 80,
        Təqvim_planı = 82,
        Kurslar = 83,
        Elektron_jurnal = 84,
        Sorağçlar = 90,
        Demoqrafik_məlumatlar = 95,
        Hesabatlar = 100,
        Yükləmələr = 105,
        Qaleriya = 106,
        İşçilər = 110,
        Əks_əlaqə = 190
    }

    public enum ListPersonsStatus
    {
        Müraciətçi = 10,
        Məxfi = 20,
        Əsas_şəxs = 30
    }

    public enum ReportsTypes
    {
        Aylıq_hesabat = 10,
        İllik_hesabat = 20
    }

    public enum SIBRStatus
    {
        Aktiv = 10,
        Deaktiv = 20
    }

    public enum SIBRTypes
    {
        Short_Form = 10,
        Early_Development_Form = 20,
        Full_Scale = 30
    }

    public enum ApplicationsStatus
    {
        İcra_olunur = 10,
        Həll_olunub_təmin_edilib = 20,
        Qismən_təmin_edilib = 30,
        Müvafiq_izahat_verilib = 40,
        Əsassız_sayılıb = 50,
        Baxılması_üçün_aidiyyatı_quruma_göndərilib = 60,
    }

    public enum ListOperationTypes
    {
        [Description("EvaluationsID")]
        Qiymətləndirilənlər = 10,
        [Description("ApplicationsPersonsServicesID")]
        Xidmətdən_istifadə_edənlər = 20,
        [Description("ApplicationsFamilyID")]
        Ailə_səfəri_olunanlar = 30,
        [Description("ApplicationsCaseID")]
        CASE_açılanlar = 40
    }

    public enum Query
    {
        [Description("Where IsActive=1 Order By Priority asc")]
        IsActive_Priority_asc,
        [Description("Where IsActive=1 Order By ID asc")]
        IsActive_ID_asc,
        [Description("Order By ID asc")]
        ID_asc,
        [Description("Order By ID des")]
        ID_desc,
    }

    public enum Result
    {
        [Description("Sistemdə yüklənmə var. Daha sonra cəhd edin.")]
        Dt_Null = -1,
        [Description("Məlumat tapılmadı.")]
        Dt_Rows_Count_0 = 0,
        [Description("Əməliyyat uğurla yerinə yetirildi.")]
        Succes = 1
    }

    public enum SIBRMaladaptiveScoresDetailsTypes
    {
        Hurtful_to_Self = 1,
        Hurtful_to_Others = 2,
        Destructive_to_Property = 3,
        Disruptive_Behavior = 4,
        Unusual_or_Repetitive_Habits = 5,
        Socially_Offensive_Behavior = 6,
        Withdrawal_or_Inattentive_Behavior = 7,
        Uncooperative_Behavior = 8
    }

    public enum SIBRNormFTypes
    {
        Short_Form = 1,
        Early_Development_Form = 2,
        Motor_Skills = 3,
        Social_Interaction_Communication_Skills = 4,
        Personal_Living_Skills = 5,
        Community_Living_Skills = 6,
        Broad_Independence = 7,
    }

    public enum SIBRNormITypes
    {
        Broad_Independence_W = 1,
        Short_Form_W = 2,
        Early_Development_Form_W = 3
    }

    public enum SIBRScoringTypes
    {
        [Description("A. Gross Motor / Adaptive Behavior")]
        Gross_Motor = 1,

        [Description("B. Fine Motor / Adaptive Behavior")]
        Fine_Motor = 2,

        [Description("C. Social Interaction / Adaptive Behavior")]
        Social_Interaction = 3,

        [Description("D. Language Comprehension / Adaptive Behavior")]
        Language_Comprehension = 4,

        [Description("E. Language Expression / Adaptive Behavior")]
        Language_Expression = 5,

        [Description("F. Eating & Meal Preparation / Adaptive Behavior")]
        Eating_Meal_Preparation = 6,

        [Description("G. Toileting / Adaptive Behavior")]
        Toileting = 7,

        [Description("H. Dressing / Adaptive Behavior")]
        Dressing = 8,

        [Description("I. Personal Self-Care / Adaptive Behavior")]
        Personal_Self_Care = 9,

        [Description("J. Domestic Skills / Adaptive Behavior")]
        Domestic_Skills = 10,

        [Description("K. Time & Punctuality / Adaptive Behavior")]
        Time_Punctuality = 11,

        [Description("L. Money & Value / Adaptive Behavior")]
        Money_Value = 12,

        [Description("M. Work Skills / Adaptive Behavior")]
        Work_Skills = 13,

        [Description("N. Home/Community Orientation / Adaptive Behavior")]
        Home_Community_Orientation = 14,

        [Description("Adaptive Behavior/Early Development Form")]
        Broad_Independence = 15
    }

    public enum SIBRScoringTypesGroups
    {
        //Description ichindekiler SIBRScoringTypesGroups-a aid olan SIBRScoringTypesID-lerdi

        [Description(",1,2,")]
        Motor_Skills = 1,

        [Description(",3,4,5,")]
        Social_Interaction_Communication_Skills = 2,

        [Description(",6,7,8,9,10,")]
        Personal_Living_Skills = 3,

        [Description(",11,12,13,14,")]
        Community_Living_Skills = 4,

        [Description()]
        Broad_Independence_Full_Scale = 5
    }

    public enum DownloadsTypes
    {
        Digər = 1,
        Təlim_Tədbir = 2,
        Müraciət = 3,
        İzahat = 4,
        Sifariş = 5,
        Yeni_sənəd_forması = 6,
        Qalareya = 7,
    }

    public enum DownloadsQualityTypes
    {
        Əla = 10,
        Yaxşı = 20,
        Kafi = 30,
        Qiymətləndirilməyib = 40
    }

    public enum ServicesCoursesLessonsUseHistoryTypes
    {
        İştirak_edib = 10,
        İştirak_etməyib = 20
    }

    public enum EvaluationsSkillGroups
    {
        Böyük_motor_bacarıqları = 10,
        Kiçik_motor_bacarıqları = 20,
        Qavrama_bacarıqları = 30,
        Sosial_bacarıqlar = 40,
        Özünə_xidmət_bacarıqları = 50
    }

    public enum EvaluationsSkillPoints
    {
        İcra_edir = 10,
        Şifahi_dəstək = 20,
        Fiziki_dəstək = 30,
        Asılı = 40
    }

}