using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using NgAir.FrontEnd.Helpers;
using NgAir.Shared.DTOs;
using NgAir.Shared.Entities;

namespace NgAir.FrontEnd.Pages.Products
{
    public partial class ProductForm
    {
        private EditContext editContext = null!;
        private string? imageUrl;

        private List<MultipleSelectorModel> selected { get; set; } = new();
        private List<MultipleSelectorModel> nonSelected { get; set; } = new();

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter, EditorRequired] public ProductDto ProductDto { get; set; } = null!;
        [Parameter, EditorRequired] public EventCallback OnValidSubmit { get; set; }
        [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
        [Parameter, EditorRequired] public List<Category> NonSelectedCategories { get; set; } = new();
        [Parameter] public bool IsEdit { get; set; } = false;
        [Parameter] public EventCallback AddImageAction { get; set; }
        [Parameter] public EventCallback RemoveImageAction { get; set; }
        [Parameter] public List<Category> SelectedCategories { get; set; } = new();

        public bool FormPostedSuccessfully { get; set; } = false;

        protected override void OnInitialized()
        {
            editContext = new(ProductDto);

            selected = SelectedCategories.Select(x => new MultipleSelectorModel(x.Id.ToString(), x.Name)).ToList();
            nonSelected = NonSelectedCategories.Select(x => new MultipleSelectorModel(x.Id.ToString(), x.Name)).ToList();
        }

        private void ImageSelected(string imagenBase64)
        {
            if (ProductDto.ProductImages is null)
            {
                ProductDto.ProductImages = new List<string>();
            }

            ProductDto.ProductImages!.Add(imagenBase64);
            imageUrl = null;
        }

        private async Task OnDataAnnotationsValidatedAsync()
        {
            ProductDto.ProductCategoryIds = selected.Select(x => int.Parse(x.Key)).ToList();
            await OnValidSubmit.InvokeAsync();
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();

            if (!formWasEdited)
            {
                return;
            }

            if (FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmation",
                Text = "Do you wish to leave the page and lose the changes?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });

            var confirm = !string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }
    }
}