using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using NgAir.Shared.Interfaces;

namespace NgAir.FrontEnd.Shared
{
    public partial class FormWithName<TModel> where TModel : IEntityWithName
    {
        private EditContext editContext = null!;

        [EditorRequired, Parameter] public TModel Model { get; set; } = default!;
        [EditorRequired, Parameter] public string Label { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [Inject] private ModalService ModalService { get; set; } = null!;

        public bool FormPostedSuccessfully { get; set; }


        protected override void OnInitialized()
        {
            editContext = new(Model);
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var options = new ConfirmOptions
            {
                Title = "Confirmation",
                OkText = "Delete",
                Content = "Do you wish to leave the page and lose the changes?",
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
                return;
            }

            context.PreventNavigation();
        }
    }
}