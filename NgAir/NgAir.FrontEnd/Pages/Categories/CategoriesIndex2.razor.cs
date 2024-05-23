using AntDesign;
using AntDesign.TableModels;
using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;
using System.ComponentModel;
using System.Net;

namespace NgAir.FrontEnd.Pages.Categories
{
    public partial class CategoriesIndex2
    {

        private int currentPage = 1;
        private int totalPages;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string PageNumber { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int PageSize { get; set; } = 10;
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        public List<Category>? Categories { get; set; }



        bool _loading = false;

        int _total;
        List<Category> _data = new List<Category>();
        IEnumerable<Category> _items;
        TableFilter<string>[] _genderFilters = new[]
        {
            new TableFilter<string> { Text = "Male", Value = "male" },
            new TableFilter<string> { Text = "Female", Value = "female" },
        };



        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        {
            IModalReference modalReference;

            if (isEdit)
            {
                modalReference = Modal.Show<CategoryEdit>(string.Empty, new ModalParameters().Add("Id", id));
            }
            else
            {
                modalReference = Modal.Show<CategoryCreate>();
            }

            var result = await modalReference.Result;
            if (result.Confirmed)
            {
                await LoadAsync();
            }
        }

        private async Task SelectedPageSizeAsync(int pageSize)
        {
            PageSize = pageSize;
            int pageNumber = 1;
            await LoadAsync(pageNumber);
            await SelectedPageNumberAsync(pageNumber);
        }
        private async Task FilterCallBack(string filter)
        {
            Filter = filter;
            await ApplyFilterAsync();
            StateHasChanged();
        }

        private async Task SelectedPageNumberAsync(int pageNumber)
        {
            if (!string.IsNullOrWhiteSpace(PageNumber))
            {
                pageNumber = Convert.ToInt32(PageNumber);
            }

            currentPage = pageNumber;
            await LoadAsync(pageNumber);
        }

        private async Task LoadAsync(int pageNumber = 1)
        {
            var ok = await LoadListAsync(pageNumber);
            if (ok)
            {
                await LoadPagesAsync();
            }
        }

        private async Task<bool> LoadListAsync(int pageNumber)
        {
            ValidatePageSize();
            var url = $"api/categories/?pageNumber={pageNumber}&PageSize={PageSize}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var url2 = $"api/categories/paged?pageNumber={pageNumber}&PageSize={PageSize}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url2 += $"&filter={Filter}";
            }

            try
            {
                var responseHttp2 = await Repository.GetAsync<IEnumerable<Category>>(url2);
                if (responseHttp2.Error)
                {
                    var message2 = await responseHttp2.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message2, SweetAlertIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
            }



            var responseHttp = await Repository.GetAsync<List<Category>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Categories = responseHttp.Response;


            //ApiResponse data = await Http.GetFromJsonAsync<ApiResponse>("https://randomuser.me/api?" + GetRandomuserParams(queryModel));
            //int i = 0;
            //foreach (var item in data.Results)
            //{
            //    //item.IID = queryModel.StartIndex + i++;
            //}

            _loading = false;
            _data = Categories;
            _total = 20;



            return true;
        }

        private async Task LoadPagesAsync()
        {
            ValidatePageSize();
            var url = $"api/categories/totalPages?pageSize={PageSize}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<int>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = responseHttp.Response;
        }

        private void ValidatePageSize()
        {
            if (PageSize == 0)
            {
                PageSize = 10;
            }
        }

        private async Task ApplyFilterAsync()
        {
            int pageNumber = 1;
            await LoadAsync(pageNumber);
            await SelectedPageNumberAsync(pageNumber);
        }

        private async Task DeleteAsycn(Category category)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmation",
                Text = $"Are you sure you want to delete the category: {category.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Category>($"api/categories/{category.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/categories");
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Record successfully deleted.");
        }





        async Task HandleTableChange(QueryModel<Category> queryModel)
        {
            _loading = true;


            await LoadAsync();


            //////ApiResponse data = await Http.GetFromJsonAsync<ApiResponse>("https://randomuser.me/api?" + GetRandomuserParams(queryModel));
            //////int i = 0;
            //////foreach (var item in data.Results)
            //////{
            //////    //item.IID = queryModel.StartIndex + i++;
            //////}

            //////_loading = false;
            //////_data = data.Results;
            //////_total = data.TotalCount;
        }

        string GetRandomuserParams(QueryModel<Category> queryModel)
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
            public Category[] Results { get; set; }

            public int TotalCount { get; set; } = 200; // 200 is mock data, you should read it from server
        }
    }
}