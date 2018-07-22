$('#navProfileDesktop')
    .dropdown({
        action: 'hide'
    })

$('#modal')
    .modal('attach events', '#show_modal', 'show')
    ;

function show_hide() {
    var x = document.getElementById("toggle");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
}