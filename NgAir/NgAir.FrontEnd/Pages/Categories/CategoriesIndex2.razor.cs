using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Paging;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;
using System.Net;

namespace NgAir.FrontEnd.Pages.Categories
{
    public partial class CategoriesIndex2
    {

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private IMessageService Message { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;


        public List<Category>? Categories { get; set; }

        bool _visible = false;


        public bool Loading = false;

        public int _total;
        public IEnumerable<Category> _items;

        TableFilter<string>[] _genderFilters = new[]
        {
            new TableFilter<string> { Text = "Male", Value = "male" },
            new TableFilter<string> { Text = "Female", Value = "female" },
        };


        public async Task HandleTableChange(QueryModel<Category> queryModel)
        {
            Loading = true;

            var url = $"api/categories/paged?" + GetRandomuserParams(queryModel);

            var responseHttp = await Repository.GetAsync<PagingResponse<Category>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await ModalService.ErrorAsync(new ConfirmOptions
                {
                    Title = "Error",
                    Content = message,
                    OkText = "Close"
                });
            }

            Categories = responseHttp.Response?.Items.ToList();
            Loading = false;
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
                Width = 500,
                Footer = null,
            };

            if (isEdit)
            {
                ModalService.CreateModal<CategoryEdit, int>(modalConfig, id);
            }
            else
            {
                ModalService.CreateModal<CategoryCreate, string>(modalConfig, "");
            }
        }



        private async Task DeleteAsycn(Category category)
        {

            var options = new ConfirmOptions
            {
                Title = "Confirmation",
                OkText = "Delete",
                Content = $"Are you sure you want to delete the category: {category.Name}?",
                Centered = true,
                //Icon = @<Icon Type="exclamation-circle" Theme="outline"/>,
                Button1Props =
                {
                    Danger = true,
                    Shape = ButtonShape.Round,
                    Icon = "delete",
                },
                Button2Props =
                {
                    Shape = ButtonShape.Round,
                    Icon = "close"
                }
            };

            var result = await ModalService.ConfirmAsync(options);
            if (result)
            {
                Loading = true;
                var responseHttp = await Repository.DeleteAsync<Category>($"api/categories/{category.Id}");
                if (responseHttp.Error)
                {
                    if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        NavigationManager.NavigateTo("/categories2");
                    }
                    else
                    {
                        Loading = false;
                        var mensajeError = await responseHttp.GetErrorMessageAsync();
                        await ModalService.ErrorAsync(new ConfirmOptions
                        {
                            Title = "Error",
                            Content = mensajeError,
                            OkText = "Close"
                        });
                    }
                    return;
                }
            }
            else
            {
                return;
            }
            return;
        }


    }
}


////Ant Blazor Modal Using Template Component