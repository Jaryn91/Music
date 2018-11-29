var presentationsApi = getDomain() + "api/presentation/";

function createPresentation(outputFileType, songs, funcOk, funcError) {
    var url = presentationsApi + outputFileType + songs;
    $.ajax({
        url: url,
        type: 'GET',
        headers: getAuthorizationHeader(),
        success: function (result) {
            funcOk(result);
        },
        error: function (result, status) {
            errorHandling(result, funcError);
        },
        complete: function (result, status) {
        }
    });
}



function errorHandling(result, funcError) {
    var errorMassage;
    if (result.status === 401) {
        errorMassage = "No weź!! <a href=\"login.html\">Zaloguj się</a>, żeby coś zrobić!";
    }
    else if (result.status === 400) {
        errorMassage = funcError(result, status);
    }
    else {
        errorMassage = "I nawet nie wiem co nie działa :(((.";
    }
    displayAlert("Coś tutaj chyba nie gra", errorMassage);
}
