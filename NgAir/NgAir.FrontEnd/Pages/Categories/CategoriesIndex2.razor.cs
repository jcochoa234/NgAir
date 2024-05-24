using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Paging;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;

namespace NgAir.FrontEnd.Pages.Categories
{
    public partial class CategoriesIndex2
    {

        [Inject] private IRepository Repository { get; set; } = null!;
        //[Inject] private SweetAlertService SweetAlertService { get; set; } = null!;


        [Inject] private ModalService _modalService { get; set; } = null!;


        public List<Category>? Categories { get; set; }

        bool _visible = false;


        public bool _loading = false;

        public int _total;
        public IEnumerable<Category> _items;

        TableFilter<string>[] _genderFilters = new[]
        {
            new TableFilter<string> { Text = "Male", Value = "male" },
            new TableFilter<string> { Text = "Female", Value = "female" },
        };


        public async Task HandleTableChange(QueryModel<Category> queryModel)
        {
            _loading = true;

            var url = $"api/categories/paged?" + GetRandomuserParams(queryModel);

            var responseHttp = await Repository.GetAsync<PagingResponse<Category>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await _modalService.ErrorAsync(new ConfirmOptions
                {
                    Title = "Error",
                    Content = message,
                    OkText = "Close"
                });
            }



            var modalConfig = new ModalOptions
            {
                Title = "Recharge Your Account",
                Centered = true,
                OkText = "Ok",
                Width = 500,
            };
            _modalService.CreateModal<CategoryCreate>(modalConfig);

            Categories = responseHttp.Response?.Items.ToList();
            _loading = false;
            _total = (int)(responseHttp.Response?.Total!);

        }

        public string GetRandomuserParams(QueryModel<Category> queryModel)
        {
            List<string> query = new List<string>()
        {
            $"pageSize={queryModel.PageSize}",
            $"pageNumber={queryModel.PageIndex}",
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

            return string.Join('&', query);
        }
    }
}