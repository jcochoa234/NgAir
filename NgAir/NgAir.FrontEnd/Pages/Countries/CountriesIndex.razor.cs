using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Paging;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;
using System.Net;

namespace NgAir.FrontEnd.Pages.Countries
{
    [Authorize(Roles = "Admin")]
    public partial class CountriesIndex
    {
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public EventCallback<string> OnSave { get; set; }

        public List<Country>? Countries { get; set; }

        private bool Loading = false;

        private int Total;

        private IEnumerable<Country>? _items;

        private Table<Country> Table;
        private QueryModel SavedQueryModel;

        private async Task HandleTableChange(QueryModel<Country> queryModel)
        {
            Loading = true;

            var url = $"api/countries/paged?" + GetRandomuserParams(queryModel);

            var responseHttp = await Repository.GetAsync<PagingResponse<Country>>(url);
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

            Countries = responseHttp.Response?.Items.ToList();
            Loading = false;
            Total = (int)(responseHttp.Response?.Total!);
        }

        private static string GetRandomuserParams(QueryModel<Country> queryModel)
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

            queryModel.FilterModel.ForEach(filter =>
            {
                filter.SelectedValues.ForEach(value =>
                {
                    query.Add($"{filter.FieldName.ToLower()}={value}");
                });
            });

            return string.Join('&', query);
        }

        private void ShowModal(int id = 0, bool isEdit = false)
        {
            var modalConfig = new ModalOptions
            {
                Title = isEdit ? "Edit Country" : "Create Country",
                Centered = true,
                OkText = "Ok",
                Width = 500,
                Footer = null,
                Content = builder =>
                {
                    if (isEdit)
                    {
                        builder.OpenComponent(0, typeof(CountryEdit));
                        builder.AddAttribute(1, "Id", id);
                        builder.AddAttribute(2, "OnSave", EventCallback.Factory.Create<string>(this, OnModalSave));
                    }
                    else
                    {
                        builder.OpenComponent(0, typeof(CountryCreate));
                        builder.AddAttribute(1, "OnSave", EventCallback.Factory.Create<string>(this, OnModalSave));
                    }
                    builder.CloseComponent();
                },

            };

            ModalService.CreateModal(modalConfig);
        }

        private async Task DeleteAsycn(Country country)
        {

            var options = new ConfirmOptions
            {
                Title = "Confirmation",
                OkText = "Delete",
                Content = $"Are you sure you want to delete the country: {country.Name}?",
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
                var responseHttp = await Repository.DeleteAsync<Country>($"api/countries/{country.Id}");
                if (responseHttp.Error)
                {
                    if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        NavigationManager.NavigateTo("/countries");
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

        private void OnModalSave(string newValue)
        {
            LoadTable();
        }

    }
}