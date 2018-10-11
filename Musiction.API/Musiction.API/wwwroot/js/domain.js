function getDomain() {
    return document.location.origin + "/";
}

function loadNavBar() {
    $("#nav-placeholder").load("nav.html");
};
