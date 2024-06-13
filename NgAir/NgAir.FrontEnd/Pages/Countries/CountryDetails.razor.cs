using AntDesign;
using AntDesign.TableModels;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Pages.States;
using NgAir.FrontEnd.Paging;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;
using System.Net;

namespace NgAir.FrontEnd.Pages.Countries
{
    [Authorize(Roles = "Admin")]
    public partial class CountryDetails
    {

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private AntDesign.ModalService ModalService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public int CountryId { get; set; }

        private Country? country;
        public List<State>? States { get; set; }

        private bool Loading = false;

        private int Total;

        private IEnumerable<State>? _items;

        private Table<State> Table;
        private QueryModel SavedQueryModel;


        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            await LoadCountryAsync();
        }

        private async Task<bool> LoadCountryAsync()
        {
            var responseHttp = await Repository.GetAsync<Country>($"/api/countries/{CountryId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/countries");
                    return false;
                }

                var message = await responseHttp.GetErrorMessageAsync();
                await ModalService.ErrorAsync(new ConfirmOptions
                {
                    Title = "Error",
                    Content = message,
                    OkText = "Close"
                });
                return false;
            }
            country = responseHttp.Response;
            return true;
        }

        private async Task HandleTableChange(QueryModel<State> queryModel)
        {
            Loading = true;

            var url = $"api/states/paged?Id={CountryId}&" + GetRandomuserParams(queryModel);

            var responseHttp = await Repository.GetAsync<PagingResponse<State>>(url);
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

            States = responseHttp.Response?.Items.ToList();
            Loading = false;
            Total = (int)(responseHttp.Response?.Total!);
        }

        private static string GetRandomuserParams(QueryModel<State> queryModel)
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

        private void ShowModal(int id = 0, bool isEdit = false, int countryId = 0)
        {
            var modalConfig = new ModalOptions
            {
                Title = isEdit ? "Edit State" : "Create State",
                Centered = true,
                OkText = "Ok",
                Width = 500,
                Footer = null,
                Content = builder =>
                {
                    if (isEdit)
                    {
                        builder.OpenComponent(0, typeof(StateEdit));
                        builder.AddAttribute(1, "StateId", id);
                        builder.AddAttribute(2, "OnSave", EventCallback.Factory.Create<string>(this, OnModalSave));
                    }
                    else
                    {
                        builder.OpenComponent(0, typeof(StateCreate));
                        builder.AddAttribute(1, "CountryId", countryId);
                        builder.AddAttribute(2, "OnSave", EventCallback.Factory.Create<string>(this, OnModalSave));
                    }
                    builder.CloseComponent();
                },

            };

            ModalService.CreateModal(modalConfig);
        }

        private async Task DeleteAsycn(State state)
        {

            var options = new ConfirmOptions
            {
                Title = "Confirmation",
                OkText = "Delete",
                Content = $"Are you sure you want to delete the state: {state.Name}?",
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
                var responseHttp = await Repository.DeleteAsync<State>($"api/states/{state.Id}");
                if (responseHttp.Error)
                {
                    if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        NavigationManager.NavigateTo("/states");
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