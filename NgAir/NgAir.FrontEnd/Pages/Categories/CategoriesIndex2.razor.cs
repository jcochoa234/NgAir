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

        private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        {

            var modalConfig = new AntDesign.ModalOptions
            {
                Title = isEdit ? "Edit Category" : "Create Category",
                Centered = true,
                OkText = "Ok",
                Width = 500
            };


            if (isEdit)
            {
                //  modalReference = Modal.Show<CategoryEdit>(string.Empty, new ModalParameters().Add("Id", id));
                _modalService.CreateModal<CategoryEdit, string, string>(modalConfig, "120");
            }
            else
            {
                _modalService.CreateModal<CategoryCreate, string, string>(modalConfig, "120");
            }
        }
    }
}


////Ant Blazor Modal Using Template Component