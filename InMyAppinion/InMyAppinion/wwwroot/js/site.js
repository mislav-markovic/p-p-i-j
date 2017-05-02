function AddCommentAjax(selector, url, paramname, username) {
    $(document).on('click', selector, function (event) {
        var paramval = $('#' + paramname).val();
        var user = $('#' + username).val();
        //$("#tempmessage").removeClass("alert-success");
        //$("#tempmessage").removeClass("alert-danger");
        //$("#tempmessage").html('');
        $.post(url, { text: paramval }, function (data) {
            $("#tempmessage").addClass(data.success ? "alert-success" : "alert-danger");
            //$("#tempmessage").addClass("panel-body");
            //$("#tempmessage").html(data.message);
            if (data.success) {
                $('#comments').append('<div id="' + data.commentId + '"></div>');
                $('#' + data.commentId).append('<button data-commentid="' + data.commentId + '" data-userid="' + data.userId + '" class="newButton"><span class="newSpan"></span></button>');
                $('.newButton').addClass("pull-right btn btn-danger btn-xs deleteajax");
                $('.newSpan').addClass("glyphicon glyphicon-remove");
                $('#' + data.commentId).append('<h5><span>' + user + '</span></h5>' + '<p class="comment"> ' + paramval + '</p ><hr/>');
                $('#text').val('');
                console.log(data.commentId);
            }
        });
    });
}

function DeleteCommentAjax(selector, url, paramname) {
    $(document).on('click', selector, function (event) {
        event.preventDefault();
        var paramval = $(this).data(paramname);
        console.log(paramval);
        $.post(url, { id: paramval }, function (data) {
            if (data.success) {
                $('#' + paramval).remove();
            }
        });
    });
}

function HideDeleteButtons(btnClass, user) {
    var x = document.getElementsByClassName(btnClass);
    var i;
    for (i = 0; i < x.length; i++) {
        if (x[i].dataset.userid === user) {
            x[i].style.visibility = 'visible';
        } else {
            x[i].style.visibility = 'hidden';
        }
    }
}
