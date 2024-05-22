using AntDesign;
using AntDesign.TableModels;
using System.ComponentModel;
using System.Net.Http.Json;

namespace NgAir.FrontEnd.Pages.Categories
{
    public partial class CategoriesIndex2
    {
        bool _loading = false;

        int _total;
        Data[] _data = Array.Empty<Data>();
        IEnumerable<Data> _items;
        TableFilter<string>[] _genderFilters = new[]
        {
            new TableFilter<string> { Text = "Male", Value = "male" },
            new TableFilter<string> { Text = "Female", Value = "female" },
        };

        async Task HandleTableChange(QueryModel<Data> queryModel)
        {
            _loading = true;

            ApiResponse data = await Http.GetFromJsonAsync<ApiResponse>("https://randomuser.me/api?" + GetRandomuserParams(queryModel));
            int i = 0;
            foreach (var item in data.Results)
            {
                item.IID = queryModel.StartIndex + i++;
            }

            _loading = false;
            _data = data.Results;
            _total = data.TotalCount;
        }

        string GetRandomuserParams(QueryModel<Data> queryModel)
        {
            List<string> query = new List<string>()
        {
            $"results={queryModel.PageSize}",
            $"page={queryModel.PageIndex}",
        };

            queryModel.SortModel.ForEach(x =>
            {
                query.Add($"sortField={x.FieldName.ToLower()}");
                query.Add($"sortOrder={x.Sort}");
            });

            queryModel.FilterModel.ForEach(filter =>
            {
                filter.SelectedValues.ForEach(value =>
                {
                    query.Add($"{filter.FieldName.ToLower()}={value}");
                });
            });

            string sss = string.Join('&', query);

            return string.Join('&', query);
        }

        public class Data
        {
            public int IID { get; set; }
            [DisplayName("Name")]
            public Name Name { get; set; }

            [DisplayName("Gender")]
            public string Gender { get; set; }

            [DisplayName("Email")]
            public string Email { get; set; }
        }

        public struct Name
        {
            public string First { get; set; }

            public string Last { get; set; }
        }

        public class ApiResponse
        {
            public Data[] Results { get; set; }

            public int TotalCount { get; set; } = 200; // 200 is mock data, you should read it from server
        }
    }
}