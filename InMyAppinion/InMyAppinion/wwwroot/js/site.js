function AddCommentAjax(selector, url, paramname, username) {
    $(document).on('click', selector, function (event) {
        var paramval = $('#' + paramname).val();
        var user = $('#' + username).val();
        console.log(url);
        $("#tempmessage").removeClass("alert-success");
        $("#tempmessage").removeClass("alert-danger");
        $("#tempmessage").html('');
        $.post(url, { text: paramval }, function (data) {
            $("#tempmessage").addClass(data.success ? "alert-success" : "alert-danger");
            $("#tempmessage").addClass("panel-body");
            $("#tempmessage").html(data.message);
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

function SetEditAjax(selector, url, paramname) {
    $(document).on('click', selector, function (event) {
        event.preventDefault();
        var paramval = $(this).data(paramname);
        $("#tempmessage").removeClass("alert-success");
        $("#tempmessage").removeClass("alert-danger");
        $("#tempmessage").html('');
        var chk = $(this).is(":checked");
        var x = $(this);
        console.log(chk);
        $.get(url, { id: paramval }, function (data) {
            $("#tempmessage").addClass(data.success ? "alert-success" : "alert-danger");
            if (data.success) {
                $(x).prop("checked", chk);
            }
            $("#tempmessage").addClass("panel-body");
            $("#tempmessage").html(data.message);
        });
    });
}

function DeleteCommentAjax(selector, url, paramname) {
    $(document).on('click', selector, function (event) {
        event.preventDefault();
        $("#tempmessage").removeClass("alert-success");
        $("#tempmessage").removeClass("alert-danger");
        $("#tempmessage").html('');
        var paramval = $(this).data(paramname);
        console.log(paramval);
        $.post(url, { id: paramval }, function (data) {
            $("#tempmessage").addClass(data.success ? "alert-success" : "alert-danger");
            if (data.success) {
                $('#' + paramval).remove();
            }
            $("#tempmessage").addClass("panel-body");
            $("#tempmessage").html(data.message);
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

function VoteReview(selector, url) {
    $(document).on('click', selector, function (event) {
        event.preventDefault();
        var vote = $(this).data('vote');
        console.log(selector);
        console.log(vote);
        console.log(url);

        $("#tempmessage").removeClass("alert-success");
        $("#tempmessage").removeClass("alert-danger");
        $("#tempmessage").html('');
        $.post(url, { vote: vote }, function (data) {
            $("#tempmessage").addClass("panel-body");
            $("#tempmessage").addClass(data.successful ? "alert-success" : "alert-danger");
            $("#tempmessage").html(data.message);
            console.log(data.message);
            if (data.successful) {
                $("#points").text(data.points);
                if (vote === true) {
                    $("#voteup").prop('disabled', true);
                    $("#voteup").hide();
                    $("#votedown").prop('disabled', false);
                    $("#votedown").show();
                } else {
                    $("#votedown").prop('disabled', true);
                    $("#votedown").hide();
                    $("#voteup").prop('disabled', false);
                    $("#voteup").show();
                }
            }
        });
    });
}

function VoteComment(selector, url, commentid) {
    $(document).on('click', selector, function (event) {
        event.preventDefault();
        var vote = $(this).data('vote');
        var id = $(this).data(commentid);
        console.log(selector);
        console.log(vote);
        console.log(url);

        $("#tempmessage").removeClass("alert-success");
        $("#tempmessage").removeClass("alert-danger");
        $("#tempmessage").html('');
        $.post(url, { id: id, vote: vote }, function (data) {
            $("#tempmessage").addClass("panel-body");
            $("#tempmessage").html(data.message);
            console.log(data.message);
            if (data.successful) {
                $("#points-comment-" + id).text(data.points);
                if (vote === true) {
                    $("#voteup-comment-" + id).prop('disabled', true);
                    $("#voteup-comment-" + id).hide();
                    $("#votedown-comment-" + id).prop('disabled', false);
                    $("#votedown-comment-" + id).show();
                } else {
                    $("#votedown-comment-" + id).prop('disabled', true);
                    $("#votedown-comment-" + id).hide();
                    $("#voteup-comment-" + id).prop('disabled', false);
                    $("#voteup-comment-" + id).show();
                }
            }
        });
    });
}

function SetDeleteAjax(selector, url, paramname1, paramname2) {
    $(document).on('click', selector, function (event) {
        event.preventDefault();
        var tag = $(this).data(paramname1);
        var subject = $(this).data(paramname2);
        var span = $(this).parent('span');
        if (confirm('Obrisati zapis?')) {
            var token = $('input[name="__RequestVerificationToken"]').first().val();
            $("#tempmessage").children().remove();
            $("#tempmessage").removeClass("alert-success");
            $("#tempmessage").removeClass("alert-danger");
            $("#tempmessage").html('');
            $.post(url, { tagid: tag, subjectid: subject, __RequestVerificationToken: token }, function (data) {
                if (data.success) {
                    $(span).remove();
                }
                $("#tempmessage").addClass(data.success ? "alert alert-success" : "alert alert-danger");
                $("#tempmessage").html(data.message);
            }).fail(function (jqXHR) {
                alert(jqXHR.status + " : " + jqXHR.responseText);
            });
        }
    });
}

function GraphByYear(chartid, url) {
    $(document).ready(function () {
        //console.log(url);
        //console.log(chartid);

        $.ajax({
            url: url,
            async: false,
            type: 'GET',
            success: function (data) {
                console.log(data);

                var grades = [];
                var content = $.parseJSON(data);
                var tickArray = [];
                $.each(content, function (key, value) {
                    var year = parseInt(key);
                    tickArray.push(year);
                    console.log([year, value]);
                    //grades.push([value, parseInt(key)]);
                    grades.push([parseInt(key), value]);
                });
                if (tickArray.length <= 1) {
                    var elem = $("#" + chartid).toggle();
                }
                //console.log(grades);
                var plot = $.jqplot(chartid, [grades], {
                    // Turns on animatino for all series in this plot.
                    animate: true,
                    series: [
                        {
                            pointLabels: {
                                show: true
                            },
                            rendererOptions: {
                                // speed up the animation a little bit.
                                // This is a number of milliseconds.
                                // Default for a line series is 2500.
                                animation: {
                                    speed: 1500
                                }
                            }
                        }
                    ],
                    axesDefaults: {
                        pad: 5
                    },
                    axes: {
                        xaxis: {
                            ticks: tickArray,
                            tickOptions: {
                                formatString: "%d"
                            },
                            drawMajorGridlines: false,
                            drawMinorGridlines: false,
                            drawMajorTickMarks: false
                        },
                        yaxis: {
                            min: 1,
                            max: 5,
                            tickInterval: 1,
                            tickOptions: {                                
                                formatString: "%.2f"
                            },
                            rendererOptions: {
                                forceTickAt0: true
                            }
                        }
                    },
                    highlighter: {
                        show: true,
                        showLabel: true,
                        tooltipAxes: 'y',
                        sizeAdjust: 7.5, tooltipLocation: 'ne'
                    }
                });
            }
        });
    });
}

$(function () {

    $('a[href="#toggle-search"], .navbar-bootsnipp .bootsnipp-search .input-group-btn > .btn[type="reset"]').on('click', function (event) {
        event.preventDefault();
        $('.navbar-bootsnipp .bootsnipp-search .input-group > input').val('');
        $('.navbar-bootsnipp .bootsnipp-search').toggleClass('open');
        $('a[href="#toggle-search"]').closest('li').toggleClass('active');

        if ($('.navbar-bootsnipp .bootsnipp-search').hasClass('open')) {
            /* I think .focus dosen't like css animations, set timeout to make sure input gets focus */
            setTimeout(function () {
                $('.navbar-bootsnipp .bootsnipp-search .form-control').focus();
            }, 100);
        }
    });

    $(document).on('keyup', function (event) {
        if (event.which == 27 && $('.navbar-bootsnipp .bootsnipp-search').hasClass('open')) {
            $('a[href="#toggle-search"]').trigger('click');
        }
    });

});

/* user profile */
//tab js//
$(document).ready(function (e) {


    $('.form').find('input, textarea').on('keyup blur focus', function (e) {

        var $this = $(this),
            label = $this.prev('label');

        if (e.type === 'keyup') {
            if ($this.val() === '') {
                label.removeClass('active highlight');
            } else {
                label.addClass('active highlight');
            }
        } else if (e.type === 'blur') {
            if ($this.val() === '') {
                label.removeClass('active highlight');
            } else {
                label.removeClass('highlight');
            }
        } else if (e.type === 'focus') {

            if ($this.val() === '') {
                label.removeClass('highlight');
            }
            else if ($this.val() !== '') {
                label.addClass('highlight');
            }
        }

    });

    $('.tab a').on('click', function (e) {

        e.preventDefault();

        $(this).parent().addClass('active');
        $(this).parent().siblings().removeClass('active');
        target = $(this).attr('href');

        $('.tab-content > div').not(target).hide();

        $(target).fadeIn(600);

    });
    //canvas off js//
    $('#menu_icon').click(function () {
        if ($("#content_details").hasClass('drop_menu')) {
            $("#content_details").addClass('drop_menu1').removeClass('drop_menu');
        }
        else {
            $("#content_details").addClass('drop_menu').removeClass('drop_menu1');
        }


    });

    //search box js//


    $("#flip").click(function () {
        $("#panel").slideToggle("5000");
    });

    // sticky js//

    //$(window).scroll(function () {
    //    if ($(window).scrollTop() >= 500) {
    //        $('nav').addClass('stick');
    //    }
    //    else {
    //        $('nav').removeClass('stick');
    //    }
    //});




});