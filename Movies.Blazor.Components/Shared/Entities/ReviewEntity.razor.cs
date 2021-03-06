using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Movies.Domain.Models;
using Movies.BusinessLogic.Results;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Infrastructure.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.Blazor.Components.Shared.Entities
{
    public partial class ReviewEntity
    {
        [Inject]
        private IReviewService reviewService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        [Parameter]
        public EventCallback<ReviewResponse> OnActionDoneAsync { get; set; }

        [Parameter]
        public ReviewResponse Review { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        private bool canEditAndDelete { get; set; }

        private bool confirmActionDialogOpen { get; set; }

        private int userId { get; set; }

        private string movieLink { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            var state = await authenticationStateTask;

            if (state.User.Identity != null && state.User.Identity.IsAuthenticated)
            {
                var claim = state.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (claim != null)
                {
                    userId = int.Parse(claim.Value);

                    if (state.User.IsInRole(Enum.GetName(UserRoles.Reviewer)) && Review.ReviewerId == userId)
                    {
                        canEditAndDelete = true;
                    }
                    else
                    {
                        canEditAndDelete = false;
                    }
                }
            }

            movieLink = $"/movies/{Review.MovieId}";

            await base.OnParametersSetAsync();
        }

        private void ShowDeleteDialog()
        {
            confirmActionDialogOpen = true;
            DynamicRender = CreateComponent("Delete",
                "Are you sure?",
                ConfirmDialog.ModalDialogType.DeleteCancel,
                null,
                OnDeletedAsync);
        }

        private void GoToEditReview()
        {
            navigationManager.NavigateTo($"/reviews/{Review.ReviewId}/edit");
        }

        private async Task OnDeletedAsync(bool confirm)
        {
            if (confirm)
            {
                var result = await reviewService.DeleteReviewAsync(userId, Review.ReviewId);

                if (result.ResultType == ResultType.Ok)
                {
                    await OnActionDoneAsync.InvokeAsync(Review);
                    confirmActionDialogOpen = false;
                }
                else
                {
                    DynamicRender = CreateComponent("Error", null, ConfirmDialog.ModalDialogType.Ok, result, OnConfirmAsync);
                }
            }

        }

        private async Task OnConfirmAsync(bool confirm)
        {
            confirmActionDialogOpen = false;
        }

        private RenderFragment DynamicRender { get; set; }

        private RenderFragment CreateComponent(string title, string text, ConfirmDialog.ModalDialogType dialogType, Result result, Func<bool, Task> task)
        {
            EventCallback<bool> callback = new EventCallbackFactory().Create<bool>(this, task);
            return new RenderFragment((builder) =>
            {
                builder.OpenComponent(0, typeof(ConfirmDialog));
                builder.AddAttribute(1, "Title", title);
                builder.AddAttribute(2, "Text", text);
                builder.AddAttribute(3, "DialogType", dialogType);
                builder.AddAttribute(4, "Result", result);
                builder.AddAttribute(4, "OnClose", callback);
                builder.CloseComponent();
            });

        }
    }
}
