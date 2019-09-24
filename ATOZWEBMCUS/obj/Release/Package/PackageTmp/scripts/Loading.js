var contentModified = false;
var inFormOrLink = false;


$(document).ready(function () {

    window.showSpinner = function () {
        $("#spinnerDiv").show();
    };

    window.hideSpinner = function () {
        $("#spinnerDiv").hide();
    };

    window.setContentModified = function (flag) {
        contentModified = flag;
    };

    $('a').live('click', function () {
        //Anchor click
        //alert('Anchor');
        inFormOrLink = true;
    });

    $('form').bind('submit', function () {
        //Button Submit
        //alert('Button Submit');
        inFormOrLink = true;
    });

    $("input, select").change(function () {
        //Button text changed , Dropdown Selected index changed , radio button selected changed, check box selected changed
        //alert('Button text changed , Dropdown Selected index changed , radio button selected changed, check box selected changed');
        contentModified = true;
        if (window.location != window.parent.location) {
            window.parent.setContentModified(true);
        }
    });

    $("input").keypress(function () {
        //Button keypress
        //alert('Button keypress');
        contentModified = true;
        if (window.location != window.parent.location) {
            window.parent.setContentModified(true);
        }
    });

    $(window).bind("beforeunload", function (event) {
        if (window.location != window.parent.location) {
            window.parent.showSpinner();
        } else {
            if (inFormOrLink || contentModified) {
                $("#spinnerDiv").show();
            }
        }

    });

    $(window).load(function () {
        $("#spinnerDiv").hide();
        //$(window).scrollTop();
    });

    $("iframe").load(function () {
        $("#spinnerDiv").hide();
        window.scrollTo(0, 0);
    });

});
