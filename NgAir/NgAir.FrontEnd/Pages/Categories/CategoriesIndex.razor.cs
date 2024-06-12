using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Helpers;
using NgAir.FrontEnd.Paging;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;
using System.Net;
using System.Text.Json;

namespace NgAir.FrontEnd.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public partial class CategoriesIndex
    {

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private List<Category>? Categories { get; set; }

        private bool Loading = false;

        private int Total;

        private IEnumerable<Category>? _items;

        private Table<Category> Table;
        private QueryModel SavedQueryModel;


        private async Task HandleTableChange(QueryModel<Category> queryModel)
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
            Total = (int)(responseHttp.Response?.Total!);
        }

        private static string GetRandomuserParams(QueryModel<Category> queryModel)
        {
            List<string> query =
            [
                $"pageSize={queryModel.PageSize}",
                $"pageNumber={queryModel.PageIndex}",
            ];

            queryModel.SortModel.ForEach(x =>
            {
                query.Add($"sortField={x.FieldName.ToLower()}");
                query.Add($"sortOrder={x.Sort}");
            });

            List<ColumnFilter> columnFilters = [];
            queryModel.FilterModel.ForEach(filter =>
            {
                filter.SelectedValues.ForEach(value =>
                {
                    columnFilters.Add(new ColumnFilter { id = filter.FieldName.ToLower(), value = value });
                });
            });

            if (columnFilters.Count > 0)
            {
                var json = JsonSerializer.Serialize(columnFilters);
                query.Add($"ColumnFilters=" + json);
            }

            return string.Join('&', query);
        }

        private void ShowModal(int id = 0, bool isEdit = false)
        {
            var modalConfig = new ModalOptions
            {
                Title = isEdit ? "Edit Category" : "Create Category",
                Centered = true,
                OkText = "Ok",
                Width = 500,
                Footer = null,
                Content = builder =>
                {
                    if (isEdit)
                    {
                        builder.OpenComponent(0, typeof(CategoryEdit));
                        builder.AddAttribute(1, "Id", id);
                        builder.AddAttribute(1, "paramenter2", id);
                        builder.AddAttribute(1, "paramenter", "valueparamenter");
                    }
                    else
                    {
                        builder.OpenComponent(0, typeof(CategoryCreate));
                    }

                    builder.CloseComponent();
                }
            };

            ModalService.CreateModal(modalConfig);

            //if (isEdit)
            //{
            //    ModalService.CreateModalAsync(modalConfig);
            //    //ModalService.CreateModal<CategoryEdit, int>(modalConfig, id);
            //}
            //else
            //{
            //    ModalService.CreateModalAsync(modalConfig);
            //    //ModalService.CreateModal<CategoryCreate, string>(modalConfig, "");
            //}
        }

        private async Task DeleteAsycn(Category category)
        {

            var options = new ConfirmOptions
            {
                Title = "Confirmation",
                OkText = "Delete",
                Content = $"Are you sure you want to delete the category: {category.Name}?",
                Centered = true,
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
                        NavigationManager.NavigateTo("/categories");
                    }
                    else
                    {
                        Loading = false;
                        var messageError = await responseHttp.GetErrorMessageAsync();
                        await ModalService.ErrorAsync(new ConfirmOptions
                        {
                            Title = "Error",
                            Content = messageError,
                            OkText = "Close"
                        });
                    }
                }
                else
                {
                    Loading = false;
                    LoadTable();
                    await ModalService.SuccessAsync(new ConfirmOptions
                    {
                        Title = "Success",
                        Content = "Record successfully deleted.",
                        OkText = "Close"
                    });
                }
            }
        }

        void LoadTable()
        {
            SavedQueryModel = Table.GetQueryModel();
            Table.ReloadData(SavedQueryModel);
        }

    }
}