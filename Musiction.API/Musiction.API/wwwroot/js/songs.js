var songApi = getDomain() + "api/songs/";


function addSong(songName, funcOk, funcError) {
    $.ajax({
        url: songApi,
        type: 'POST',
        data: songName,
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
    if (allSongs !== null) {
        var song = $.grep(allSongs, function (e) { return e.id === songId; });
        if (song !== null) {
            func(song[0]);
            return;
        }
    }

    var url = songApi + songId;
    $.ajax({
        url: url,
        headers: getAuthorizationHeader()
    })
        .done(function (result) {
            func(result.songs[0]);
        });
}

function updateSong(songId, songToUpdate, funcOk, funcError) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        type: "PUT",
        data: songToUpdate,
        headers: getAuthorizationHeader(),
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
            updateSongInList(songId, songToUpdate);
            funcOk();
        },
        error: function (result, status) {
            errorHandling(result, funcError);
        },
        complete: function (result, status) {
        }
    });
}

function deleteSong(funcOk, songId) {
    var url = songApi + songId;
    $.ajax({
        url: url,
        headers: getAuthorizationHeader(),
        type: 'DELETE',
        success: function (result) {
            funcOk(result);
            deleteSongFromList(songId);
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

function updateSongInList(songId, songToUpdate) {
    if (allSongs !== null) {
        song = allSongs.find(item => item.id == songId);
        song.name = songToUpdate.get("Name");
        song.youTubeUrl = songToUpdate.get("YouTubeUrl");
    }
}

function deleteSongFromList(songId) {
    var index = allSongs.findIndex(item => item.id === songId);
    allSongs.splice(index, 1);
}