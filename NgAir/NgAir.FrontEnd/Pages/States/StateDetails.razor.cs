using AntDesign;
using AntDesign.TableModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using NgAir.FrontEnd.Pages.Cities;
using NgAir.FrontEnd.Paging;
using NgAir.FrontEnd.Repositories;
using NgAir.Shared.Entities;
using System.Net;

namespace NgAir.FrontEnd.Pages.States
{
    [Authorize(Roles = "Admin")]
    public partial class StateDetails
    {
        private State? State;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ModalService ModalService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter] public int StateId { get; set; }

        public List<City>? Cities { get; set; }

        private bool Loading = false;

        private int Total;

        private IEnumerable<City>? _items;

        private Table<City> Table;
        private QueryModel SavedQueryModel;


        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            await LoadStateAsync();
        }

        private async Task<bool> LoadStateAsync()
        {
            var responseHttp = await Repository.GetAsync<State>($"/api/states/{StateId}");
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
            State = responseHttp.Response;
            return true;
        }

        private async Task HandleTableChange(QueryModel<City> queryModel)
        {
            Loading = true;

            var url = $"api/cities/paged?" + GetRandomuserParams(queryModel);

            var responseHttp = await Repository.GetAsync<PagingResponse<City>>(url);
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

            Cities = responseHttp.Response?.Items.ToList();
            Loading = false;
            Total = (int)(responseHttp.Response?.Total!);
        }

        private static string GetRandomuserParams(QueryModel<City> queryModel)
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

        private void ShowModal(int id = 0, bool isEdit = false, int stateId = 0)
        {
            var modalConfig = new ModalOptions
            {
                Title = isEdit ? "Edit City" : "Create City",
                Centered = true,
                OkText = "Ok",
                Width = 500,
                Footer = null,
            };

            if (isEdit)
            {
                ModalService.CreateModal<CityEdit, int>(modalConfig, id);
            }
            else
            {
                ModalService.CreateModal<CityCreate, int>(modalConfig, stateId);
            }
        }

        private async Task DeleteAsycn(City city)
        {

            var options = new ConfirmOptions
            {
                Title = "Confirmation",
                OkText = "Delete",
                Content = $"Are you sure you want to delete the city: {city.Name}?",
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
                var responseHttp = await Repository.DeleteAsync<City>($"api/cities/{city.Id}");
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


    }
}