var accountApi = getDomain() + "api/account/";

function getAuthorizationHeader() {
    var accessToken = localStorage.getItem('access_token');
    var header = { Authorization: 'Bearer ' + accessToken };
    return header;
}

function testAuthorization() {
    var authApi = accountApi + "testAuth";
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


function getRemainingCredits(displayCredits) {
    var creditsUrl = accountApi + "credits";
    $.ajax({
        url: creditsUrl,
        headers: getAuthorizationHeader(),
        success: function (result) {
            displayCredits(result);
        },
        error: function (result, status) {
            errorHandling(result, null);
        }
    });
}