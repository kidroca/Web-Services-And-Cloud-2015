namespace ConsoleHttpClient.Queries
{
    using System.Collections.Generic;
    using System.Text;

    public class JusticeQuery : Query
    {
        public static readonly string[] DefaultEndpoints =
        {
            "blog_entries", "press_releases", "speeches", "vacancy_announcements"
        };

        private HashSet<string> fields;

        public JusticeQuery(string endpoint)
        {
            this.fields = new HashSet<string>();
            this.Endpoint = endpoint;
        }

        public override Query AddField(string field)
        {
            this.fields.Add(field);
            return this;
        }

        public override Query RemoveField(string field)
        {
            this.fields.Remove(field);
            return this;
        }

        public override string GetQueryString()
        {
            if (this.Uuid != null)
            {
                return $"{this.Endpoint}/{this.Uuid}";
            }

            var queryBuilder = new StringBuilder();
            queryBuilder.Append(this.Endpoint);

            List<string> parameters = this.GetQueryStringParameters();

            if (parameters.Count > 0)
            {
                queryBuilder.Append($"?{parameters[0]}");
            }

            for (int i = 1; i < parameters.Count; i++)
            {
                queryBuilder.Append($"&{parameters[i]}");
            }

            return queryBuilder.ToString();
        }

        private List<string> GetQueryStringParameters()
        {
            List<string> parameters = new List<string>();

            if (this.fields.Count > 0)
            {
                string fieldsQuery = string.Format("fields={0}", string.Join(",", this.fields));
                parameters.Add(fieldsQuery);
            }

            if (this.Page != null)
            {
                parameters.Add($"page={this.Page}");
            }

            if (this.PageSize != null)
            {
                parameters.Add($"pagesize={this.PageSize}");
            }

            if (this.SortByFieldName != null)
            {
                parameters.Add($"sort={this.SortByFieldName}");
            }

            if (this.SortOrder != null)
            {
                parameters.Add($"direction={this.SortOrder}");
            }

            return parameters;
        }
    }
}