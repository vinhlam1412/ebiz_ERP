namespace HQSOFT.Configuration.CSAttributeDetails
{
    public static class CSAttributeDetailConsts
    {
        private const string DefaultSorting = "{0}ValueID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CSAttributeDetail." : string.Empty);
        }

        public const int ValueIDMaxLength = 10;
        public const int DescriptionMaxLength = 60;
    }
}