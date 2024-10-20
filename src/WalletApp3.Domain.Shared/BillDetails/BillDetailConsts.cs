namespace WalletApp3.BillDetails
{
    public static class BillDetailConsts
    {
        private const string DefaultSorting = "{0}Money asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "BillDetail." : string.Empty);
        }

        public const int NoteMinLength = 5;
        public const int NoteMaxLength = 250;
    }
}