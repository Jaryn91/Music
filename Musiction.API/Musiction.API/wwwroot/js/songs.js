var songApi = getDomain() + "api/songs/";

function getAllSongs(lstBoxName, func) {
    $.ajax({
        url: songApi,
        headers: getAuthorizationHeader(),
        success: function (result) {
            func(lstBoxName, result);
        },
        error: function (result, status) {
            errorHandling(result, null);
        }
    });
}

function addOptionsToListBox(lstBoxName, result) {
    var option = '';
    for (var i = 0; i < result.length; i++) {
        option += '<li class="ui-state-default" value="' + result[i].id + '">' + result[i].name + '</li>';
    }
    $(lstBoxName).append(option);
}


function addSong(song, funcOk, funcError) {
    $.ajax({
        url: songApi,
        type: 'POST',
        data: song,
        headers: getAuthorizationHeader(),
        processData: false, // tell jQuery not to process the data
        contentType: false, // tell jQuery not to set contentType
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

function deleteSong(songId) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        headers: getAuthorizationHeader(),
        type: 'DELETE',
        success: function (result) {
            alert("usunięto");
        }
    });
}

function errorHandling(result, funcError) {
    var errorMassage;
    if (result.status === 401) {
        errorMassage = "No weź!! Zaloguj się, żeby coś zrobić!";
    }
    else if (result.status === 400) {
        errorMassage = funcError(result, status);
    }
    else {
        errorMassage = "I nawet nie wiem co nie działa :(((.";
    }
    displayAlert("Coś tutaj chyba nie gra", errorMassage);
}
