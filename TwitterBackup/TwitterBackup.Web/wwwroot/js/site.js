$(function () {
    console.log("hit");
    $('#logoutLink').on("click", function (e) {
        e.preventDefault();
        $.post(this.href, function () { })
    })
})