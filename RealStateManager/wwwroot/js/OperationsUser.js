function ApproveUser(userId, name) {
    const url = "/User/ApproveUser";

    $.ajax({
        method: 'POST',
        url: url,
        data: { userId: userId },
        success: function (data) {
            if (data === true) {
                $('#' + userId).removeClass("purple darken-3").addClass("green darken-3").text("Approved")

                $("." + userId).children("a").remove();
                $("." + userId).append('<a class="btn-floating blue darken-4" href="User/UserManagement?userId=' + userId + '&name=' + name + '" asp-controller="User" asp-action="UserManagement" asp-route-userId="' + userId + '" asp-route-name="' + name + '"><i class="material-icons">group</i></a>');

                M.toast({
                    html: "User approved",
                    classes: "green darken-3"
                });
            }
            else {
                M.toast({
                    html: "Unable to approve user"
                });
            }
        }
    })
}

function DisapproveUser(userId) {
    const url = "/User/DisapproveUser";

    $.ajax({
        method: 'POST',
        url: url,
        data: { userId: uerId },
        success: function (data) {
            if (data === true) {
                $("#" + userId).removeClass("purple darken-3").addClass("orange darken-3").text("Disapproved");
                $("." + userId).remove();

                M.toast({
                    html: "Disapproved User",
                    classes: "orange darken-3"
                });
            }

            else {
                M.toast({
                    html: "Unable to approve user"
                });
            }
        }
    });
}