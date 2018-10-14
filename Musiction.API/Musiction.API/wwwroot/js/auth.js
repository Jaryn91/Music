var authApi = getDomain() + "api/testAuth/";

function getAuthorizationHeader() {
    var accessToken = localStorage.getItem('access_token');
    var header = { Authorization: 'Bearer ' + accessToken };
    return header;
}

function testAuthorization() {
    $.ajax({
        url: authApi,
        headers: getAuthorizationHeader(),
        success: function (result) {
            displayAlert("Sukces!", "Brawo jesteś zalogowany! Wszystko działa :)!");
        },
        error: function (result, status) {
            errorHandling(result, null);
        }
    });
}