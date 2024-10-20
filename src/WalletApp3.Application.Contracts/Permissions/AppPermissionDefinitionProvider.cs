using WalletApp3.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace WalletApp3.Permissions;

public class AppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AppPermissions.GroupName);


        //Define your own permissions here. Example:
        //myGroup.AddPermission(AppPermissions.MyPermission1, L("Permission:MyPermission1"));

        var paymentTypeCategoryPermission = myGroup.AddPermission(AppPermissions.PaymentTypeCategories.Default, L("Permission:PaymentTypeCategories"));
        paymentTypeCategoryPermission.AddChild(AppPermissions.PaymentTypeCategories.Create, L("Permission:Create"));
        paymentTypeCategoryPermission.AddChild(AppPermissions.PaymentTypeCategories.Edit, L("Permission:Edit"));
        paymentTypeCategoryPermission.AddChild(AppPermissions.PaymentTypeCategories.Delete, L("Permission:Delete"));

        var billDetailPermission = myGroup.AddPermission(AppPermissions.BillDetails.Default, L("Permission:BillDetails"));
        billDetailPermission.AddChild(AppPermissions.BillDetails.Create, L("Permission:Create"));
        billDetailPermission.AddChild(AppPermissions.BillDetails.Edit, L("Permission:Edit"));
        billDetailPermission.AddChild(AppPermissions.BillDetails.Delete, L("Permission:Delete"));
        billDetailPermission.AddChild(AppPermissions.BillDetails.ViewTotalBalance, L("Permission:ViewTotalBalance"));
        billDetailPermission.AddChild(AppPermissions.BillDetails.ViewTotalToDay, L("Permission:ViewTotalToDay"));
        billDetailPermission.AddChild(AppPermissions.BillDetails.ViewTotalCategory, L("Permission:ViewTotalCategory"));
        billDetailPermission.AddChild(AppPermissions.BillDetails.ViewCategory, L("Permission:ViewCategory"));
        billDetailPermission.AddChild(AppPermissions.BillDetails.ViewTotalToDate, L("Permission:ViewTotalToDate"));



    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WalletApp3Resource>(name);
    }
}