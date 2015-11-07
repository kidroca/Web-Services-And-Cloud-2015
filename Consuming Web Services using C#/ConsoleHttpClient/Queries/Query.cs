namespace ConsoleHttpClient.Queries
{
    using System;

    public enum SortOrder
    {
        // Casing need to be capital
        ASC = 0,
        DESC = 1
    }

    public abstract class Query
    {
        private string endpoint;

        public string Uuid { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }

        /// <summary>
        /// Set the field name to be used to sort by
        /// </summary>
        public string SortByFieldName { get; set; }

        public SortOrder? SortOrder { get; set; }

        public string Endpoint
        {
            get
            {
                return this.endpoint;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Cannot assign null or empty string to an Endpoint");
                }

                this.endpoint = value;
            }
        }

        public abstract Query AddField(string field);

        public abstract Query RemoveField(string field);

        public abstract string GetQueryString();
    }
}
