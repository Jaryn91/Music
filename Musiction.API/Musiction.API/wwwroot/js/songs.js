var songApi = getDomain() + "api/songs/";


function addSong(song, funcOk, funcError) {
    $.ajax({
        url: songApi,
        type: 'POST',
        data: song,
        headers: getAuthorizationHeader(),
        processData: false, // tell jQuery not to process the data
        contentType: false, // tell jQuery not to set contentType
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

function getSong(songId, func) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        headers: getAuthorizationHeader()
    })
        .done(function (result) {
            func(result);
        });
}

function updateSong(songId, song, funcOk, funcError) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        type: "PUT",
        data: song,
        headers: getAuthorizationHeader(),
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
            funcOk();
        },
        error: function (result, status) {
            errorHandling(result, funcError);
        },
        complete: function (result, status) {
        }
    });
}

function deleteSong(funcOk, songId, songName) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        headers: getAuthorizationHeader(),
        type: 'DELETE',
        success: function (result) {
            funcOk(result);
        },
        error: function (result, status) {
            errorHandling(result, null);
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
