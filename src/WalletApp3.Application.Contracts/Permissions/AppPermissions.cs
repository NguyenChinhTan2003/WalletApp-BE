namespace WalletApp3.Permissions;

public static class AppPermissions
{
    public const string GroupName = "App";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
        public const string Tenant = DashboardGroup + ".Tenant";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

  

    public static class PaymentTypeCategories
    {
        public const string Default = GroupName + ".PaymentTypeCategories";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class BillDetails
    {
        public const string Default = GroupName + ".BillDetails";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string ViewTotalBalance = Default + ".ViewTotalBalance";
        public const string ViewTotalToDay = Default + ".ViewTotalToDay";
        public const string ViewTotalCategory = Default + ".ViewTotalCategory";
        public const string ViewCategory = Default + ".ViewCategory";
        public const string ViewTotalToDate = Default + ".ViewTotalToDate";


    }
}