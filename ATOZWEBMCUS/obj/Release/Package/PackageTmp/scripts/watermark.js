$(function() {

    $(".cls").each(function() {
        $txt = $(this);
        if ($txt.val() != this.title) {
            $txt.removeClass("cls");

        }
    });

    $(".cls").focus(function() {
        $txt = $(this);
        if ($txt.val() == this.title) {
            $txt.val("");
            $txt.removeClass("cls");
        }
    });

    $(".cls").blur(function() {
        $txt = $(this);
        if ($.trim($txt.val()) == "") {
            $txt.val(this.title);
            $txt.addClass("cls");
        }
    });
});        