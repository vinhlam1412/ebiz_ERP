namespace HQSOFT.Configuration.CSAttributes
{
    public static class CSAttributeConsts
    {
        private const string DefaultSorting = "{0}AttributeID asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CSAttribute." : string.Empty);
        }

        public const int AttributeIDMaxLength = 10;
        public const int DescriptionMaxLength = 60;
        public const int EntryMaskMaxLength = 60;
        public const int RegExpMaxLength = 255;
        public const int ObjectNameMaxLength = 512;
        public const int FieldNameMaxLength = 512;
    }
}