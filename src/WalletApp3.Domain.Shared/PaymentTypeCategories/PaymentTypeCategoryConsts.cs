namespace WalletApp3.PaymentTypeCategories
{
    public static class PaymentTypeCategoryConsts
    {
        private const string DefaultSorting = "{0}CategoryName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "PaymentTypeCategory." : string.Empty);
        }

        public const int CategoryNameMinLength = 1;
        public const int CategoryNameMaxLength = 50;
        public const int DescriptionMinLength = 5;
        public const int DescriptionMaxLength = 250;
    }
}